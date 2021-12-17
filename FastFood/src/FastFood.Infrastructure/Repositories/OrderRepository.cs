namespace FastFood.Infrastructure.Repositories
{
    using Core.Models;
    using FastFood.Infrastructure.Services;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(MongoServices services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            _orders = services.GetDatabase().GetCollection<Order>("order");
        }

        public async Task<Order> GetOrder(int orderId)
        {
            return await _orders.Find(c => c.OrderId == orderId).FirstAsync();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _orders.Find(_ => true)
                .SortByDescending(x => x.OrderId)
                .ToListAsync();
        }

        public async Task AddOrder(Order order)
        {
            if (order is null)
                throw new ArgumentNullException(nameof(order));

            await _orders.InsertOneAsync(order);
        }

        public async Task CancelOrder(int orderId)
        {
            await _orders.DeleteOneAsync(x => x.OrderId == orderId);
        }
    }
}
