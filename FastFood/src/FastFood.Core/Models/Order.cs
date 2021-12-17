namespace FastFood.Core.Models
{
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;

    public class Order
    {
        [BsonId]
        public int OrderId { get; set; }
        public Customer Customer { get; set; }
        public List<Item> Items { get; set; }
        public int TotalTimeToCook { get; set; }
        public decimal TotalCost { get; set; }
        public bool Complete { get; set; }
    }
}
