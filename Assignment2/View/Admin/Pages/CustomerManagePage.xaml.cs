using BusinessLayer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccessLayer.Models;

namespace Assignment2.View.Admin.Pages {
    /// <summary>
    /// Interaction logic for CustomerManagePage.xaml
    /// </summary>
    public partial class CustomerManagePage: UserControl {
        private readonly ICustomerService _customerService;
        public ObservableCollection<Customer> Customers { get; set; }

        public CustomerManagePage(ICustomerService customerService) {
            InitializeComponent();
            _customerService = customerService;
            Customers = new ObservableCollection<Customer>();
            DataContext = this;

            LoadCustomers();
            CustomerDataGrid.ItemsSource = Customers;
        }

        private async void LoadCustomers() {
            Customers.Clear();
            var customers = await _customerService.GetAllCustomers();
            foreach (var customer in customers) {
                Customers.Add(customer);
            }
        }
        private async void EnableButton_Click(object sender, RoutedEventArgs e) {
            if (CustomerDataGrid.SelectedItem is Customer selectedCustomer) {
                selectedCustomer.CustomerStatus = 1;
                await _customerService.UpdateCustomer(selectedCustomer);
                LoadCustomers();
            } else {
                MessageBox.Show("Please select a customer to enable.");
            }
        }

        private async void DisableButton_Click(object sender, RoutedEventArgs e) {
            if (CustomerDataGrid.SelectedItem is Customer selectedCustomer) {
                selectedCustomer.CustomerStatus = 0;
                await _customerService.UpdateCustomer(selectedCustomer);
                LoadCustomers();
            } else {
                MessageBox.Show("Please select a customer to disable.");
            }
        }
    }
}
