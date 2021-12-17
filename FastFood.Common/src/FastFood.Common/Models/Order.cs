namespace FastFood.Common.Models
{
    using System.Collections.Generic;

    public class Order
    {
        public int OrderId { get; set; }
        public Customer Customer { get; set; }
        public List<Item> Items { get; set; }
        public int TotalTimeToCook { get; set; }
        public decimal TotalCost { get; set; }
        public bool Complete { get; set; }
    }
}
