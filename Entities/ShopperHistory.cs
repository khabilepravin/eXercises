using System.Collections.Generic;

namespace eXercise.Entities
{
    public class ShopperHistory
    {
        public int CustomerId { get; set; }
        public IEnumerable<ProductBase> Products { get; set; }
    }
}
