using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.WPF.DataObjects;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using System;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Expensier.WPF.Commands.Assets
{
    public class DeleteAssetCommand : AsyncCommandBase
    {
        private readonly AssetModel _assetDataModel;
        private readonly IAssetService _assetService;
        private readonly IRenavigator _renavigator;
        private readonly AccountStore _accountStore;


        public DeleteAssetCommand(
            AssetModel assetDataModel,
            IAssetService assetService,
            IRenavigator renavigator,
            AccountStore accountStore )
        {
            _assetDataModel = assetDataModel;
            _assetService = assetService;
            _renavigator = renavigator;
            _accountStore = accountStore;
        }


        public override async Task ExecuteAsync( object parameter )
        {
            try
            {
                Account updatedAccount = await _assetService.DeleteAsset(
                    _accountStore.CurrentAccount,
                    _assetDataModel.ID );

                _accountStore.CurrentAccount = updatedAccount;
                _renavigator.Renavigate();
            }
            catch ( Exception e )
            {
                Trace.TraceError( e.Message );
                throw;
            }
        }
    }
}
