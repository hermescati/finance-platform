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
    public class DeleteTransactionCommand : AsyncCommandBase
    {
        private readonly TransactionDataModel _transactionDataModel;
        private readonly ITransactionService _transactionService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;

        public DeleteTransactionCommand(TransactionDataModel transactionDataModel, ITransactionService transactionService, AccountStore accountStore, IRenavigator renavigator)
        {
            _transactionDataModel = transactionDataModel;
            _transactionService = transactionService;
            _accountStore = accountStore;
            _renavigator = renavigator;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                Account updatedAccount = await _transactionService.DeleteTransaction(
                    _accountStore.CurrentAccount,
                    _transactionDataModel.Id);

                _accountStore.CurrentAccount = updatedAccount;
                _renavigator.Renavigate();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
