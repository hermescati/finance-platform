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
        public SubscriptionViewModel SubscriptionViewModel { get; }
        public ModalViewModel ModalViewModel { get; }

        public ExpensesViewModel(TransactionViewModel recentTransactions, SubscriptionViewModel subscriptionViewModel, ModalViewModel modalViewModel)
        {
            TransactionViewModel = recentTransactions;
            SubscriptionViewModel = subscriptionViewModel;
            ModalViewModel = modalViewModel;
        }
    }
}
