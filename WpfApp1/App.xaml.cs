using System;
using System.Data;
using System.Configuration;
using System.Windows;
using WpfApp1.Views;
using WpfApp1.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        //注入するserviceの登録
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MenuWindow>();
            services.AddTransient<MenuViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mw = _serviceProvider.GetService<MenuWindow>();
            mw!.Show();
        }
    }
}
