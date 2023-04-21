using Expensier.Domain.Services.Subscriptions;
using Expensier.Domain.Services.Transactions;
using Expensier.WPF.Commands;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Expenses;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.State.Subscriptions;
using Expensier.WPF.ViewModels.Charts;
using Expensier.WPF.ViewModels.Expenses;
using Expensier.WPF.ViewModels.Modals;
using Expensier.WPF.ViewModels.Subscriptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Expensier.WPF.ViewModels.Charts.ChartDropdownValues;

namespace Expensier.WPF.ViewModels
{
    public class ExpensesViewModel : ViewModelBase
    {
        public TransactionViewModel TransactionViewModel { get; }
        public SubscriptionViewModel SubscriptionViewModel { get; }
        public TransactionModalViewModel TransactionModalViewModel { get; }
        public SubscriptionModalViewModel SubscriptionModalViewModel { get; }
        public MonthlyExpensesViewModel MonthlyExpensesViewModel { get; }
        public PredictionsViewModel PredictionsViewModel { get; }

        private string _searchBar;
        public string SearchBar
        {
            get
            {
                return _searchBar;
            }
            set
            {
                _searchBar = value;
                OnPropertyChanged(nameof(SearchBar));
            }
        }

        public ExpensesViewModel(
            TransactionStore transactionStore, 
            SubscriptionStore subscriptionStore, 
            TransactionModalViewModel transactionModalViewModel, 
            SubscriptionModalViewModel subscriptionModalViewModel,
            MonthlyExpensesViewModel monthlyExpensesViewModel,
            PredictionsViewModel predictionsViewModel,
            ITransactionService transactionService,
            ISubscriptionService subscriptionService,
            AccountStore accountStore,
            IRenavigator renavigator)
        {
            TransactionViewModel = new TransactionViewModel(transactionStore, transactionService, accountStore, renavigator);
            SubscriptionViewModel = new SubscriptionViewModel(subscriptionStore, subscriptionService, accountStore, renavigator);
            MonthlyExpensesViewModel = monthlyExpensesViewModel;
            PredictionsViewModel = predictionsViewModel;
            TransactionModalViewModel = transactionModalViewModel;
            SubscriptionModalViewModel = subscriptionModalViewModel;

            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(SearchBar))
                {
                    if (SearchBar.IsNullOrEmpty())
                    {
                        TransactionViewModel.ResetTransactions();
                    }
                    else
                    {
                        TransactionViewModel.FilterTransactions(SearchBar);
                    }
                }
            };
        }
    }
}
