using Expensier.Domain.Services.Transactions;
using Expensier.WPF.Commands.Transactions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using static Expensier.Domain.Models.Transaction;


namespace Expensier.WPF.ViewModels.Modals
{
    public class TransactionModalViewModel : ViewModelBase
    {
        private string _transactionName;
        public string TransactionName
        {
            get => _transactionName;
            set
            {
                _transactionName = value;
                OnPropertyChanged( nameof( TransactionName ) );
                OnPropertyChanged( nameof( CanAdd ) );
            }
        }


        private double _amount;
        public double Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged( nameof( Amount ) );
                OnPropertyChanged( nameof( CanAdd ) );
            }
        }


        private TransactionCategory _category;
        public TransactionCategory Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged( nameof( Category ) );
            }
        }


        private DateTime _processDate = DateTime.Now;
        public DateTime ProcessDate
        {
            get => _processDate;
            set
            {
                _processDate = value;
                OnPropertyChanged( nameof( ProcessDate ) );
            }
        }


        public bool CanAdd =>
            !string.IsNullOrEmpty( TransactionName ) &&
            Amount > 0.0;


        public IEnumerable<TransactionCategory> TransactionType =>
            Enum.GetValues( typeof( TransactionCategory ) ).Cast<TransactionCategory>();


        public ICommand AddNewTransaction { get; }


        public TransactionModalViewModel(
            ITransactionService transactionService,
            AccountStore accountStore,
            IRenavigator renavigator )
        {
            AddNewTransaction = new AddTransactionCommand( this, transactionService, renavigator, accountStore );
        }
    }
}
