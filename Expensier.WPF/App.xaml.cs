using Expensier.API;
using Expensier.API.Services;
using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.Domain.Services.Authentication;
using Expensier.Doman.Services;
using Expensier.EntityFramework;
using Expensier.EntityFramework.Services;
using Expensier.WPF.State.Authenticators;
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

            services.AddSingleton<IExpensierViewModelAbstractFactory, ExpensierViewModelAbstractFactory>();

            services.AddSingleton<IExpensierViewModelFactory<LoginViewModel>>((services) =>
                new LoginViewModelFactory(services.GetRequiredService<IAuthenticator>(), 
                new ViewModelFactoryRenavigator<DashboardViewModel>(services.GetRequiredService<INavigator>(),
                services.GetRequiredService<IExpensierViewModelFactory<DashboardViewModel>>())));

            services.AddSingleton<IExpensierViewModelFactory<DashboardViewModel>, DashboardViewModelFactory>();
            services.AddSingleton<IExpensierViewModelFactory<ExpensesViewModel>, ExpensesViewModelFactory>();
            services.AddSingleton<IExpensierViewModelFactory<WalletViewModel>, WalletViewModelFactory>();

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<IAuthenticator, Authenticator>();
            services.AddScoped<MainViewModel>();

            services.AddScoped<MainView>(s => new MainView(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}
