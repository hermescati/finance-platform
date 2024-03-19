using Expensier.Domain.Services.Transactions;
using Expensier.WPF.Controls;
using Expensier.WPF.DataObjects;
using Expensier.WPF.Enums;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Expenses;
using Expensier.WPF.State.Navigators;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;


namespace Expensier.WPF.ViewModels.Expenses
{
    public class TransactionViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        private readonly TransactionStore _transactionStore;
        private readonly ITransactionService _transactionService;
        private readonly IRenavigator _renavigator;
        private readonly Func<IEnumerable<TransactionModel>, IEnumerable<TransactionModel>> _filterTransaction;


        private readonly ObservableCollection<TransactionModel> _transactions;
        public IEnumerable<TransactionModel> Transactions => _transactions;


        private bool _listEmpty;
        public bool ListEmpty
        {
            get => _listEmpty;
            set
            {
                _listEmpty = value;
                OnPropertyChanged( nameof( ListEmpty ) );
            }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged( nameof( SearchQuery ) );
            }
        }


        private SortOptions _selectedItem;
        public SortOptions SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged( nameof( SelectedItem ) );
            }
        }
        public IEnumerable<SortOptions> Sort => Enum.GetValues( typeof( SortOptions ) )
            .Cast<SortOptions>();


        public TransactionViewModel(
            AccountStore accountStore,
            TransactionStore transactionStore,
            ITransactionService transactionService,
            IRenavigator renavigator ) : this(
                accountStore,
                transactionStore,
                transactionService,
                renavigator,
                transactions => transactions )
        { }


        public TransactionViewModel(
            AccountStore accountStore,
            TransactionStore transactionStore,
            ITransactionService transactionService,
            IRenavigator renavigator,
            Func<IEnumerable<TransactionModel>, IEnumerable<TransactionModel>> filterTransactions )
        {
            _accountStore = accountStore;
            _transactionStore = transactionStore;
            _transactionService = transactionService;
            _renavigator = renavigator;
            _filterTransaction = filterTransactions;

            _transactions = new ObservableCollection<TransactionModel>();
            _transactionStore.StateChanged += Transaction_StateChanged;

            ResetTransactions();
        }


        public TransactionViewModel(
            TransactionStore transactionStore,
            Func<IEnumerable<TransactionModel>, IEnumerable<TransactionModel>> filterTransactions )
        {
            _transactionStore = transactionStore;
            _filterTransaction = filterTransactions;

            _transactions = new ObservableCollection<TransactionModel>();
            _transactionStore.StateChanged += Transaction_StateChanged;

            ResetTransactions();
        }


        private void ResetTransactions()
        {
            IEnumerable<TransactionModel> filteredTransactions = _transactionStore.TransactionList
                .Select( t => new TransactionModel( t.ID, t.Name, t.Category, t.Amount, t.IsCredit, t.ProcessedDate, _accountStore, _transactionService, _renavigator ) )
                .OrderByDescending( o => o.ProcessedDate );

            _transactions.Clear();
            foreach ( TransactionModel transaction in filteredTransactions )
            {
                _transactions.Add( transaction );
            }

            ListEmpty = _transactions.IsNullOrEmpty();

            PropertyChanged += PropertyChangedEventHandler;
        }


        public void FilterTransactions( string query )
        {
            IEnumerable<TransactionModel> filteredTransactions = new List<TransactionModel>( _transactions );

            filteredTransactions = filteredTransactions
                .Where( t => t.Name.ToLower().Contains( query.ToLower() ) );

            _transactions.Clear();
            foreach ( TransactionModel transaction in filteredTransactions )
            {
                _transactions.Add( transaction );
            }
        }


        public void SortTransactions()
        {
            IEnumerable<TransactionModel> sortedTransactions = new List<TransactionModel>( _transactions );

            sortedTransactions = _selectedItem switch
            {
                SortOptions.Asceding => sortedTransactions.OrderBy( t => t.Name ),
                SortOptions.Descending => sortedTransactions.OrderByDescending( t => t.Name ),
                SortOptions.Amount => sortedTransactions.OrderByDescending( t => t.Amount ),
                _ => sortedTransactions.OrderBy( s => s.ProcessedDate )
            };

            _transactions.Clear();
            foreach ( TransactionModel transaction in sortedTransactions )
            {
                _transactions.Add( transaction );
            }
        }


        private void PropertyChangedEventHandler( object sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof( SelectedItem ) )
            {
                SortTransactions();
            }
            if ( e.PropertyName == nameof( SearchQuery ) )
            {
                if ( SearchQuery.IsNullOrEmpty() )
                {
                    ResetTransactions();
                }
                else
                {
                    FilterTransactions( SearchQuery );
                }
            }
        }


        private void Transaction_StateChanged()
        {
            ResetTransactions();
        }
    }
}
