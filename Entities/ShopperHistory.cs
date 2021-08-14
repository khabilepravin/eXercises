using System.Collections.Generic;

namespace eXercise.Entities
{
    public class ShopperHistory
    {
        public int CustomerId { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
