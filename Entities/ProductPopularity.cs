using System.Text.Json.Serialization;

namespace eXercise.Entities
{
    public class ProductPopularity : Product
    {
        [JsonIgnore]
        public decimal PopularityIndex { get; set; }
    }
}
