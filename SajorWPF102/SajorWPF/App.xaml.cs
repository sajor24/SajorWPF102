using System;
using System.Windows;
using Domain.Commands;
using Domain.Queries;
using Framework;
using Framework.Commands;
using Framework.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SajorWPF.Services;
using SajorWPF.Stores;
using SajorWPF.ViewModels;
using SajorWPF.Views;

namespace SajorWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Database Auto Setup
                    services.AddSingleton<DatabaseAutoSetup>();

                    // Repository
                    services.AddSingleton<Repository>();

                    // Domain Commands and Queries
                    services.AddSingleton<ICreateEmployee, CreateEmployee>();
                    services.AddSingleton<IGetAllEmployees, GetAllEmployee>();
                    services.AddSingleton<IUpdateEmployee, UpdateEmployee>();
                    services.AddSingleton<IDeleteEmployee, DeleteEmployee>();
                    services.AddSingleton<IReadEmployeeById, ReadEmployeeById>();

                    // Navigation Store
                    services.AddSingleton<NavigationStore>();

                    // Navigation Services
                    services.AddSingleton<INavigationService>(services => CreateHomeNavigationService(services));

                    // ViewModels
                    services.AddTransient<HomeViewModel>(services => CreateHomeViewModel(services));
                    services.AddTransient<AddEmployeeViewModel>(services => CreateAddEmployeeViewModel(services));
                    services.AddTransient<MainViewModel>(services => CreateMainViewModel(services));

                    // Main View
                    services.AddSingleton<MainView>(services => new MainView()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        private MainViewModel CreateMainViewModel(IServiceProvider services)
        {
            return new MainViewModel(
                navigationStore: services.GetRequiredService<NavigationStore>());
        }

        private INavigationService CreateHomeNavigationService(IServiceProvider services)
        {
            return new NavigationService<HomeViewModel>(
                navigationStore: services.GetRequiredService<NavigationStore>(),
                createViewModel: () => services.GetRequiredService<HomeViewModel>());
        }

        private INavigationService CreateAddEmployeeNavigationService(IServiceProvider services)
        {
            return new NavigationService<AddEmployeeViewModel>(
                navigationStore: services.GetRequiredService<NavigationStore>(),
                createViewModel: () => services.GetRequiredService<AddEmployeeViewModel>());
        }

        private HomeViewModel CreateHomeViewModel(IServiceProvider services)
        {
            return new HomeViewModel(
                addEmployeeNavigationService: CreateAddEmployeeNavigationService(services));
        }

        private AddEmployeeViewModel CreateAddEmployeeViewModel(IServiceProvider services)
        {
            return new AddEmployeeViewModel(
                createEmployee: services.GetRequiredService<ICreateEmployee>(),
                getAllEmployees: services.GetRequiredService<IGetAllEmployees>(),
                updateEmployee: services.GetRequiredService<IUpdateEmployee>(),
                deleteEmployee: services.GetRequiredService<IDeleteEmployee>(),
                homeNavigationService: CreateHomeNavigationService(services));
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            // AUTOMATIC DATABASE SETUP - No manual update needed!
            try
            {
                var dbSetup = _host.Services.GetRequiredService<DatabaseAutoSetup>();
                await dbSetup.EnsureDatabaseSetupAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database setup error: {ex.Message}\n\nPlease check your connection string.", 
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
                return;
            }

            var navigationService = _host.Services.GetRequiredService<INavigationService>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainView>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
