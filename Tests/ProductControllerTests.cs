using eXercise.Controllers;
using eXercise.Entities;
using eXercise.ServiceInterfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
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

        [Theory]
        [AutoDomainData]
        public async Task GetSortedProducts_WithValidSortOption_ReturnsSortedProducts(IEnumerable<Product> expectedItems)
        {
            // Arrange
            productServiceStub.Setup(repo => repo.GetSortedProductsAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedItems);

            var productsController = new ProductController(loggerStub.Object, productServiceStub.Object);

            // Act
            var response = await productsController.GetSortedProducts(GetRandomSortOption());

            var result = response.Result.As<OkObjectResult>();

            // Assert
            result.Value.Should().BeEquivalentTo(expectedItems);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetSortedProducts_WithInvalidSortOption_ReturnsBadRequest(IEnumerable<Product> expectedItems)
        {
            // Arrange
            productServiceStub.Setup(repo => repo.GetSortedProductsAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedItems);
            
            var productsController = new ProductController(loggerStub.Object, productServiceStub.Object);

            // Act
            var response = await productsController.GetSortedProducts("invalidsortoption");

            // Assert
            response.Result.Should().BeOfType<BadRequestObjectResult>();
        }
               
        private string GetRandomSortOption()
        {
            return sortOptions[random.Next(maxValue: 4)];
        }
    }
}
