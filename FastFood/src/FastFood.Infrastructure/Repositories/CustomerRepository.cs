namespace FastFood.Infrastructure.Repositories
{
    using Core.Models;
    using FastFood.Infrastructure.Services;
    using MongoDB.Driver;
    using System;
    using System.Threading.Tasks;

    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerRepository(MongoServices services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));            
            _customers = services.GetDatabase().GetCollection<Customer>("customer");
        }

        public async Task<Customer> GetCustomer(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException($"'{nameof(email)}' cannot be null or empty.", nameof(email));
            return await _customers.Find(c => c.Email == email).FirstAsync();
        }

        public async Task AddCustomer(Customer customer)
        {
            if (customer is null)
                throw new ArgumentNullException(nameof(customer));
            if (GetCustomer(customer.Email) != null)
                throw new ArgumentException("A customer with this email already exists.");
            await _customers.InsertOneAsync(customer);
        }

        public async Task UpdateCustomer(Customer customer)
        {
            if (customer is null)
                throw new ArgumentNullException(nameof(customer));
            if (GetCustomer(customer.Email) == null)
                throw new ArgumentException("This customer does not exist");
            await _customers.ReplaceOneAsync(c => c.Email == customer.Email, customer);
        }

        public async Task DeleteCustomer(Customer customer)
        {
            if (customer is null)
                throw new ArgumentNullException(nameof(customer));
            if (GetCustomer(customer.Email) == null)
                throw new ArgumentException("This customer does not exist");
            await _customers.DeleteOneAsync(x => x.Id == customer.Id);
        }
    }
}
