namespace FastFood.Infrastructure.Services
{
    using FastFood.Infrastructure.Options;
    using MongoDB.Driver;

    public class MongoServices : IMongoService
    {
        private readonly MongoDbOptions _options;
        private readonly IMongoClient _client;

        public MongoServices(MongoDbOptions options, IMongoClient client)
        {
            _options = options;
            _client = client;
        }

        public IMongoDatabase GetDatabase()
        {
            return _client.GetDatabase(_options.DatabaseName);
        }
    }
}
