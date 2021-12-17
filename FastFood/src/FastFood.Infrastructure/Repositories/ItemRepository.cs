namespace FastFood.Infrastructure.Repositories
{
    using Core.Models;
    using FastFood.Infrastructure.Services;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ItemRepository : IItemRepository
    {
        private readonly IMongoCollection<Item> _items;

        public ItemRepository(MongoServices services)
        {
            _items = services.GetDatabase().GetCollection<Item>("item");
        }

        public async Task<Item> GetItem(string name)
        {
            return await _items.Find(x => x.Name == name).FirstAsync();
        }

        public async Task<List<Item>> GetItems()
        {
            return await _items.Find(_ => true)
                .SortByDescending(x => x.Category)
                .ThenByDescending(x => x.Name)
                .ToListAsync();
        }

        public async Task AddItem(Item item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if (await GetItem(item.Name) != null)
                throw new ArgumentException("An item with this name already exists.");
            await _items.InsertOneAsync(item);
        }

        public async Task UpdateItem(Item item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));
            if (GetItem(item.Name) == null)
                throw new ArgumentException("This customer does not exist");
            await _items.ReplaceOneAsync(c => c.Id == item.Id, item);
        }
    }
}
