using System.Windows.Controls;

namespace Assignment2.Services
{
    public interface INavigationService
    {
        void NavigateTo<T>() where T : Page, new();
    }
}
