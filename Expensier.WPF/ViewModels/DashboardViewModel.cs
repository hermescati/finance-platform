using Expensier.WPF.Commands;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Charts;
using Expensier.WPF.ViewModels.Cryptos;
using Expensier.WPF.ViewModels.Expenses;
using Expensier.WPF.ViewModels.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Expensier.WPF.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public RecentExpensesViewModel RecentExpensesViewModel { get; }
        public TopSubscriptionsViewModel TopSubscriptionsViewModel { get; }
        public TopPerformingCryptosViewModel TopPerformingCryptosViewModel { get; }
        public SpendingSummaryViewModel SpendingSummaryViewModel { get; }
        public ExpenditureAllocationViewModel ExpenditureAllocationViewModel { get; }


        public ICommand NavigateToWatchlist { get; }


        public DashboardViewModel(
            RecentExpensesViewModel recentExpensesViewModel,
            TopSubscriptionsViewModel topSubscriptionsViewModel,
            TopPerformingCryptosViewModel topPerformingCryptosViewModel,
            SpendingSummaryViewModel spendingSummaryViewModel,
            ExpenditureAllocationViewModel expenditureAllocationViewModel,
            IRenavigator portfolioNavigator )
        {
            RecentExpensesViewModel = recentExpensesViewModel;
            TopSubscriptionsViewModel = topSubscriptionsViewModel;
            TopPerformingCryptosViewModel = topPerformingCryptosViewModel;
            SpendingSummaryViewModel = spendingSummaryViewModel;
            ExpenditureAllocationViewModel = expenditureAllocationViewModel;

            NavigateToWatchlist = new RenavigateCommand( portfolioNavigator );
        }
    }
}
