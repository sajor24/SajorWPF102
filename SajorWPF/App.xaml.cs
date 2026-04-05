using System.Windows;
using Domain.Commands;
using Domain.Queries;
using Framework.Commands;
using Framework.Queries;
using Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Interfaces;
using SajorWPF.Services;
using SajorWPF.Stores;
using SajorWPF.ViewModels;
using SajorWPF.Views;

namespace SajorWPF
{
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider = null!;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            services.AddSingleton<IConfiguration>(config);

            // Repository & DB setup
            services.AddSingleton<IRepository, Repository.Repository>();
            services.AddSingleton<DatabaseAutoSetup>();

            // Domain commands & queries
            services.AddTransient<ICreateEmployee, CreateEmployee>();
            services.AddTransient<IUpdateEmployee, UpdateEmployee>();
            services.AddTransient<IDeleteEmployee, DeleteEmployee>();
            services.AddTransient<IGetAllEmployees, GetAllEmployee>();
            services.AddTransient<IReadEmployeeById, ReadEmployeeById>();

            // Navigation
            services.AddSingleton<NavigationStore>();

            services.AddTransient<HomeViewModel>(sp => new HomeViewModel(
                new NavigationService<AddEmployeeViewModel>(
                    sp.GetRequiredService<NavigationStore>(),
                    () => sp.GetRequiredService<AddEmployeeViewModel>()
                )
            ));

            services.AddTransient<AddEmployeeViewModel>(sp => new AddEmployeeViewModel(
                sp.GetRequiredService<ICreateEmployee>(),
                sp.GetRequiredService<IGetAllEmployees>(),
                sp.GetRequiredService<IUpdateEmployee>(),
                sp.GetRequiredService<IDeleteEmployee>(),
                new NavigationService<HomeViewModel>(
                    sp.GetRequiredService<NavigationStore>(),
                    () => sp.GetRequiredService<HomeViewModel>()
                )
            ));

            services.AddSingleton<MainViewModel>(sp => new MainViewModel(
                sp.GetRequiredService<NavigationStore>()
            ));

            services.AddSingleton<MainView>(sp =>
            {
                var window = new MainView();
                window.DataContext = sp.GetRequiredService<MainViewModel>();
                return window;
            });

            _serviceProvider = services.BuildServiceProvider();

            // Show window immediately
            var navStore = _serviceProvider.GetRequiredService<NavigationStore>();
            navStore.CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();

            var mainWindow = _serviceProvider.GetRequiredService<MainView>();
            mainWindow.Show();

            // Run DB setup in background, show error if it fails
            var dbSetup = _serviceProvider.GetRequiredService<DatabaseAutoSetup>();
            _ = dbSetup.EnsureDatabaseSetupAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        System.Windows.MessageBox.Show(
                            $"Database setup failed:\n{t.Exception?.InnerException?.Message ?? t.Exception?.Message}",
                            "DB Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    });
                }
            });
        }
    }
}
