using AutoFixture;
using AutoFixture.Xunit2;

namespace Tests
{
    public class AutoDomainDataAttribute : AutoDataAttribute
    {
        public AutoDomainDataAttribute() : base(() =>
        {
            var fixture = new Fixture();
            
            return fixture;
        })
        {

        }
    }
}
