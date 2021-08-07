using eXercise.Entities;
using eXercise.ServiceInterfaces;
using ServiceImplementations.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eXercise.ServiceImplementations
{
    public class ProductService : IProductService
    {
        private readonly IShopperHistoryService _shopperHistoryService;
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository,  IShopperHistoryService shopperHistoryService)
        {
            _productRepository = productRepository;
            _shopperHistoryService = shopperHistoryService;
        }

        public async Task<IEnumerable<Product>> GetSortedProductsAsync(string sortOption)
        {
            var products = await _productRepository.GetAllProductsAsync();
            
            return await GetSortedProducts(products, sortOption);
        }

        private async Task<IEnumerable<Product>> GetSortedProducts(IEnumerable<ProductPopularity> products, string sortOption)
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
            var shoppersHistory = await _shopperHistoryService.GetShopperHistoryAsync(); 

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
