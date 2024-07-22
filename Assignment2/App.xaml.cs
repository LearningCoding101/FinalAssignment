using Assignment2.Services;
using Assignment2.View.Authentication;
using BusinessLayer.Services;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Assignment2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            }).ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnectionStringDB");
                services.AddDbContext<FuminiHotelManagementContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

                services.AddTransient<AuthenticationRepository>();
                services.AddTransient<BookingDetailRepository>();
                services.AddTransient<BookingReservationRepository>();
                services.AddTransient<CustomerRepository>();
                services.AddTransient<RoomRepository>();
                services.AddTransient<RoomTypeRepository>();

                services.AddTransient<AuthenticationService>();
                services.AddTransient<CustomerService>();
                services.AddTransient<BookingService>();
                services.AddTransient<RoomService>();

                services.AddTransient<LoginWindow>();
                services.AddTransient<RegisterWindow>();

                services.AddSingleton<Frame>(provider =>
                {
                    var mainWindow = Application.Current.MainWindow as MainWindow;
                    return mainWindow?.MainFrame!;
                });

                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<IWindowNavigationService, WindowNavigationService>();
            }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            var loginWindow = _host.Services.GetRequiredService<LoginWindow>();
            loginWindow.Show();
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
