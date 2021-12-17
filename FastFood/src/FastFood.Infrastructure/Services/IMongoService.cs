namespace FastFood.Infrastructure.Services
{
    using MongoDB.Driver;

    public interface IMongoService
    {
        IMongoDatabase GetDatabase();
    }
}
