using System.Text.Json.Serialization;

namespace eXercise.Entities
{
    public class Product : ProductBase
    {
        [JsonIgnore]
        public decimal PopularityIndex { get; set; }
    }
}
