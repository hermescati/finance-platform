using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Factories;
using Expensier.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expensier.Domain.Services.Transactions;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Authenticators;
using Expensier.WPF.ViewModels.Expenses;
using Expensier.WPF.State.Expenses;
using Expensier.WPF.ViewModels.Subscriptions;
using Expensier.WPF.State.Subscriptions;
using Expensier.WPF.ViewModels.Modals;
using Expensier.Domain.Services.Subscriptions;
using Expensier.WPF.Controls.Modals;
using Expensier.WPF.ViewModels.Charts;

namespace Expensier.WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtension
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MainViewModel>();
                services.AddSingleton(CreateDashboardViewModel);
                services.AddSingleton<RecentExpensesViewModel>();
                services.AddSingleton<TopSubscriptionsViewModel>();
                services.AddTransient<SpendingSummaryViewModel>();
                services.AddTransient<ExpenditureAllocationViewModel>();
                services.AddSingleton(CreateExpensesViewModel);
                services.AddTransient<TransactionModalViewModel>();
                services.AddTransient<SubscriptionModalViewModel>();
                services.AddTransient(CreateTransactionModalViewModel);
                services.AddSingleton(CreateSubscriptionModalViewModel);
                services.AddSingleton(CreateWalletViewModel);

                services.AddSingleton<IExpensierViewModelFactory, ExpensierViewModelFactory>();

                services.AddSingleton<DelegateRenavigator<DashboardViewModel>>();
                services.AddSingleton<DelegateRenavigator<ExpensesViewModel>>();
                services.AddSingleton<DelegateRenavigator<RegisterViewModel>>();
                services.AddSingleton<DelegateRenavigator<LoginViewModel>>();

                services.AddSingleton<CreateViewModel<DashboardViewModel>>(services => () => CreateDashboardViewModel(services));
                services.AddSingleton<CreateViewModel<ExpensesViewModel>>(services => () => CreateExpensesViewModel(services));
                services.AddSingleton<CreateViewModel<WalletViewModel>>(services => () => CreateWalletViewModel(services));
                services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));
                services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));
            });

            return host;
        }


        private static DashboardViewModel CreateDashboardViewModel(IServiceProvider services)
        {
            return new DashboardViewModel(
                services.GetRequiredService<RecentExpensesViewModel>(),
                services.GetRequiredService<TopSubscriptionsViewModel>(),
                services.GetRequiredService<SpendingSummaryViewModel>(),
                services.GetRequiredService<ExpenditureAllocationViewModel>());
        }

        private static ExpensesViewModel CreateExpensesViewModel(IServiceProvider services)
        {
            return new ExpensesViewModel(
                services.GetRequiredService<TransactionStore>(),
                services.GetRequiredService<SubscriptionStore>(),
                services.GetRequiredService<TransactionModalViewModel>(),
                services.GetRequiredService<SubscriptionModalViewModel>());
        }

        private static WalletViewModel CreateWalletViewModel(IServiceProvider services)
        {
            return new WalletViewModel();
        }

        private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
        {
            return new LoginViewModel(
                services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<DelegateRenavigator<DashboardViewModel>>(),
                services.GetRequiredService<DelegateRenavigator<RegisterViewModel>>());
        }

        private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
        {
            return new RegisterViewModel(
                services.GetRequiredService<DelegateRenavigator<LoginViewModel>>(),
                services.GetRequiredService<DelegateRenavigator<LoginViewModel>>(),
                services.GetRequiredService<IAuthenticator>());
        }

        private static TransactionModalViewModel CreateTransactionModalViewModel(IServiceProvider services)
        {
            return new TransactionModalViewModel(
                services.GetRequiredService<ITransactionService>(),
                services.GetRequiredService<AccountStore>(),
                services.GetRequiredService<DelegateRenavigator<ExpensesViewModel>>());
        }

        private static SubscriptionModalViewModel CreateSubscriptionModalViewModel(IServiceProvider services)
        {
            return new SubscriptionModalViewModel(
                services.GetRequiredService<ISubscriptionService>(),
                services.GetRequiredService<AccountStore>(),
                services.GetRequiredService<DelegateRenavigator<ExpensesViewModel>>());
        }
    }
}
