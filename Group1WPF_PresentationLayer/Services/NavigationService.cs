using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Assignment2.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Frame _frame;

        public NavigationService(Frame frame)
        {
            _frame = frame;
        }

        public void NavigateTo<T>() where T : Page, new()
        {
            _frame.Navigate(new T());
        }
    }
}
