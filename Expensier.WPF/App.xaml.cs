using Expensier.API;
using Expensier.API.Services;
using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.Domain.Services.Authentication;
using Expensier.Doman.Services;
using Expensier.EntityFramework;
using Expensier.EntityFramework.Services;
using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Authenticators;
using Expensier.WPF.State.Expenses;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels;
using Expensier.WPF.ViewModels.Factories;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Expensier.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();
            IAuthenticationService authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();

            Window window = serviceProvider.GetRequiredService<MainView>();
            window.Show();

            //authenticationService.userRegister(
            //    firstName: "Hermes", 
            //    lastName: "Cati",
            //    email: "hermescati99@gmail.com",
            //    password: "HermesParidi1127",
            //    confirmPassword: "HermesParidi1127"
            //);

            authenticationService.userLogin(
                email: "hermescati99@gmail.com",
                password: "HermeParidi1127"
            );

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<ExpensierDbContextFactory>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<IAccountService, AccountDataService>();
            services.AddSingleton<ICryptoService, CryptoService>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            
            string apiKey = ConfigurationManager.AppSettings.Get("ApiKey");
            services.AddSingleton<APIClientFactory>(new APIClientFactory(apiKey)); 

            services.AddSingleton<IExpensierViewModelFactory, ExpensierViewModelFactory>();
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<ExpensesViewModel>(services => new ExpensesViewModel(
                services.GetRequiredService<TransactionViewModel>()));
            services.AddSingleton<TransactionViewModel>();
            services.AddSingleton<WalletViewModel>();
            services.AddSingleton<DelegateRenavigator<DashboardViewModel>>();

            services.AddSingleton<CreateViewModel<DashboardViewModel>>(services =>
            {
                return () => services.GetRequiredService<DashboardViewModel>();
            });

            services.AddSingleton<CreateViewModel<ExpensesViewModel>>(services =>
            {
                return () => services.GetRequiredService<ExpensesViewModel>();
            });

            services.AddSingleton<CreateViewModel<WalletViewModel>>(services =>
            {
                return () => services.GetRequiredService<WalletViewModel>();
            });

            services.AddSingleton<CreateViewModel<LoginViewModel>>(services =>
            {
                return () => new LoginViewModel(
                    services.GetRequiredService<IAuthenticator>(),
                    services.GetRequiredService<DelegateRenavigator<DashboardViewModel>>());
            });

            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<AccountStore, AccountStore>();
            services.AddSingleton<TransactionStore, TransactionStore>();
            services.AddScoped<MainViewModel>();

            services.AddScoped<MainView>(s => new MainView(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}
