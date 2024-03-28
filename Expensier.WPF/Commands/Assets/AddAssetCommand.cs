using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Modals;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using static Expensier.Domain.Models.AssetTransaction;


namespace Expensier.WPF.Commands.Assets
{
    public class AddAssetCommand : AsyncCommandBase
    {
        private readonly CryptoModalViewModel _assetModalViewModel;
        private readonly IAssetService _assetService;
        private readonly IRenavigator _renavigator;
        private readonly AccountStore _accountStore;


        public AddAssetCommand(
            CryptoModalViewModel assetModalViewModel,
            IAssetService assetService,
            IRenavigator renavigator,
            AccountStore accountStore )
        {
            _assetModalViewModel = assetModalViewModel;
            _assetService = assetService;
            _renavigator = renavigator;
            _accountStore = accountStore;

            _assetModalViewModel.PropertyChanged += CryptoModalViewModel_PropertyChanged;
        }


        public override bool CanExecute( object parameter ) =>
            _assetModalViewModel.CanAdd &&
            base.CanExecute( parameter );


        public override async Task ExecuteAsync( object parameter )
        {
            try
            {
                Account updatedAccount = await _assetService.AddAsset(
                    _accountStore.CurrentAccount,
                    _assetModalViewModel.Crypto,
                    _assetModalViewModel.PurchasePrice,
                    _assetModalViewModel.Coins,
                    AssetType.Cryptocurrency,
                    DateTime.Now );

                _accountStore.CurrentAccount = updatedAccount;
                _assetModalViewModel.CryptoSymbol = string.Empty;
                _assetModalViewModel.Crypto = null;
                _assetModalViewModel.PurchasePrice = 0.0;
                _assetModalViewModel.Coins = 0.0;
                _renavigator.Renavigate();
            }
            catch ( Exception e )
            {
                Trace.TraceError( e.Message );
                throw;
            }
        }


        private void CryptoModalViewModel_PropertyChanged( object? sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof( CryptoModalViewModel.CanAdd ) )
                OnCallExecuteChanged();
        }
    }
}
