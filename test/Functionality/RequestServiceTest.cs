using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace PlacetoPay.RedirectionTests.Functionality
{
    [TestFixture, Ignore("request")]
    public class RequestServiceTest : BaseTestCase
    {
        private const string DATA = "{" +
            "   \"payment\":{" +
            "       \"reference\":\"TEST_20200824_004602\"," +
            "       \"description\":\"Sint unde ab non dolor.\"," +
            "       \"amount\":{" +
            "           \"currency\":\"COP\"," +
            "           \"total\":160000" +
            "       }" +
            "   }," +
            "   \"ipAddress\":\"191.92.31.160\"," +
            "   \"returnUrl\":\"https:\\/\\/you-url.com\\/return?reference=TEST_20200824_004602\"," +
            "   \"userAgent\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36\"," +
            "}";

        [Test]
        public void Should_Make_A_Rest_Payment_Request()
        {
            var gateway = GetGateway(new JObject
            {
                { "additional", new JObject { { "timeout", 45000 }, { "connect_timeout", 30000 } } },
            });

            var response = gateway.Request(DATA);

            Assert.True(response.IsSuccessful());
        }

        [Test]
        public void Should_Make_A_Soap_Payment_Request()
        {
            var gateway = GetGateway(new JObject
            {
                { "type", "soap" },
            });

            var response = gateway.Request(DATA);

            Assert.True(response.IsSuccessful());
        }
    }
}
