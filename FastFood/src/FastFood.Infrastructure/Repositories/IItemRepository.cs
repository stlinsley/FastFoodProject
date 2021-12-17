namespace FastFood.Infrastructure.Repositories
{
    using Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IItemRepository
    {
        Task<Item> GetItem(string name);

        Task<List<Item>> GetItems();

        Task AddItem(Item item);

        Task UpdateItem(Item item);
    }
}
