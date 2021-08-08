using eXercise.Entities;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServiceImplementations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceImplementations.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _apiBaseUrl;
        private readonly string _token;
        public ProductRepository(IOptions<ExternalServiceSettings> options)
        {
            _apiBaseUrl = options.Value.ExternalServiceBaseUrl;
            _token = options.Value.Token;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var requestUrl = $"{_apiBaseUrl}/resource/products?token={_token}";

            var result = await requestUrl.GetStringAsync();

            if (string.IsNullOrWhiteSpace(result))
            {
                throw new System.Exception("External api failed to return list of products");
            }
            else
            {
                return JsonConvert.DeserializeObject<List<Product>>(result);
            }
        }
    }
}
