using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Tests
{
    public class AutoDomainDataAttribute : AutoDataAttribute
    {
        public AutoDomainDataAttribute() : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            return fixture;
        })
        {

        }
    }
}
