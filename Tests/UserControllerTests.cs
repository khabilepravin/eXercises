using AutoFixture.Xunit2;
using eXercise.Controllers;
using eXercise.Entities;
using eXercise.ServiceInterfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> userRepositoryStub;
        private readonly Mock<ILogger<UserController>> loggerStub;
        private readonly Random randomNumberGenerator;
     
        public UserControllerTests()
        {
            userRepositoryStub = new Mock<IUserService>();
            loggerStub = new Mock<ILogger<UserController>>();
            randomNumberGenerator = new Random();
        }
        
        [Theory]
        [AutoDomainData]
        public async Task GetUser_WithNoPrams_ReturnsUserRecord(User expectedItem)
        {
            // Arrange
            userRepositoryStub.Setup(repo => repo.GetUserAsync()).ReturnsAsync(expectedItem);
            
            var exceriseController = new UserController(loggerStub.Object,                                                             
                                                            userRepositoryStub.Object);

            // Act
            var response = await exceriseController.GetUserAsync();

            // Assert
            response.Value.Should().BeEquivalentTo(expectedItem);
        }    
    }
}
