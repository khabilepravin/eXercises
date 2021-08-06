using eXercise.Controllers;
using eXercise.Entities;
using eXercise.ServiceInterfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> productRepositoryStub;
        private readonly Mock<ILogger<ProductController>> loggerStub;
        private readonly Random random;
        private readonly string[] sortOptions;

        public ProductControllerTests()
        {
            productRepositoryStub = new Mock<IProductService>();
            loggerStub = new Mock<ILogger<ProductController>>();
            random = new Random();
            sortOptions = new string[] { "Low", "High", "Ascending", "Descending", "Recommended" };
        }

        [Fact]
        public async Task GetSortedProducts_WithValidSortOption_ReturnsSortedProducts()
        {
            // Arrange
            var expectedItems = new[] { CreateRandomItem(), CreateRandomItem(), CreateRandomItem() };

            productRepositoryStub.Setup(repo => repo.GetSortedProductsAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedItems);

            var productsController = new ProductController(loggerStub.Object, productRepositoryStub.Object);

            // Act
            var response = await productsController.GetSortedProducts(GetRandomSortOption());

            var result = response.Result.As<OkObjectResult>();

            // Assert
            result.Value.Should().BeEquivalentTo(expectedItems);
        }

        [Fact]
        public async Task GetSortedProducts_WithInvalidSortOption_ReturnsBadRequest()
        {
            // Arrange
            var expectedItems = new[] { CreateRandomItem(), CreateRandomItem(), CreateRandomItem() };

            productRepositoryStub.Setup(repo => repo.GetSortedProductsAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedItems);
            
            var productsController = new ProductController(loggerStub.Object, productRepositoryStub.Object);

            // Act
            var response = await productsController.GetSortedProducts("invalidsortoption");

            // Assert
            response.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        private Product CreateRandomItem()
        {
            return new Product
            {
                name = Guid.NewGuid().ToString(),
                quantity = random.Next(maxValue: 10),
                price = random.Next(maxValue: 100)
            };
        } 

        private string GetRandomSortOption()
        {
            return sortOptions[random.Next(maxValue: 4)];
        }
    }
}
