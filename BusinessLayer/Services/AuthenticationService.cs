using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationRepository _authenticationRepository;
        private static string AdminEmail = "admin@gmail.com";
        private static string AdminPassword = "admin";

        public AuthenticationService(AuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public async Task<Customer?> Login(string email, string password)
        {
            if (email.Equals(AdminEmail) && password.Equals(AdminPassword))
            {
                return new Customer
                {
                    CustomerId = -1,
                    EmailAddress = email,
                    Password = password,
                    CustomerStatus = 1
                };
            }
            return await _authenticationRepository.AuthenticateUser(email, password);
        }

        public string GetUserRole(string email)
        {
            return email == AdminEmail ? "ADMIN" : "USER";
        }

        public async Task Register(Customer customer)
        {
            await _authenticationRepository.CreateUser(customer);
        }
    }
}
