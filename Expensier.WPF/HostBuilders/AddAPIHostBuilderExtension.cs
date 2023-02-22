using Expensier.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.HostBuilders
{
    public static class AddAPIHostBuilderExtension
    {
        public static IHostBuilder AddAPI(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                string apiKey = context.Configuration.GetValue<string>("API_KEY");
                services.AddSingleton<APIClientFactory>(new APIClientFactory(apiKey));
            });

            return host;
        }
    }
}
