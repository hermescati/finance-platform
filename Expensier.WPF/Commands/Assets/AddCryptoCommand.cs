using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.Commands.Assets
{
    public class AddCryptoCommand : AsyncCommandBase
    {
        private readonly CryptoModalViewModel _cryptoModalViewModel;
        private readonly IAssetService _cryptoService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;

        public AddCryptoCommand(CryptoModalViewModel cryptoModalViewModel, IAssetService cryptoService, AccountStore accountStore, IRenavigator renavigator)
        {
            _cryptoModalViewModel = cryptoModalViewModel;
            _cryptoService = cryptoService;
            _accountStore = accountStore;
            _renavigator = renavigator;

            _cryptoModalViewModel.PropertyChanged += CryptoModalViewModel_PropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _cryptoModalViewModel.CanAdd && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                Account updatedAccount = await _cryptoService.AddCrypto(
                    _accountStore.CurrentAccount,
                    _cryptoModalViewModel.Crypto,
                    _cryptoModalViewModel.PurchasePrice,
                    _cryptoModalViewModel.Coins);

                _accountStore.CurrentAccount = updatedAccount;
                _cryptoModalViewModel.CryptoSymbol = string.Empty;
                _cryptoModalViewModel.Crypto = null;
                _cryptoModalViewModel.PurchasePrice = 0.0;
                _cryptoModalViewModel.Coins = 0.0;
                _renavigator.Renavigate();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CryptoModalViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CryptoModalViewModel.CanAdd))
            {
                OnCallExecuteChanged();
            }
        }
    }
}
