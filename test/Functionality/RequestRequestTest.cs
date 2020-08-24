using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace PlacetoPay.RedirectionTests.Functionality
{
    [TestFixture, Ignore("request")]
    public class RequestRequestTest : BaseTestCase
    {
        [Test]
        public void Should_Make_Request_Request()
        {
            string data =
            "{" +
            "   \"buyer\":{" +
            "       \"name\":\"Thurman\"," +
            "       \"surname\":\"Glover\"," +
            "       \"email\":\"dnetix@yopmail.com\"," +
            "       \"document\":\"1040035000\"," +
            "       \"documentType\":\"CC\"," +
            "       \"mobile\":3006108300" +
            "   }," +
            "   \"payment\":{" +
            "       \"reference\":\"TEST_20200824_004602\"," +
            "       \"description\":\"Sint unde ab non dolor.\"," +
            "       \"amount\":{" +
            "           \"currency\":\"COP\"," +
            "           \"total\":160000" +
            "       }" +
            "   }," +
            "   \"expiration\":\"2020-08-25T00:46:02-05:00\"," +
            "   \"ipAddress\":\"191.92.31.160\"," +
            "   \"returnUrl\":\"https://dnetix.co/p2p/client\"," +
            "   \"userAgent\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36\"," +
            "   \"paymentMethod\":\"\"" +
            "}";

            var gateway = GetGateway(new JObject
            {
                { "rest", new JObject { { "timeout", 45000 }, { "connect_timeout", 30000 } } },
            });

            var response = gateway.Request(data);

            Assert.True(response.IsSuccessful());
        }
    }
}
