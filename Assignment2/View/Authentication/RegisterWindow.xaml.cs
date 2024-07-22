using Assignment2.Services;
using System.Windows;

namespace Assignment2.View.Authentication
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private readonly IWindowNavigationService _windowNavigationService;

        public RegisterWindow()
        {
            InitializeComponent();
            _windowNavigationService = new WindowNavigationService();
        }

        public RegisterWindow(IWindowNavigationService windowNavigationService) : this()
        {
            _windowNavigationService = windowNavigationService;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            _windowNavigationService.ShowWindow<LoginWindow>();
            Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            textBoxAddress.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
