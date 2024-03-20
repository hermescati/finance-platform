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
using Expensier.WPF.ViewModels.Transactions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Expensier.WPF.ViewModels
{
    public class ExpensesViewModel : ViewModelBase
    {
        public TransactionViewModel TransactionViewModel { get; }
        public SubscriptionsViewModel SubscriptionViewModel { get; }
        public TransactionModalViewModel TransactionModalViewModel { get; }
        public ExportModalViewModel ExportModalViewModel { get; }
        public SubscriptionModalViewModel SubscriptionModalViewModel { get; }
        public MonthlyExpensesViewModel MonthlyExpensesViewModel { get; }
        public PredictionsViewModel PredictionsViewModel { get; }


        public ExpensesViewModel(
            TransactionStore transactionStore,
            SubscriptionStore subscriptionStore,
            TransactionModalViewModel transactionModalViewModel,
            SubscriptionModalViewModel subscriptionModalViewModel,
            ExportModalViewModel exportModalViewModel,
            MonthlyExpensesViewModel monthlyExpensesViewModel,
            PredictionsViewModel predictionsViewModel,
            ITransactionService transactionService,
            ISubscriptionService subscriptionService,
            AccountStore accountStore,
            IRenavigator renavigator )
        {
            TransactionViewModel = new TransactionViewModel( accountStore, transactionStore, transactionService, renavigator );
            SubscriptionViewModel = new SubscriptionsViewModel( accountStore, subscriptionStore, subscriptionService,  renavigator );
            ExportModalViewModel = new ExportModalViewModel( transactionService, accountStore, renavigator );
            MonthlyExpensesViewModel = monthlyExpensesViewModel;
            PredictionsViewModel = predictionsViewModel;
            TransactionModalViewModel = transactionModalViewModel;
            SubscriptionModalViewModel = subscriptionModalViewModel;
        }
    }
}
