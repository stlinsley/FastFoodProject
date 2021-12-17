namespace FastFood.Core.Models
{
    using Enums;
    using MongoDB.Bson.Serialization.Attributes;

    public class Item
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal Cost { get; set; }
        public int TimeToCookSeconds { get; set; }
    }
}
