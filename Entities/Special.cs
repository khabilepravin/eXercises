using System.Collections.Generic;

namespace eXercise.Entities
{
    public class Special
    {
        public string name { get; set; }
        public int quantity { get; set; }        
    }

    public class Specials
    {
        public IEnumerable<Special> quantities { get; set; }
        public decimal total { get; set; }
    }
}
