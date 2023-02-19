using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels
{
    public class ExpensesViewModel : ViewModelBase
    {
        public TransactionViewModel TransactionViewModel { get; }

        public ExpensesViewModel(TransactionViewModel recentTransactions)
        {
            TransactionViewModel = recentTransactions;
        }
    }
}
