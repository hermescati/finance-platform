using Expensier.Domain.Models;
using Expensier.Domain.Services.Transactions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.Commands
{
    public class AddTransactionCommand : AsyncCommandBase
    {
        private readonly ModalViewModel _modalViewModel;
        private readonly ITransactionService _transactionService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;

        public AddTransactionCommand(ModalViewModel modalViewModel, ITransactionService transactionService, AccountStore accountStore, IRenavigator renavigator)
        {
            _modalViewModel = modalViewModel;
            _transactionService = transactionService;
            _accountStore = accountStore;
            _renavigator = renavigator;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                Account updatedAccount = await _transactionService.AddTransaction(
                    _accountStore.CurrentAccount,
                    _modalViewModel.TransactionName,
                    _modalViewModel.ProcessDate,
                    _modalViewModel.Amount,
                    _modalViewModel.Category);

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
