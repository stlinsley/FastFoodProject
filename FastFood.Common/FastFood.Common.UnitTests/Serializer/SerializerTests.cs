namespace FastFood.Common.UnitTests.Serializer
{
    using FastFood.Common.Models;
    using Xunit;

    public class SerializerTests
    {
        [Fact]
        public void Serialize_ReturnsSerializeObject()
        {
            //arrange
            var serializer = new Common.Serializer.Serializer();
            var customer = new Customer
            {
                Email = "email@email.com",
                Id = 1,
                Name = "Jimmy Bones"
            };

            //act
            var result = serializer.Serialize(customer);

            //assert
            result.
        }

    }
}
