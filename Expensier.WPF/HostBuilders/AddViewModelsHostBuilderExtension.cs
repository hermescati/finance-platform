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
using Expensier.WPF.ViewModels;
using Expensier.Domain.Services.Authentication;
using Expensier.Domain.Services;
using Expensier.WPF.State.Crypto;
using Expensier.WPF.ViewModels.Cryptos;

namespace Expensier.WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtension
    {
        public static IHostBuilder AddViewModels( this IHostBuilder host )
        {
            host.ConfigureServices( services =>
            {
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<SidePanelViewModel>();
                services.AddSingleton( CreateDashboardViewModel );
                services.AddTransient<RecentExpensesViewModel>();
                services.AddTransient<TopSubscriptionsViewModel>();
                services.AddTransient<SpendingSummaryViewModel>();
                services.AddTransient<ExpenditureAllocationViewModel>();
                services.AddTransient<MonthlyExpensesViewModel>();
                services.AddTransient<PredictionsViewModel>();
                services.AddSingleton<TopPerformingCryptosViewModel>();
                services.AddSingleton( CreateExpensesViewModel );
                services.AddTransient( CreateTransactionModalViewModel );
                services.AddTransient( CreateSubscriptionModalViewModel );
                services.AddTransient( CreateCryptoModalViewModel );
                services.AddTransient( CreateExportModalViewModel );
                services.AddSingleton( CreateWalletViewModel );
                services.AddSingleton<CryptoWatchlistViewModel>();
                services.AddTransient<PortfolioValueViewModel>();
                services.AddTransient<AssetAllocationViewModel>();
                services.AddTransient<PortfolioPerformanceViewModel>();

                services.AddSingleton<IExpensierViewModelFactory, ExpensierViewModelFactory>();

                services.AddSingleton<DelegateRenavigator<DashboardViewModel>>();
                services.AddSingleton<DelegateRenavigator<ExpensesViewModel>>();
                services.AddSingleton<DelegateRenavigator<WalletViewModel>>();
                services.AddSingleton<DelegateRenavigator<RegisterViewModel>>();
                services.AddSingleton<DelegateRenavigator<LoginViewModel>>();
                services.AddSingleton<DelegateRenavigator<ResetPasswordViewModel>>();

                services.AddSingleton<CreateViewModel<DashboardViewModel>>( services => () => CreateDashboardViewModel( services ) );
                services.AddSingleton<CreateViewModel<ExpensesViewModel>>( services => () => CreateExpensesViewModel( services ) );
                services.AddSingleton<CreateViewModel<WalletViewModel>>( services => () => CreateWalletViewModel( services ) );
                services.AddSingleton<CreateViewModel<RegisterViewModel>>( services => () => CreateRegisterViewModel( services ) );
                services.AddSingleton<CreateViewModel<LoginViewModel>>( services => () => CreateLoginViewModel( services ) );
                services.AddSingleton<CreateViewModel<ResetPasswordViewModel>>( services => () => CreateForgotPasswordViewModel( services ) );
            } );

            return host;
        }


        private static DashboardViewModel CreateDashboardViewModel( IServiceProvider services )
        {
            return new DashboardViewModel(
                services.GetRequiredService<RecentExpensesViewModel>(),
                services.GetRequiredService<TopSubscriptionsViewModel>(),
                services.GetRequiredService<TopPerformingCryptosViewModel>(),
                services.GetRequiredService<SpendingSummaryViewModel>(),
                services.GetRequiredService<ExpenditureAllocationViewModel>(),
                services.GetRequiredService<DelegateRenavigator<WalletViewModel>>() );
        }

        private static ExpensesViewModel CreateExpensesViewModel( IServiceProvider services )
        {
            return new ExpensesViewModel(
                services.GetRequiredService<TransactionStore>(),
                services.GetRequiredService<SubscriptionStore>(),
                services.GetRequiredService<TransactionModalViewModel>(),
                services.GetRequiredService<SubscriptionModalViewModel>(),
                services.GetRequiredService<ExportModalViewModel>(),
                services.GetRequiredService<MonthlyExpensesViewModel>(),
                //services.GetRequiredService<PredictionsViewModel>(),
                services.GetRequiredService<ITransactionService>(),
                services.GetRequiredService<ISubscriptionService>(),
                services.GetRequiredService<AccountStore>(),
                services.GetRequiredService<DelegateRenavigator<ExpensesViewModel>>() );
        }

        private static WalletViewModel CreateWalletViewModel( IServiceProvider services )
        {
            return new WalletViewModel(
                services.GetRequiredService<CryptoWatchlistViewModel>(),
                services.GetRequiredService<PortfolioValueViewModel>(),
                services.GetRequiredService<AssetAllocationViewModel>(),
                services.GetRequiredService<PortfolioPerformanceViewModel>(),
                services.GetRequiredService<CryptoStore>(),
                services.GetRequiredService<CryptoModalViewModel>(),
                services.GetRequiredService<ICryptoService>(),
                services.GetRequiredService<AccountStore>(),
                services.GetRequiredService<DelegateRenavigator<WalletViewModel>>() );
        }

        private static LoginViewModel CreateLoginViewModel( IServiceProvider services )
        {
            return new LoginViewModel(
                services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<DelegateRenavigator<DashboardViewModel>>(),
                services.GetRequiredService<DelegateRenavigator<RegisterViewModel>>(),
                services.GetRequiredService<DelegateRenavigator<ResetPasswordViewModel>>(),
                services.GetRequiredService<SidePanelViewModel>() );
        }

        private static RegisterViewModel CreateRegisterViewModel( IServiceProvider services )
        {
            return new RegisterViewModel(
                services.GetRequiredService<DelegateRenavigator<LoginViewModel>>(),
                services.GetRequiredService<IAuthenticator>() );
        }

        private static ResetPasswordViewModel CreateForgotPasswordViewModel( IServiceProvider services )
        {
            return new ResetPasswordViewModel(
                services.GetRequiredService<IAuthenticationService>(),
                services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<DelegateRenavigator<LoginViewModel>>() );
        }

        private static TransactionModalViewModel CreateTransactionModalViewModel( IServiceProvider services )
        {
            return new TransactionModalViewModel(
                services.GetRequiredService<ITransactionService>(),
                services.GetRequiredService<AccountStore>(),
                services.GetRequiredService<DelegateRenavigator<ExpensesViewModel>>() );
        }

        private static SubscriptionModalViewModel CreateSubscriptionModalViewModel( IServiceProvider services )
        {
            return new SubscriptionModalViewModel(
                services.GetRequiredService<ISubscriptionService>(),
                services.GetRequiredService<AccountStore>(),
                services.GetRequiredService<DelegateRenavigator<ExpensesViewModel>>() );
        }

        private static CryptoModalViewModel CreateCryptoModalViewModel( IServiceProvider services )
        {
            return new CryptoModalViewModel(
                services.GetRequiredService<ICryptoService>(),
                services.GetRequiredService<AccountStore>(),
                services.GetRequiredService<DelegateRenavigator<WalletViewModel>>() );
        }

        private static ExportModalViewModel CreateExportModalViewModel (IServiceProvider services )
        {
            return new ExportModalViewModel(
                services.GetRequiredService<ITransactionService>(),
                services.GetRequiredService<AccountStore>(),
                services.GetRequiredService<DelegateRenavigator<ExpensesViewModel>>() );
        }
    }
}
