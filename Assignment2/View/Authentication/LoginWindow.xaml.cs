using Assignment2.Services;
using BusinessLayer.Services;
using System.Text.RegularExpressions;
using System.Windows;

namespace Assignment2.View.Authentication
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IWindowNavigationService _windowNavigationService;

        public LoginWindow()
        {
            InitializeComponent();
            _windowNavigationService = new WindowNavigationService();
        }

        public LoginWindow(AuthenticationService authenticationService, IWindowNavigationService windowNavigationService) : this()
        {
            _authenticationService = authenticationService;
            _windowNavigationService = windowNavigationService;
        }

        private async void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                var user = await _authenticationService.Login(textBoxEmail.Text, passwordBox1.Password);
                if (user != null)
                {
                    string role = _authenticationService.GetUserRole(user.EmailAddress);
                    
                    //navigate qua page thì _navigationService.NavigateTo<?>(), window thì _windowNavigationService.ShowWindow<?>()
                }
                else
                {
                    errormessage.Text = "Please enter existing email/password !!";
                }
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            _windowNavigationService.ShowWindow<RegisterWindow>();
            Close();
        }
    }
}
