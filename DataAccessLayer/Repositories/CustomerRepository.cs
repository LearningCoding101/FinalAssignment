using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class CustomerRepository
    {
        private readonly FuminiHotelManagementContext _context;
        public CustomerRepository(FuminiHotelManagementContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetById(int customerId)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task Add(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Customer?>> SearchCustomer(string keyword)
        {
            return await _context.Customers
                .Where(c =>
                !string.IsNullOrEmpty(c.CustomerFullName) && c.CustomerFullName.Contains(keyword)
                || !string.IsNullOrEmpty(c.EmailAddress) && c.EmailAddress.Contains(keyword))
                .ToListAsync();
        }
    }
}
