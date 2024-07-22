using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Assignment2.Services
{
    public class WindowNavigationService : IWindowNavigationService
    {
        public void ShowWindow<T>() where T : Window, new()
        {
            var window = new T();
            window.Show();
        }
    }
}
