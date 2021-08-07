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
            var expectedItems = new List<ProductPopularity>() 
            { 
                new ProductPopularity { name = "A", price=1 }, 
                new ProductPopularity { name = "B", price=2 }, 
                new ProductPopularity { name = "C", price=3 } 
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
            sortedProducts.Should().BeInAscendingOrder(x => x.price);
        }

    }
}
