using Expensier.EntityFramework;
using Expensier.WPF.HostBuilders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                .AddConfiguration()
                .AddAPI()
                .AddDbContext()
                .AddServices()
                .AddStores()
                .AddViewModels()
                .AddViews();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            ExpensierDbContextFactory contextFactory = _host.Services.GetRequiredService<ExpensierDbContextFactory>();
            using(ExpensierDbContext context = contextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }

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
