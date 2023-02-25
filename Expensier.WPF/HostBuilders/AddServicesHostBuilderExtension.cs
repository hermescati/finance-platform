using Expensier.API.Services;
using Expensier.Domain.Models;
using Expensier.Domain.Services.Authentication;
using Expensier.Domain.Services.Transactions;
using Expensier.Domain.Services;
using Expensier.Doman.Services;
using Expensier.EntityFramework.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Expensier.Domain.Services.Subscriptions;

namespace Expensier.WPF.HostBuilders
{
    public static class AddServicesHostBuilderExtension
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IAuthenticationService, AuthenticationService>();
                services.AddSingleton<IDataService<Account>, AccountDataService>();
                services.AddSingleton<IAccountService, AccountDataService>();
                services.AddSingleton<ITransactionService, TransactionService>();
                services.AddSingleton<ISubscriptionService, SubscriptionService>();
                services.AddSingleton<ICryptoService, CryptoService>();
                services.AddSingleton<IPasswordHasher, PasswordHasher>();
            });

            return host;
        }
    }
}
