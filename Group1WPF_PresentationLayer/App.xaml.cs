﻿using Assignment2.Factory;
using Assignment2.Services;
using Assignment2.View.Admin;
using Assignment2.View.Admin.Pages;
using Assignment2.View.CustomerView;
using Assignment2.View.CustomerView.Pages;
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

namespace Assignment2 {
    public partial class App: Application {
        private readonly IHost _host;

        public App() {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) => {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) => {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnectionStringDB");
                    services.AddDbContext<FuminiHotelManagementContext>(options => {
                        options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure());
                    });

                    RegisterRepositories(services);
                    RegisterServices(services);
                    RegisterViews(services);

                    services.AddSingleton<Frame>(provider => {
                        var mainWindow = Application.Current.MainWindow as MainWindow;
                        return mainWindow?.MainFrame!;
                    });

                    services.AddSingleton<INavigationService, NavigationService>();
                    services.AddSingleton<IWindowNavigationService, WindowNavigationService>();
                    // Register factory delegate
                    services.AddTransient<CustomerMainWindowFactory>(provider =>
                        (serviceProvider, customerId) => new CustomerMainWindow(serviceProvider, customerId));
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e) {
            await _host.StartAsync();
            var loginWindow = _host.Services.GetRequiredService<LoginWindow>();
            var adminWindow = _host.Services.GetRequiredService<AdminWindow>();

            loginWindow.LoginSuccessEvent += OnLoginSuccessEvent;
            loginWindow.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e) {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }

        private void OnLoginSuccessEvent(string role, int id) {
            Window window = null;

            if (role == "ADMIN") {
                window = _host.Services.GetRequiredService<AdminWindow>();
            } else if (role == "USER") {
                var factory = _host.Services.GetRequiredService<CustomerMainWindowFactory>();
                window = factory(_host.Services, id);
            }

            if (window != null) {
                window.ShowDialog();
            }
        }

        private void RegisterRepositories(IServiceCollection services) {
            services.AddTransient<AuthenticationRepository>();
            services.AddTransient<BookingDetailRepository>();
            services.AddTransient<BookingReservationRepository>();
            services.AddTransient<CustomerRepository>();
            services.AddTransient<RoomRepository>();
            services.AddTransient<RoomTypeRepository>();
        }

        private void RegisterServices(IServiceCollection services) {
            services.AddTransient<AuthenticationService>();
            services.AddTransient<CustomerService>();
            services.AddTransient<BookingService>();
            services.AddTransient<RoomService>();

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IBookingService, BookingService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
        }

        private void RegisterViews(IServiceCollection services) {
            services.AddTransient<LoginWindow>();
            services.AddTransient<AdminWindow>();
            services.AddTransient<CustomerManagePage>();
            services.AddTransient<RoomManagePage>();
            services.AddTransient<CustomerMainWindow>();
            services.AddTransient<RoomDialog>();
            services.AddTransient<BookingManagePage>();
            services.AddTransient<ReservationManage>();
            services.AddTransient<RoomsAvailable>();
            services.AddTransient<BookingDialog>();
            services.AddTransient<BookingHistory>();
        }
    }
}