namespace FastFood.Infrastructure.Repositories
{
    using Core.Models;
    using System.Threading.Tasks;

    public interface ICustomerRepository
    {

        Task<Customer> GetCustomer(string email);

        Task AddCustomer(Customer customer);

        Task UpdateCustomer(Customer customer);
    }
}
