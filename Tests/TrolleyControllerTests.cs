using AutoFixture.Xunit2;
using eXercise.Controllers;
using eXercise.Entities;
using eXercise.ServiceInterfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class TrolleyControllerTests
    {
        [Theory]
        [AutoDomainData]
        public async Task GetTrolleyTotalAsync_WithValidTrolleyRequest_ReturnsAPositiveNumber([Frozen]Mock<ITrolleyService> trolleyServiceStub,
            Mock<ILogger<TrolleyController>> loggerStub,
            [Greedy]TrolleyController trolleyController)
        {
            // arrange
            var trolleyRequest = JsonConvert.DeserializeObject<TrolleyRequest>("{'products':[{'name':'1','price':2,'quantity':0},{'name':'2','price':5,'quantity':0}],'specials':[{'quantities':[{'name':'1','quantity':3},{'name':'2','quantity':0}],'total':5},{'quantities':[{'name':'1','quantity':1},{'name':'2','quantity':2}],'total':10}],'quantities':[{'name':'1','quantity':3},{'name':'2','quantity':2}]}");
            var expectedValue = 14;


            trolleyServiceStub.Setup(service => service.GetTrolleyTotalAsync(It.IsAny<TrolleyRequest>()))
                .ReturnsAsync(expectedValue);

            // act
            var response = await trolleyController.GetTrolleyTotalAsync(trolleyRequest);

            var result = response.Result.As<OkObjectResult>();

            // assert
            result.Value.Should().BeEquivalentTo(expectedValue);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetTrolleyTotalAsync_WithInvalidTrolleyRequest_ReturnsBadRequest([Frozen] Mock<ITrolleyService> trolleyServiceStub,
            Mock<ILogger<TrolleyController>> loggerStub,
            [Greedy] TrolleyController trolleyController)
        {
            // arrange            
            trolleyServiceStub.Setup(service => service.GetTrolleyTotalAsync(It.IsAny<TrolleyRequest>()))
                .ReturnsAsync(0);

            // act
            var response = await trolleyController.GetTrolleyTotalAsync(null);


            // assert
            response.Result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
