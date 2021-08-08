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
        private readonly Mock<IProductService> productServiceStub;
        private readonly Mock<ILogger<ProductController>> loggerStub;
        private readonly Random random;
        private readonly string[] sortOptions;

        public ProductControllerTests()
        {
            productServiceStub = new Mock<IProductService>();
            loggerStub = new Mock<ILogger<ProductController>>();
            random = new Random();
            sortOptions = new string[] { "Low", "High", "Ascending", "Descending", "Recommended" };
        }

        [Fact]
        public async Task GetSortedProducts_WithValidSortOption_ReturnsSortedProducts()
        {
            // Arrange
            var expectedItems = new[] { CreateRandomItem(), CreateRandomItem(), CreateRandomItem() };

            productServiceStub.Setup(repo => repo.GetSortedProductsAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedItems);

            var productsController = new ProductController(loggerStub.Object, productServiceStub.Object);

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

            productServiceStub.Setup(repo => repo.GetSortedProductsAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedItems);
            
            var productsController = new ProductController(loggerStub.Object, productServiceStub.Object);

            // Act
            var response = await productsController.GetSortedProducts("invalidsortoption");

            // Assert
            response.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        private Product CreateRandomItem()
        {
            return new Product
            {
                Name = Guid.NewGuid().ToString(),
                Quantity = random.Next(maxValue: 10),
                Price = random.Next(maxValue: 100)
            };
        } 

        private string GetRandomSortOption()
        {
            return sortOptions[random.Next(maxValue: 4)];
        }
    }
}
