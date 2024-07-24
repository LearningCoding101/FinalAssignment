using Assignment2.View.Admin;
using BusinessLayer.Services;
using DataAccessLayer.Models;
using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assignment2 {
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow: Window {

        private readonly IAuthenticationService _authenticationService;

        public delegate void LoginSuccessHandler(string role, int id);

        // Define an event based on the delegate
        public event LoginSuccessHandler LoginSuccessEvent;

        public LoginWindow(IAuthenticationService authenticationService) {
            InitializeComponent();
            _authenticationService = authenticationService;
        }

        private void Close_Login_Window(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e) {
            errormessage.Text = string.Empty;

            if (textBoxEmail.Text.Length == 0) {
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            } else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$")) {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            } else {
                try {
                    var customer = await _authenticationService.Login(textBoxEmail.Text, passwordBox.Password);
                    if (customer != null) {
                        string role = _authenticationService.GetUserRole(customer.EmailAddress);
                        int id = customer.CustomerId;
                        LoginSuccessEvent?.Invoke(role, id);
                        this.Hide();
                    } else {
                        errormessage.Text = "Please enter existing email/password !!";
                    }
                } catch (Exception ex) {
                    errormessage.Text = $"An error occurred: {ex.Message}";
                }

            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
        }

    }
}
