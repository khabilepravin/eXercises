using AutoFixture.Xunit2;
using eXercise.Controllers;
using eXercise.Entities;
using eXercise.ServiceInterfaces;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class UserControllerTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetUser_WithNoPrams_ReturnsUserRecord(User expectedItem,
            [Frozen] Mock<IUserService> userRepositoryStub,
            [Greedy]UserController userController
            )
        {
            // Arrange
            userRepositoryStub.Setup(repo => repo.GetUserAsync()).ReturnsAsync(expectedItem);
            
            // Act
            var response = await userController.GetUserAsync();

            // Assert
            response.Value.Should().BeEquivalentTo(expectedItem);
        }    
    }
}
