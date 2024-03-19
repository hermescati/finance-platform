using Expensier.WPF.DataObjects;
using Expensier.WPF.State.Expenses;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace Expensier.WPF.ViewModels.Expenses
{
    public class RecentExpensesViewModel : ViewModelBase
    {
        private readonly TransactionStore _transactionStore;
        public TransactionViewModel TransactionViewModel { get; }


        private readonly IEnumerable<TransactionModel> _transactions;
        public IEnumerable<TransactionModel> Transactions => _transactions;

        public RecentExpensesViewModel( TransactionStore transactionStore )
        {
            _transactionStore = transactionStore;
            _transactions = new ObservableCollection<TransactionModel>();

            TransactionViewModel = new TransactionViewModel( 
                transactionStore, 
                transactions => transactions
                .Where( t => t.IsCredit == true )
                .Take( 5 ) );

            _transactions = TransactionViewModel.Transactions;
        }
    }
}
