using Assignment2.View.Admin.Pages;
using Assignment2.View.CustomerView.Pages;
using BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assignment2.View.CustomerView {
    /// <summary>
    /// Interaction logic for CustomerMainWindow.xaml
    /// </summary>
    public partial class CustomerMainWindow: Window, INotifyPropertyChanged {
        private readonly IServiceProvider _serviceProvider;
        private readonly int _customerId;
        private string _customerName;

        public event PropertyChangedEventHandler PropertyChanged;

        public string CustomerName {
            get => _customerName;
            set {
                _customerName = value;
                OnPropertyChanged();
            }
        }

        private async void LoadCustomerDetails() {
            try {
                var customerService = _serviceProvider.GetRequiredService<ICustomerService>();
                var customer = await customerService.GetCustomerById(_customerId);
                if (customer != null) {
                    CustomerName = customer.CustomerFullName;
                } else {
                    CustomerName = "Customer not found";
                }
            } catch (Exception ex) {
                // Handle the error appropriately (e.g., log it and show a message to the user)
                CustomerName = $"Error: {ex.Message}";
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public CustomerMainWindow(IServiceProvider serviceProvider, int customerId) {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _customerId = customerId;

            // Set DataContext for data binding
            DataContext = this;

            // Load customer details (simulated for this example)
            LoadCustomerDetails();
        }

        private void Close_Admin_Window(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Minimize_Admin_Window(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void ViewRoomButton_Click(object sender, RoutedEventArgs e) {
            var roomPage = new RoomsAvailable(
                _serviceProvider.GetRequiredService<IRoomService>(),
                _serviceProvider.GetRequiredService<IBookingService>(),
                _customerId);
            MainContentControl.Content = roomPage;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e) {
            var bookingManagePage = _serviceProvider.GetRequiredService<BookingManagePage>();
            MainContentControl.Content = bookingManagePage;
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e) {
            var historyPage = new BookingHistory(
                _serviceProvider.GetRequiredService<IBookingService>(),
                _customerId);
            MainContentControl.Content = historyPage;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
            var loginWindow = new LoginWindow(_serviceProvider.GetRequiredService<IAuthenticationService>());
        }
    }
}
