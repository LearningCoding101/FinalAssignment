using System.Windows;

namespace Assignment2.Services
{
    public interface IWindowNavigationService
    {
        void ShowWindow<T>() where T : Window, new();
    }
}
