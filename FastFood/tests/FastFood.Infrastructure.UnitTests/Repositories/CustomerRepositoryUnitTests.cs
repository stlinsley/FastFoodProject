namespace FastFood.Infrastructure.UnitTests.Repositories
{
    using FastFood.Infrastructure.Services;
    using FastFood.Infrastructure.Options;
    using FastFood.Infrastructure.Repositories;
    using FluentAssertions;
    using Microsoft.Extensions.Options;
    using Moq;
    using System;
    using Xunit;
    using MongoDB.Driver;
    using System.Threading.Tasks;

    public class CustomerRepositoryUnitTests
    {
        private readonly CustomerRepository _customerRepo;
        private MongoServices _services;
        private MongoDbOptions _options;
        private Mock<IMongoClient> _mongoClient;

        public CustomerRepositoryUnitTests()
        {
            _options = new MongoDbOptions
            {
                DatabaseName = "customer"
            };
            _mongoClient = new Mock<IMongoClient>();
            _services = new MongoServices(_options, _mongoClient.Object);
            _customerRepo = new CustomerRepository(_services);
        }

        [Fact]
        public void Ctor_LiteDbContextNull_ThrowsArgumentNullException()
        {
            Action act = () => new CustomerRepository(null);

            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'context')");
        }

        [Fact]
        public async Task GetCustomer_EmailNull_ThrowsArgumentNullException()
        {
            Action act = async () => await _customerRepo.GetCustomer(null);
            act.Should().Throw<ArgumentNullException>().WithMessage("'email' cannot be null or empty.");
        }

        [Fact]
        public async Task GetCustomer_EmailEmpty_ThrowsArgumentNullException()
        {
            Action act = async () => await _customerRepo.GetCustomer(string.Empty);
            act.Should().Throw<ArgumentNullException>().WithMessage("'email' cannot be null or empty.");
        }

        [Fact]
        public async Task GetCustomer_NoCustomerFound_ReturnsNull()
        {
            Action act = async () => await _customerRepo.GetCustomer("notfound@notfound.com");
            act.Should().BeNull();
        }
    }
}
