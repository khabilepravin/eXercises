using eXercise.Entities;
using eXercise.ServiceInterfaces;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServiceImplementations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eXercise.ServiceImplementations
{
    public class ProductService : IProductService
    {
        private readonly string _apiBaseUrl;
        private readonly string _token;
        private readonly IShopperHistoryService _shopperHistoryService;
        public ProductService(IOptions<ExternalServiceSettings> options, IShopperHistoryService shopperHistoryService)
        {
            _apiBaseUrl = options.Value.ExternalServiceBaseUrl;
            _token = options.Value.Token;
            _shopperHistoryService = shopperHistoryService;
        }

        public async Task<IEnumerable<Product>> GetSortedProductsAsync(string sortOption)
        {

            var requestUrl = $"{_apiBaseUrl}/resource/products?token={_token}";

            var result = await requestUrl.GetStringAsync();

            if (string.IsNullOrWhiteSpace(result))
            {
                throw new System.Exception("External api failed to return list of products");
            }
            else
            {
               var products = JsonConvert.DeserializeObject<List<ProductPopularity>>(result);

                return await GetSortedProducts(products, sortOption);
            }
        }

        private async Task<IEnumerable<Product>> GetSortedProducts(List<ProductPopularity> products, string sortOption)
        {
            switch (sortOption.ToLower())
            {
                default:
                case "low":
                    return products.OrderBy(p => p.price);
                case "high":                        
                    return products.OrderByDescending(p => p.price);
                case "ascending":
                    return products.OrderBy(p => p.name);
                case "descending":
                    return products.OrderByDescending(p => p.name);                    
                case "recommended":
                    return await SortByPopularity(products);                                    
            }
        }

        private async Task<IEnumerable<Product>> SortByPopularity(IEnumerable<ProductPopularity> products)
        {
            var shoppersHistory = await _shopperHistoryService.GetShopperHistoryAsync(_token); 

            foreach (var shopHistory in shoppersHistory)
            {
                foreach (var product in products) {
                    var boughtThisProduct = (from p in shopHistory.Products
                    where string.Compare(p.name, product.name, ignoreCase: true) == 0
                    select p).FirstOrDefault<Product>();

                    if(boughtThisProduct != null)
                    {
                        product.PopularityIndex += boughtThisProduct.quantity;
                    }
                }
            }
          
            return products.OrderByDescending(p => p.PopularityIndex);
        }
    }
}
