using eXercise.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using ServiceImplementations;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class TrolleyServiceLocalImplementationTests
    {
        //[Fact]
        public async Task GetTrolleyTotalAsync_WithSpecialsRequest_CalculatesTheDisountProperty()
        {
            //   var input = "{'products':[{ 'name':'1','price':2.0,'quantity':0.0},{ 'name':'2','price':5.0,'quantity':0.0}],'specials':[{ 'quantities':[{ 'name':'1','quantity':3},{ 'name':'2','quantity':0}],'total':5.0},{ 'quantities':[{ 'name':'1','quantity':1},{ 'name':'2','quantity':2}],'total':10.0}],'quantities':[{ 'name':'1','quantity':3},{ 'name':'2','quantity':2}]}"; //14
            var input = "{'products':[{'name':'Product 0','price':4.6242541771495,'quantity':0},{'name':'Product 1','price':2.21907229964578,'quantity':0},{'name':'Product 2','price':12.2895504312075,'quantity':0},{'name':'Product 3','price':2.40629709670613,'quantity':0},{'name':'Product 4','price':14.6821300474378,'quantity':0},{'name':'Product 5','price':5.61604023008423,'quantity':0},{'name':'Product 6','price':5.15750243335846,'quantity':0}],'specials':[{'quantities':[{'name':'Product 0','quantity':5},{'name':'Product 1','quantity':9},{'name':'Product 2','quantity':6},{'name':'Product 3','quantity':5},{'name':'Product 4','quantity':7},{'name':'Product 5','quantity':2},{'name':'Product 6','quantity':9}],'total':12.19666540073},{'quantities':[{'name':'Product 0','quantity':6},{'name':'Product 1','quantity':4},{'name':'Product 2','quantity':8},{'name':'Product 3','quantity':6},{'name':'Product 4','quantity':9},{'name':'Product 5','quantity':4},{'name':'Product 6','quantity':5}],'total':3.12860968645802},{'quantities':[{'name':'Product 0','quantity':6},{'name':'Product 1','quantity':6},{'name':'Product 2','quantity':7},{'name':'Product 3','quantity':9},{'name':'Product 4','quantity':4},{'name':'Product 5','quantity':8},{'name':'Product 6','quantity':8}],'total':35.8606559246525},{'quantities':[{'name':'Product 0','quantity':0},{'name':'Product 1','quantity':7},{'name':'Product 2','quantity':3},{'name':'Product 3','quantity':3},{'name':'Product 4','quantity':3},{'name':'Product 5','quantity':1},{'name':'Product 6','quantity':6}],'total':21.4055397781393},{'quantities':[{'name':'Product 0','quantity':0},{'name':'Product 1','quantity':8},{'name':'Product 2','quantity':7},{'name':'Product 3','quantity':2},{'name':'Product 4','quantity':3},{'name':'Product 5','quantity':2},{'name':'Product 6','quantity':1}],'total':7.54261498611558}],'quantities':[{'name':'Product 0','quantity':4},{'name':'Product 1','quantity':7},{'name':'Product 2','quantity':1},{'name':'Product 3','quantity':4},{'name':'Product 4','quantity':5},{'name':'Product 5','quantity':3},{'name':'Product 6','quantity':5}]}"; //171.99154471838447

            var trolleyRequest = JsonConvert.DeserializeObject<TrolleyRequest>(input);

            var trolleyCalculatorLocal = new TrolleyServiceLocalImplementation();

            var result = await trolleyCalculatorLocal.GetTrolleyTotalAsync(trolleyRequest);

            result.Should().Be(14);
        }
    }
}
