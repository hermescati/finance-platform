using Expensier.Domain.Services.Transactions;
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
using System.Windows.Input;


namespace Expensier.WPF.ViewModels.Expenses
{
    public class TransactionViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        private readonly TransactionStore _transactionStore;
        private readonly ITransactionService _transactionService;
        private readonly IRenavigator _renavigator;
        private readonly Func<IEnumerable<TransactionModel>, IEnumerable<TransactionModel>> _filterTransaction;

        private ObservableCollection<TransactionModel> _transactions;
        public IEnumerable<TransactionModel> Transactions => _transactions;

        private readonly ObservableCollection<TransactionModel> _paginatedResult;
        public IEnumerable<TransactionModel> PaginatedResult => _paginatedResult;


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
                SortTransactions();
            }
        }
        public IEnumerable<SortOptions> Sort => Enum.GetValues( typeof( SortOptions ) )
            .Cast<SortOptions>();


        private readonly int _itemsPerPage = 9;

        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged( nameof( CurrentPage ) );
                ApplyPagination();
            }
        }

        private int _totalPages;
        public int TotalPages
        {
            get => _totalPages;
            set
            {
                _totalPages = value;
                OnPropertyChanged( nameof( TotalPages ) );
            }
        }

        private IEnumerable<int> _pages;
        public IEnumerable<int> Pages
        {
            get => _pages;
            set
            {
                _pages = value;
                OnPropertyChanged( nameof( Pages ) );
            }
        }


        private int _startIndex;
        public int StartIndex
        {
            get => _startIndex;
            set
            {
                _startIndex = value;
                OnPropertyChanged( nameof( StartIndex ) );
            }
        }

        private int _endIndex;
        public int EndIndex
        {
            get => _endIndex;
            set
            {
                _endIndex = value;
                OnPropertyChanged( nameof( EndIndex ) );
            }
        }


        public ICommand PreviousPage => new RelayCommand(
            _ => CurrentPage--,
            _ => CurrentPage > 1 );
        public ICommand NextPage => new RelayCommand(
            _ => CurrentPage++,
            _ => CurrentPage < TotalPages );
        public ICommand GoToPage => new RelayCommand(
            page => CurrentPage = Math.Min( Math.Max( (int) page, 1 ), TotalPages ),
            page => page != null && (int) page >= 1 && (int) page <= TotalPages );


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
            _paginatedResult = new ObservableCollection<TransactionModel>();
            _transactionStore.StateChanged += FetchTransactions;

            FetchTransactions();
            ApplyPagination();
        }


        public TransactionViewModel(
            TransactionStore transactionStore,
            Func<IEnumerable<TransactionModel>, IEnumerable<TransactionModel>> filterTransactions )
        {
            _transactionStore = transactionStore;
            _filterTransaction = filterTransactions;

            _transactions = new ObservableCollection<TransactionModel>();
            _transactionStore.StateChanged += FetchTransactions;

            FetchTransactions();
        }


        private void FetchTransactions()
        {
            IEnumerable<TransactionModel> transactions = _transactionStore.TransactionList
                .Select( t => new TransactionModel( t.ID, t.Name, t.Category, t.Amount, t.IsCredit, t.ProcessedDate, _accountStore, _transactionService, _renavigator ) )
                .OrderByDescending( o => o.ProcessedDate );

            _transactions.Clear();
            foreach ( TransactionModel transaction in transactions )
            {
                _transactions.Add( transaction );
            }

            ListEmpty = _transactions.IsNullOrEmpty();

            PropertyChanged += PropertyChangedEventHandler;
        }


        private void ApplyPagination()
        {
            int startIndex = (CurrentPage - 1) * _itemsPerPage;
            int endIndex = Math.Min( startIndex + _itemsPerPage, _transactions.Count );

            TotalPages = (int) Math.Ceiling( (double) _transactions.Count / _itemsPerPage );
            Pages = Enumerable.Range( 1, TotalPages );

            IEnumerable<TransactionModel> transactionsToDisplay = _transactions
                .Skip( startIndex )
                .Take( endIndex - startIndex );

            _paginatedResult.Clear();
            foreach ( TransactionModel transaction in transactionsToDisplay )
            {
                _paginatedResult.Add( transaction );
            }

            StartIndex = endIndex;
            EndIndex = _transactions.Count;
        }


        private void FilterTransactions( string query )
        {
            _transactions = new ObservableCollection<TransactionModel>( _transactions
                .Where( t => t.Name.ToLower().Contains( query.ToLower() ) ) );

            ApplyPagination();
        }


        private void SortTransactions()
        {
            switch ( _selectedItem )
            {
                case SortOptions.Asceding:
                    _transactions = new ObservableCollection<TransactionModel>( _transactions
                        .OrderBy( t => t.Name ) );
                    break;
                case SortOptions.Descending:
                    _transactions = new ObservableCollection<TransactionModel>( _transactions
                        .OrderByDescending( t => t.Name ) );
                    break;
                case SortOptions.Amount:
                    _transactions = new ObservableCollection<TransactionModel>( _transactions
                        .OrderByDescending( t => t.Amount ) );
                    break;
                case SortOptions.Date:
                    _transactions = new ObservableCollection<TransactionModel>( _transactions
                        .OrderBy( s => s.ProcessedDate ) );
                    break;
                default:
                    _transactions = new ObservableCollection<TransactionModel>( _transactions
                        .OrderByDescending( s => s.ProcessedDate ) );
                    break;
            }

            ApplyPagination();
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
                    FetchTransactions();
                    ApplyPagination();
                }
                else
                {
                    FilterTransactions( SearchQuery );
                }
            }
        }
    }
}
