using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Cryptos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.Commands
{
    public class DeleteCryptoCommand : AsyncCommandBase
    {
        private readonly CryptoDataModel _cryptoDataModel;
        private readonly ICryptoService _cryptoService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;

        public DeleteCryptoCommand(CryptoDataModel cryptoDataModel, ICryptoService cryptoService, AccountStore accountStore, IRenavigator renavigator)
        {
            _cryptoDataModel = cryptoDataModel;
            _cryptoService = cryptoService;
            _accountStore = accountStore;
            _renavigator = renavigator;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                Account updatedAccount = await _cryptoService.DeleteCrypto(
                    _accountStore.CurrentAccount,
                    _cryptoDataModel.Id);

                _accountStore.CurrentAccount = updatedAccount;
                _renavigator.Renavigate();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
