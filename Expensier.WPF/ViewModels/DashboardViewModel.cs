using Expensier.WPF.ViewModels.Expenses;
using Expensier.WPF.ViewModels.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public RecentExpensesViewModel RecentExpensesViewModel { get; }
        public TopSubscriptionsViewModel TopSubscriptionsViewModel { get; }

        public DashboardViewModel(RecentExpensesViewModel recentExpensesViewModel, TopSubscriptionsViewModel topSubscriptionsViewModel)
        {
            RecentExpensesViewModel = recentExpensesViewModel;
            TopSubscriptionsViewModel = topSubscriptionsViewModel;
        }
    }
}
