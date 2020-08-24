using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace PlacetoPay.RedirectionTests.Functionality
{
    [TestFixture, Ignore("reverse")]
    public class ReverseRequestTest : BaseTestCase
    {
        [Test]
        public void Should_Make_Reverse_Request()
        {
            var gateway = GetGateway(new JObject
            {
                { "rest", new JObject { { "timeout", 45000 }, { "connect_timeout", 30000 } } },
            });

            var response = gateway.Reverse("340097");

            Assert.True(response.IsSuccessful());
        }
    }
}
