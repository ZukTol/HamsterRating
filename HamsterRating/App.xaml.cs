using System.ComponentModel;
using System.Windows;
using HamsterRating.Commands;
using HamsterRating.Core.Data;
using HamsterRating.Options;
using HamsterRating.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HamsterRating
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            var services = new ServiceCollection();

            // add your services
            services.AddSingleton<IPowerUpRepository, PowerUpRepository>();
            services.Configure<FileStorage>(config.GetSection(nameof(FileStorage)));
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<UsePowerUpCommand>();
            

            // build service provider
            var serviceProvider = services.BuildServiceProvider();
            ServiceProvider = serviceProvider;
            DISource.Resolver = serviceProvider.GetService;
            
        }
    }
}
