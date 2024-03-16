using Expensier.Domain.Models;
using Expensier.Domain.Services.Transactions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Expenses;
using System;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Expensier.WPF.Commands.Transactions
{
    public class DeleteTransactionCommand : AsyncCommandBase
    {
        private readonly TransactionDataModel _transactionDataModel;
        private readonly ITransactionService _transactionService;
        private readonly IRenavigator _renavigator;
        private readonly AccountStore _accountStore;


        public DeleteTransactionCommand(
            TransactionDataModel transactionDataModel,
            ITransactionService transactionService,
            IRenavigator renavigator,
            AccountStore accountStore )
        {
            _transactionDataModel = transactionDataModel;
            _transactionService = transactionService;
            _renavigator = renavigator;
            _accountStore = accountStore;
        }


        public override async Task ExecuteAsync( object parameter )
        {
            try
            {
                Account updatedAccount = await _transactionService.DeleteTransaction(
                    _accountStore.CurrentAccount,
                    _transactionDataModel.ID );

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
