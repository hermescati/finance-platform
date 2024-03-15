using Expensier.Domain.Models;
using Expensier.Domain.Services.Transactions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels;
using Expensier.WPF.ViewModels.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.Commands
{
    public class AddTransactionCommand : AsyncCommandBase
    {
        private readonly TransactionModalViewModel _transactionModalViewModel;
        private readonly ITransactionService _transactionService;
        private readonly AccountStore _accountStore;
        private readonly IRenavigator _renavigator;

        public AddTransactionCommand( TransactionModalViewModel transactionModalViewModel, ITransactionService transactionService, AccountStore accountStore, IRenavigator renavigator )
        {
            _transactionModalViewModel = transactionModalViewModel;
            _transactionService = transactionService;
            _accountStore = accountStore;
            _renavigator = renavigator;

            _transactionModalViewModel.PropertyChanged += TransactionModalViewModel_PropertyChanged;
        }

        public override bool CanExecute( object parameter )
        {
            return _transactionModalViewModel.CanAdd && base.CanExecute( parameter );
        }

        public override async Task ExecuteAsync( object parameter )
        {
            try
            {
                Account updatedAccount = await _transactionService.AddTransaction(
                    _accountStore.CurrentAccount,
                    _transactionModalViewModel.TransactionName,
                    _transactionModalViewModel.ProcessDate,
                    _transactionModalViewModel.Amount,
                    _transactionModalViewModel.Category );

                _accountStore.CurrentAccount = updatedAccount;
                _transactionModalViewModel.TransactionName = string.Empty;
                _transactionModalViewModel.ProcessDate = DateTime.Now;
                _transactionModalViewModel.Amount = 0.0;
                _transactionModalViewModel.Category = TransactionType.Salary;
                _renavigator.Renavigate();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        private void TransactionModalViewModel_PropertyChanged( object? sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof( TransactionModalViewModel.CanAdd ) )
            {
                OnCallExecuteChanged();
            }
        }
    }
}
