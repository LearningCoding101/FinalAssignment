using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CustomerRepository
    {
        private readonly FuminiHotelManagementContext _context;
        public CustomerRepository(FuminiHotelManagementContext context)
        {
            _context = context;

        }
        public List<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer? GetById(int customerId)
        {
            return _context.Customers.Find(customerId);
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            _context.Update(customer);
            _context.SaveChanges();
        }

        public void Delete(int customerId)
        {
            var customer = _context.Customers.Find(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }
    }
}
