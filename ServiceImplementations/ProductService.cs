using eXercise.Entities;
using eXercise.ServiceInterfaces;
using ServiceImplementations.Repositories;
using System;
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

        private async Task<IEnumerable<Product>> GetSortedProducts(IEnumerable<Product> products, string sortOption)
        {
            switch (sortOption.ToLower())
            {
                default:
                case "low":
                    return products.OrderBy(p => p.Price);
                case "high":                        
                    return products.OrderByDescending(p => p.Price);
                case "ascending":
                    return products.OrderBy(p => p.Name);
                case "descending":
                    return products.OrderByDescending(p => p.Name);                    
                case "recommended":
                    return await SortByPopularity(products);                                    
            }
        }

        private async Task<IEnumerable<Product>> SortByPopularity(IEnumerable<Product> products)
        {
            var shoppersHistory = await _shopperHistoryService.GetShopperHistoryAsync();

            Dictionary<string, int> productsPopularityDictionary = new Dictionary<string, int>();

            foreach (var shopHistory in shoppersHistory)
            {
                foreach(var product in shopHistory.Products)
                {
                    if (productsPopularityDictionary.ContainsKey(product.Name))
                    {
                        productsPopularityDictionary[product.Name] = productsPopularityDictionary[product.Name] + Convert.ToInt32(product.Quantity);
                    }
                    else
                    {
                        productsPopularityDictionary.Add(product.Name, Convert.ToInt32(product.Quantity));
                    }
                }
            }

            foreach (var product in products)
            {
                if (productsPopularityDictionary.ContainsKey(product.Name))
                {
                    product.PopularityIndex += productsPopularityDictionary[product.Name];
                }
            }


            return products.OrderByDescending(p => p.PopularityIndex);
        }
    }


}
