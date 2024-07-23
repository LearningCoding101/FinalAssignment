using Assignment2.View.Admin.Pages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Assignment2.View.Admin {
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow: Window {
        private readonly IServiceProvider _serviceProvider;

        public AdminWindow(IServiceProvider serviceProvider) {
            InitializeComponent();
            _serviceProvider = serviceProvider;
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

        private void CustomerButton_Click(object sender, RoutedEventArgs e) {
            var customerManagePage = _serviceProvider.GetRequiredService<CustomerManagePage>();
            MainContentControl.Content = customerManagePage;
        }

        private void RoomButton_Click(object sender, RoutedEventArgs e) {
            var roomManagePage = _serviceProvider.GetRequiredService<RoomManagePage>();
            MainContentControl.Content = roomManagePage;
        }

        private void BookingButton_Click(object sender, RoutedEventArgs e) {
            MainContentControl.Content = new BookingManagePage();
        }
    }
}
