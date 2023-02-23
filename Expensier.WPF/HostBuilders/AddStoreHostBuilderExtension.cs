using Expensier.WPF.State.Accounts;
using Expensier.WPF.State.Authenticators;
using Expensier.WPF.State.Expenses;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.State;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expensier.WPF.ViewModels;

namespace Expensier.WPF.HostBuilders
{
    public static class AddStoreHostBuilderExtension
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<IAuthenticator, Authenticator>();
                services.AddSingleton<IRenavigator, DelegateRenavigator<LoginViewModel>>();
                services.AddSingleton<AccountStore, AccountStore>();
                services.AddSingleton<TransactionStore, TransactionStore>();
                services.AddSingleton<SubscriptionStore, SubscriptionStore>();
            });

            return host;
        }
    }
}
