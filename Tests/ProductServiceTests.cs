using AutoFixture.Xunit2;
using eXercise.Entities;
using eXercise.ServiceImplementations;
using eXercise.ServiceInterfaces;
using FluentAssertions;
using Moq;
using ServiceImplementations.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> productRepositoryStub;
        private readonly Mock<IShopperHistoryService> shopperHistoryServiceStub;

        public ProductServiceTests()
        {
            productRepositoryStub = new Mock<IProductRepository>();
            shopperHistoryServiceStub = new Mock<IShopperHistoryService>();            
        }

        [Theory]
        [AutoData]
        public async Task GetSortedProductsAsync_WithLowSortOption_OrdersProductsByPriceAsc(List<Product> expectedItems, List<ShopperHistory> shopperHistoryItems)
        {
            // Arrange
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

        [Theory]
        [AutoData]
        public async Task GetSortedProductsAsync_WithRecommendedOption_OrdersProductsByPopularity(List<Product> expectedItems, List<ShopperHistory> shopperHistoryItems)
        {
            // Arrange
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
