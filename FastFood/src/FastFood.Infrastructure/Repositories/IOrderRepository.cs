namespace FastFood.Infrastructure.Repositories
{
    using Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrderRepository
    {
        Task<Order> GetOrder(int orderId);
        Task<List<Order>> GetOrders();
        Task AddOrder(Order order);
        Task CancelOrder(int orderId);
    }
}
