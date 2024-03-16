using Expensier.Domain.Models;
using Expensier.Domain.Services.Transactions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Modals;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Expensier.WPF.Commands.Transactions
{
    public class AddTransactionCommand : AsyncCommandBase
    {
        private readonly TransactionModalViewModel _transactionModalViewModel;
        private readonly ITransactionService _transactionService;
        private readonly IRenavigator _renavigator;
        private readonly AccountStore _accountStore;


        public AddTransactionCommand(
            TransactionModalViewModel transactionModalViewModel,
            ITransactionService transactionService,
            IRenavigator renavigator,
            AccountStore accountStore )
        {
            _transactionModalViewModel = transactionModalViewModel;
            _transactionService = transactionService;
            _renavigator = renavigator;
            _accountStore = accountStore;

            _transactionModalViewModel.PropertyChanged += TransactionModalViewModel_PropertyChanged;
        }


        public override bool CanExecute( object parameter ) =>
            _transactionModalViewModel.CanAdd &&
            base.CanExecute( parameter );


        public override async Task ExecuteAsync( object parameter )
        {
            try
            {
                Account updatedAccount = await _transactionService.AddTransaction(
                    _accountStore.CurrentAccount,
                    _transactionModalViewModel.TransactionName,
                    _transactionModalViewModel.Category,
                    _transactionModalViewModel.Amount,
                    _transactionModalViewModel.ProcessDate );

                _accountStore.CurrentAccount = updatedAccount;
                _transactionModalViewModel.TransactionName = string.Empty;
                _transactionModalViewModel.ProcessDate = DateTime.Now;
                _transactionModalViewModel.Amount = 0.0;
                _transactionModalViewModel.Category = Transaction.TransactionCategory.Income;
                _renavigator.Renavigate();
            }
            catch ( Exception e )
            {
                Trace.TraceError( e.Message );
                throw;
            }
        }


        private void TransactionModalViewModel_PropertyChanged( object? sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof( TransactionModalViewModel.CanAdd ) )
                OnCallExecuteChanged();
        }
    }
}
