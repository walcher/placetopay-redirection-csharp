using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Message;

namespace PlacetoPay.RedirectionTests.Messages
{
    [TestFixture]
    public class CollectRequestTest
    {
        [Test]
        public void Should_Parse_Correctly_A_Collect_With_Credit()
        {
            string data = JsonConvert.SerializeObject(new
            {
                locale = "en_US",
                payer = new
                {
                    document = "123456789",
                    documentType = "CC",
                    name = "Simon",
                    surname = "Godoy",
                    email = "simon@testing.com",
                },
                payment = new
                {
                    reference = "Testing_2020",
                    description = "Testing payment for NUnit",
                    amount = new
                    {
                        currency = "COP",
                        total = 143000,
                    },
                    allowPartial = false,
                    subscribe = false,
                },
                instrument = new
                {
                    credit = new
                    {
                        code = 500,
                        type = "02",
                        groupCode = "P",
                        installment = 3,
                    },
                    token = new
                    {
                        token = "e317950201950c59e91b6a59b25d439888a504579715a09bc0862c76b64335d9",
                    },
                },
            });

            JObject json = JObject.Parse(data);

            var request = new CollectRequest(json);

            Assert.AreEqual((int)json["instrument"]["credit"]["code"], request.Instrument.Credit.Code);
            Assert.AreEqual((string)json["instrument"]["credit"]["type"], request.Instrument.Credit.Type);
            Assert.AreEqual((string)json["instrument"]["credit"]["groupCode"], request.Instrument.Credit.GroupCode);
            Assert.AreEqual((int)json["instrument"]["credit"]["installment"], request.Instrument.Credit.Installment);
            Assert.AreEqual((int)json["payment"]["amount"]["total"], request.Payment.Amount.Total);
            Assert.AreEqual(JObject.Parse(JsonConvert.SerializeObject(json["instrument"])), request.Instrument.ToJsonObject());
            Assert.AreEqual(json, request.ToJsonObject());
        }

        [Test]
        public void Should_Parse_Correctly_A_Collect_Request_With_String_Data()
        {
            string data =
            "{  \n" +
            "   \"payer\":{  \n" +
            "      \"name\":\"John\",\n" +
            "      \"surname\":\"Doe\",\n" +
            "      \"email\":\"johndoe@example.com\",\n" +
            "      \"document\":\"1040035000\",\n" +
            "      \"documentType\":\"CC\"\n" +
            "   },\n" +
            "   \"payment\":{  \n" +
            "      \"reference\":\"TESTING123456\",\n" +
            "      \"amount\":{  \n" +
            "         \"currency\":\"COP\",\n" +
            "         \"total\":\"10000\"\n" +
            "      }\n" +
            "   },\n" +
            "   \"instrument\":{  \n" +
            "      \"token\":{  \n" +
            "         \"token\":\"961da9f371a8edc212a525f5e8d69934bec8484f546c720d3c5bf75350602ba0\"\n" +
            "      }\n" +
            "   }\n" +
            "}";

            var request = new CollectRequest(data);

            Assert.AreEqual("961da9f371a8edc212a525f5e8d69934bec8484f546c720d3c5bf75350602ba0", request.Instrument.Token.TokenText);
            Assert.AreEqual("TESTING123456", request.GetReference());
            Assert.IsNotNull(request.Instrument);
        }

        [Test]
        public void Should_Parse_Correctly_A_Collect_Request_With_Object_Instance()
        {
            Payment payment = new Payment("TESTING_2020", null, false, false, null, null, null, null, null, null, null, null);
            var request = new CollectRequest(null, null, payment, null);

            Assert.IsInstanceOf<Payment>(payment);
            Assert.IsNotNull(request.Payment);
            Assert.AreEqual("TESTING_2020", request.GetReference());
        }

        [Test]
        public void Should_Parse_Correctly_A_Collect_Request_With_Full_Object_Instance()
        {
            Payment payment = new Payment("TESTING_2020", null, false, false, null, null, null, null, null, null, null, null);
            var request = new CollectRequest("en_US", null, null, payment, null);

            Assert.IsInstanceOf<Payment>(payment);
            Assert.IsNotNull(request.Payment);
            Assert.AreEqual("TESTING_2020", request.GetReference());
            Assert.AreEqual("EN", request.GetLanguage());
        }
    }
}
