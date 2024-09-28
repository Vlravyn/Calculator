using Calculator.Core;
using Calculator.WPF.Dialogs;
using Calculator.WPF.Services;
using Calculator.WPF.ViewModels;
using Calculator.WPF.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Windows;

namespace Calculator.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new static App Current = (App)Application.Current;


        private static readonly IHost _host =
            Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(c =>
            {
                c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location));
            })
            .ConfigureAppConfiguration(d1 =>
            {

            })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<ApplicationHostService>();
                services.AddSingleton<IDialogService, DialogService>();
                services.AddSingleton<History>();
                services.AddSingleton<Calculate>();
                services.AddSingleton<HistoryViewModel>();
                services.AddSingleton<StandardCalcViewModel>();
                services.AddSingleton<StandardCalcView>();
                services.AddKeyedSingleton<IDialogWindow, HistoryView>("historyWindow");
            })
            .Build();
        public App()
        {
        }

        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T GetService<T>() where T : class
        {
            return _host.Services.GetService(typeof(T)) as T;
        }

        /// <summary>
        /// Gets a registered service with the given key
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <param name="key">the key for this service</param>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T GetKeyedService<T>(object? key) where T : class
        {
            return _host.Services.GetKeyedService<T>(key);
        }

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();
            await GetService<ApplicationHostService>().StartAsync(new CancellationToken());
        }
    }

}
