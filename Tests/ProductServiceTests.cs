using eXercise.Entities;
using eXercise.ServiceImplementations;
using eXercise.ServiceInterfaces;
using FluentAssertions;
using Moq;
using ServiceImplementations.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> productRepositoryStub;
        private readonly Mock<IShopperHistoryService> shopperHistoryServiceStub;
        private readonly Random random;
        private readonly string[] sortOptions;

        public ProductServiceTests()
        {
            productRepositoryStub = new Mock<IProductRepository>();
            shopperHistoryServiceStub = new Mock<IShopperHistoryService>();
            random = new Random();
            sortOptions = new string[] { "Low", "High", "Ascending", "Descending", "Recommended" };
        }

        [Fact]
        public async Task GetSortedProductsAsync_WithLowSortOption_OrdersProductsByPriceAsc()
        {
            // Arrange
            var expectedItems = new List<Product>() 
            { 
                new Product { Name = "A", Price=1 }, 
                new Product { Name = "B", Price=2 }, 
                new Product { Name = "C", Price=3 } 
            };

            var shopperHistoryItems = new List<ShopperHistory>()
            {
                new ShopperHistory { CustomerId=random.Next(1000), Products = expectedItems },
                new ShopperHistory { CustomerId=random.Next(1000), Products =  expectedItems}
            };


            productRepositoryStub.Setup(repo => repo.GetAllProductsAsync())
                .ReturnsAsync(expectedItems);

            shopperHistoryServiceStub.Setup(repo => repo.GetShopperHistoryAsync())
                .ReturnsAsync(shopperHistoryItems);

            var productService = new ProductService(productRepositoryStub.Object, shopperHistoryServiceStub.Object);

            // Act
            var sortedProducts = await productService.GetSortedProductsAsync("Low");

            // Assert
            sortedProducts.Should().BeInAscendingOrder(x => x.Price);
        }

        [Fact]
        public async Task GetSortedProductsAsync_WithRecommendedOption_OrdersProductsByPopularity()
        {
            // Arrange
            var aProduct = new Product { Name = "A", Price = 1, Quantity = 2 };
            var bProduct = new Product { Name = "B", Price = 2, Quantity = 4 };
            var cProduct = new Product { Name = "C", Price = 3, Quantity= 1 };

            var expectedItems = new List<Product>()
            {
                cProduct,
                aProduct,
                bProduct
            };

            var shopperHistoryItems = new List<ShopperHistory>()
            {
                new ShopperHistory { CustomerId=random.Next(1000), Products = new List<Product>() { cProduct, aProduct } },
                new ShopperHistory { CustomerId=random.Next(1000), Products = new List<Product>() {  cProduct, aProduct, bProduct } },
                new ShopperHistory { CustomerId=random.Next(1000), Products = new List<Product>() {  cProduct, bProduct } }
            };

            productRepositoryStub.Setup(repo => repo.GetAllProductsAsync())
                .ReturnsAsync(expectedItems);

            shopperHistoryServiceStub.Setup(repo => repo.GetShopperHistoryAsync())
                .ReturnsAsync(shopperHistoryItems);

            var productService = new ProductService(productRepositoryStub.Object, shopperHistoryServiceStub.Object);

            // Act
            var sortedProducts = await productService.GetSortedProductsAsync("Recommended");

            // Assert
            sortedProducts.Should().BeInDescendingOrder(x => x.PopularityIndex);
        }
    }
}
