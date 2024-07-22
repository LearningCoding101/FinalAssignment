using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class AuthenticationRepository
    {
        private readonly FuminiHotelManagementContext _context;

        public AuthenticationRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }
        public async Task<Customer?> AuthenticateUser(string email, string password)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.EmailAddress == email);
            if (customer != null && password == customer.Password)
            {
                return customer;
            }
            return null;
        }

        public async Task CreateUser(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }
    }
}
