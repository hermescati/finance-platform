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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(c =>
                {
                    c.AddJsonFile("appsettings.json");
                    c.AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    string apiKey = context.Configuration.GetValue<string>("API_KEY");
                    services.AddSingleton<APIClientFactory>(new APIClientFactory(apiKey));

                    string connectionString = context.Configuration.GetConnectionString("default");
                    services.AddDbContext<ExpensierDbContext>(d => d.UseSqlServer(connectionString));
                    services.AddSingleton<ExpensierDbContextFactory>(new ExpensierDbContextFactory(connectionString));
                    services.AddSingleton<IAuthenticationService, AuthenticationService>();
                    services.AddSingleton<IDataService<Account>, AccountDataService>();
                    services.AddSingleton<IAccountService, AccountDataService>();
                    services.AddSingleton<ICryptoService, CryptoService>();

                    services.AddSingleton<IPasswordHasher, PasswordHasher>();

                    services.AddSingleton<IExpensierViewModelFactory, ExpensierViewModelFactory>();
                    services.AddSingleton<DashboardViewModel>();
                    services.AddSingleton<ExpensesViewModel>(services => new ExpensesViewModel(
                        services.GetRequiredService<TransactionViewModel>()));
                    services.AddSingleton<TransactionViewModel>();
                    services.AddSingleton<WalletViewModel>();
                    services.AddSingleton<DelegateRenavigator<DashboardViewModel>>();
                    services.AddSingleton<DelegateRenavigator<RegisterViewModel>>();

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

                    services.AddSingleton<DelegateRenavigator<LoginViewModel>>();
                    services.AddSingleton<CreateViewModel<RegisterViewModel>>(services =>
                    {
                        return () => new RegisterViewModel(
                            services.GetRequiredService<DelegateRenavigator<LoginViewModel>>(),
                            services.GetRequiredService<DelegateRenavigator<LoginViewModel>>(),
                            services.GetRequiredService<IAuthenticator>()
                        );
                    });

                    services.AddSingleton<CreateViewModel<LoginViewModel>>(services =>
                    {
                        return () => new LoginViewModel(
                            services.GetRequiredService<IAuthenticator>(),
                            services.GetRequiredService<DelegateRenavigator<DashboardViewModel>>(),
                            services.GetRequiredService<DelegateRenavigator<RegisterViewModel>>());
                    });

                    services.AddSingleton<INavigator, Navigator>();
                    services.AddSingleton<IAuthenticator, Authenticator>();
                    services.AddSingleton<AccountStore, AccountStore>();
                    services.AddSingleton<TransactionStore, TransactionStore>();
                    services.AddScoped<MainViewModel>();

                    services.AddScoped<MainView>(s => new MainView(s.GetRequiredService<MainViewModel>()));
                });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            Window window = _host.Services.GetRequiredService<MainView>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
