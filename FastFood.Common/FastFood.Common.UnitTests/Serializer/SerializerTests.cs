namespace FastFood.Common.UnitTests.Serializer
{
    using FastFood.Common.Models;
    using FluentAssertions;
    using System;
    using Xunit;

    public class SerializerTests
    {
        [Fact]
        public void Serialize_ReturnsSerializeObject()
        {
            //arrange
            var expected = @"{""Id"":1,""Name"":""Jester Lavore"",""Email"":""email@email.com""}";
            var serializer = new Common.Serializer.Serializer();
            var customer = new Customer
            {
                Email = "email@email.com",
                Id = 1,
                Name = "Jester Lavore"
            };

            //act
            var result = serializer.Serialize(customer);

            //assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Deserialize_NullObject_ThrowsException()
        {
            //arrange
            var serializer = new Common.Serializer.Serializer();

            //act
            Action result = () => serializer.Deserialize<Customer>(null);

            //assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Deserialize_ReturnsDeserializedObject()
        {
            //arrange
            var serializer = new Common.Serializer.Serializer();
            var customer = @"{""Id"":1,""Name"":""Jester Lavore"",""Email"":""email@email.com""}";
            var expected = new Customer
            {
                Email = "email@email.com",
                Id = 1,
                Name = "Jester Lavore"
            };

            //act
            var result = serializer.Deserialize<Customer>(customer);

            //assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
