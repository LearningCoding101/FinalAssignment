using DataAccessLayer.Models;

namespace BusinessLayer.Services
{
    public interface IAuthenticationService
    {
        Task<Customer?> Login(string email, string password);

        string GetUserRole(string email);

        Task Register(Customer customer);
    }
}
