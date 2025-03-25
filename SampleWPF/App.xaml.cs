using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SampleWPF.Services;
using SampleWPF.ViewModels;

namespace SampleWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITaskService, TaskService>();
            services.AddSingleton<TaskViewModel>();
            services.AddSingleton<Views.MainWindow>();        // MainWindow を DI に登録
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetRequiredService<Views.MainWindow>();
            mainWindow.Show();
        }
    }
}
