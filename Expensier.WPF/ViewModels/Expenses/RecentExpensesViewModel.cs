using Expensier.WPF.State.Expenses;
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

        public IEnumerable<TransactionDataModel> Transactions => _transactions;

        public RecentExpensesViewModel(TransactionStore transactionStore)
        {
            _transactions = new ObservableCollection<TransactionDataModel>();
            _transactionStore = transactionStore;
            TransactionViewModel = new TransactionViewModel(transactionStore, transactions => transactions.Where(t => t.IsCredit == true).Take(5));

            _transactions = TransactionViewModel.Transactions;
        }
    }
}
