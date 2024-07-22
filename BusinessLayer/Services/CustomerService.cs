using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task DeleteCustomer(int id)
        {
            await _customerRepository.Delete(id);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _customerRepository.GetAll();
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            return await _customerRepository.GetById(id);
        }

        public async Task<IEnumerable<Customer?>> SearchCustomerByKeyword(string keyword)
        {
            return await _customerRepository.SearchCustomer(keyword);
        }

        public Task UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
