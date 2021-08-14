using AutoFixture.Xunit2;
using eXercise.Controllers;
using eXercise.Entities;
using eXercise.ServiceInterfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ProductControllerTests
    {   
        private readonly Random random;
        private readonly string[] sortOptions;

        public ProductControllerTests()
        {
            random = new Random();
            sortOptions = new string[] { "Low", "High", "Ascending", "Descending", "Recommended" };
        }

        [Theory]
        [AutoDomainData]
        public async Task GetSortedProducts_WithValidSortOption_ReturnsSortedProducts(IEnumerable<Product> expectedItems, 
            [Frozen] Mock<IProductService> productServiceStub,
            [Greedy]ProductController productsController)
        {
            // Arrange
            productServiceStub.Setup(repo => repo.GetSortedProductsAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedItems);

            // Act
            var response = await productsController.GetSortedProducts(GetRandomSortOption());

            var result = response.Result.As<OkObjectResult>();

            // Assert
            result.Value.Should().BeEquivalentTo(expectedItems);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetSortedProducts_WithInvalidSortOption_ReturnsBadRequest(IEnumerable<Product> expectedItems,
            [Frozen] Mock<IProductService> productServiceStub,
            [Greedy] ProductController productsController)
        {
            // Arrange
            productServiceStub.Setup(repo => repo.GetSortedProductsAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedItems);
            
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
