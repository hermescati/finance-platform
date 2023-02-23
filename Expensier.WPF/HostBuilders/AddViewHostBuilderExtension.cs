using Expensier.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.HostBuilders
{
    public static class AddViewHostBuilderExtension
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton(services => new MainView(services.GetRequiredService<MainViewModel>()));
            });

            return host;
        }
    }
}
