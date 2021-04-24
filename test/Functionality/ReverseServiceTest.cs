using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace PlacetoPay.RedirectionTests.Functionality
{
    [TestFixture, Ignore("reverse")]
    public class ReverseServiceTest : BaseTestCase
    {
        [Test]
        public void Should_Make_A_Rest_Request_To_Reverse_A_Transaction()
        {
            var gateway = GetGateway(new JObject
            {
                { "additional", new JObject { { "timeout", 45000 }, { "connect_timeout", 30000 } } },
            });

            var response = gateway.Reverse("1509424156");

            Assert.True(response.IsSuccessful());
        }

        [Test]
        public void Should_Make_A_Soap_Request_To_Reverse_A_Transaction()
        {
            var gateway = GetGateway(new JObject
            {
                { "type", "soap" },
            });

            var response = gateway.Reverse("1509424060");

            Assert.True(response.IsSuccessful());
        }
    }
}
