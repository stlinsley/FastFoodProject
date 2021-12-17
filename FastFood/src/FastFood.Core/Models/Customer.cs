namespace FastFood.Core.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    public class Customer
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
