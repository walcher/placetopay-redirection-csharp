using NUnit.Framework;
using PlacetoPay.Redirection.Message;

namespace PlacetoPay.RedirectionTests.Messages
{
    [TestFixture]
    public class ReverseResponseTest
    {
        [Test]
        public void Reverse_Response_With_Status()
        {
            string result =
            "{\n" +
            "  \"status\": {\n" +
            "    \"status\": \"APPROVED\",\n" +
            "    \"reason\": \"00\",\n" +
            "    \"message\": \"Se ha reversado el pago correctamente\",\n" +
            "    \"date\": \"2020-08-02T02:30:21-05:00\"\n" +
            "  },\n" +
            "  \"payment\": {\n" +
            "    \"reference\": \"TEST_20200802_022754\",\n" +
            "    \"amount\": {\n" +
            "      \"currency\": \"COP\"\n" +
            "    },\n" +
            "    \"allowPartial\": false,\n" +
            "    \"subscribe\": false\n" +
            "  }\n" +
            "}";

            var reverseResponse = new ReverseResponse(result);

            Assert.True(reverseResponse.IsSuccessful());
        }
    }
}
