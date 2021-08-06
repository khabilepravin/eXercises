using System.Collections.Generic;

namespace eXercise.Entities
{
    public class TrolleyRequest
    {

        public IEnumerable<Product> products { get; set; }
        public IEnumerable<Specials> specials { get; set; }
        public IEnumerable<Special> quantities { get; set; }
    }
}
