using Expensier.WPF.State.Expenses;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Expenses
{
    public class RecentExpensesViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        public TransactionViewModel TransactionViewModel { get; }
        private readonly IEnumerable<TransactionDataModel> _transactions;

        private bool _listEmpty;
        public bool ListEmpty
        {
            get
            {
                return _listEmpty;
            }
            set
            {
                _listEmpty = value;
                OnPropertyChanged(nameof(ListEmpty));
            }
        }

        private bool _listNotEmpty;
        public bool ListNotEmpty
        {
            get
            {
                return _listNotEmpty;
            }
            set
            {
                _listNotEmpty = value;
                OnPropertyChanged(nameof(ListNotEmpty));
            }
        }

        public IEnumerable<TransactionDataModel> Transactions => _transactions;

        public RecentExpensesViewModel(TransactionStore transactionStore)
        {
            _transactions = new ObservableCollection<TransactionDataModel>();
            _transactionStore = transactionStore;
            TransactionViewModel = new TransactionViewModel(transactionStore, transactions => transactions.Where(t => t.IsCredit == true).Take(5));

            _transactions = TransactionViewModel.Transactions;

            if (_transactions.IsNullOrEmpty())
            {
                _listEmpty = true;
                _listNotEmpty = false;
            }
            else
            {
                _listNotEmpty = true;
                _listEmpty = false;
            }
        }
    }
}
