using Expensier.WPF.Commands;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Assets;
using Expensier.WPF.ViewModels.Charts;
using Expensier.WPF.ViewModels.Expenses;
using Expensier.WPF.ViewModels.Subscriptions;
using System.Windows.Input;


namespace Expensier.WPF.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public SpendingSummaryViewModel SpendingSummaryViewModel { get; }
        public ExpenditureAllocationViewModel ExpenditureAllocationViewModel { get; }
        public RecentExpensesViewModel RecentExpensesViewModel { get; }
        public TopSubscriptionsViewModel TopSubscriptionsViewModel { get; }
        public AssetsPerformanceViewModel AssetsPerformanceViewModel { get; }


        public ICommand NavigateToWatchlist { get; }


        public DashboardViewModel(
            SpendingSummaryViewModel spendingSummaryViewModel,
            ExpenditureAllocationViewModel expenditureAllocationViewModel,
            RecentExpensesViewModel recentExpensesViewModel,
            TopSubscriptionsViewModel topSubscriptionsViewModel,
            AssetsPerformanceViewModel assetsPerformanceViewModel,
            IRenavigator portfolioNavigator )
        {
            SpendingSummaryViewModel = spendingSummaryViewModel;
            ExpenditureAllocationViewModel = expenditureAllocationViewModel;
            RecentExpensesViewModel = recentExpensesViewModel;
            TopSubscriptionsViewModel = topSubscriptionsViewModel;
            AssetsPerformanceViewModel = assetsPerformanceViewModel;

            NavigateToWatchlist = new RenavigateCommand( portfolioNavigator );
        }
    }
}
