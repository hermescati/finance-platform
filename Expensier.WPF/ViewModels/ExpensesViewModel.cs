using Expensier.WPF.State.Expenses;
using Expensier.WPF.State.Subscriptions;
using Expensier.WPF.ViewModels.Expenses;
using Expensier.WPF.ViewModels.Modals;
using Expensier.WPF.ViewModels.Subscriptions;
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

        public ExpensesViewModel(TransactionStore transactionStore, SubscriptionStore subscriptionStore, ModalViewModel modalViewModel)
        {
            TransactionViewModel = new TransactionViewModel(transactionStore);
            SubscriptionViewModel = new SubscriptionViewModel(subscriptionStore);
            ModalViewModel = modalViewModel;
        }
    }
}
