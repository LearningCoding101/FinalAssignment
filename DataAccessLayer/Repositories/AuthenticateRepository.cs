using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class AuthenticateRepository
    {
        private readonly FuminiHotelManagementContext _context;

        public AuthenticateRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }
        public Customer? AuthenticateUser(string email, string password)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.EmailAddress == email);
            if (customer != null && password == customer.Password)
            {
                return customer;
            }
            return null;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public void CreateUser(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
    }
}
