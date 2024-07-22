using DataAccessLayer.Models;

namespace BusinessLayer.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomers();

        Task<Customer?> GetCustomerById(int id);

        Task UpdateCustomer(Customer customer);

        Task DeleteCustomer(int id);

        Task<IEnumerable<Customer?>> SearchCustomerByKeyword(string keyword);
    }
}
