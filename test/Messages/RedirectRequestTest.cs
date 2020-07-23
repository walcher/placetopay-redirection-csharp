using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PlacetoPay.Redirection.Message;

namespace PlacetoPay.RedirectionTests.Messages
{
    [TestFixture]
    class RedirectRequestTest
    {
        [Test]
        public void Should_Parse_Correctly_A_Payment_Request()
        {
            string data = JsonConvert.SerializeObject(new
            {
                locale = "en_US",
                payer = new
                {
                    name = "Simon",
                    surname = "Godoy",
                    email = "simon@testing.com",
                    documentType = "CC",
                    document = "123456789",
                    mobile = "3012563232",
                    address = new
                    {
                        street = "Fake street 123",
                        city = "Medellin",
                        state = "Antioquia",
                        postalCode = "050012",
                        country = "CO",
                        phone = "4442310",
                    },
                },
                buyer = new
                {
                    name = "Sugeis",
                    surname = "Meza",
                    email = "sugeis@testing.com",
                    documentType = "CC",
                    document = "987654321",
                    mobile = "3006108301",
                    address = new
                    {
                        street = "Fake street 321",
                        city = "Bogota",
                        state = "Bogota",
                        postalCode = "010012",
                        country = "CO",
                        phone = "+574442311",
                    },
                },
                payment = new
                {
                    reference = "Testing_2020",
                    description = "Testing payment for NUnit",
                    amount = new
                    {
                        taxes = new[]
                        {
                            new
                            {
                                kind = "valueAddedTax",
                                amount = 1.9,
                            },
                        },
                        details = new[]
                        {
                            new
                            {
                                kind = "tip",
                                amount = 1.0,
                            },
                            new
                            {
                                kind = "insurance",
                                amount = 0.1,
                            },
                        },
                        currency = "USD",
                        total = 10.283,
                    },
                    recurring = new
                    {
                        periodicity = "D",
                        interval = 7,
                        nextPayment = "2017-06-01",
                        maxPeriods = 4,
                        notificationUrl = "http://recurring-notification.com/hello",
                    },
                    shipping = new
                    {
                        name = "Jhonder",
                        surname = "Gonzalez",
                        email = "jhonder@testing.com",
                        documentType = "CC",
                        document = "918273645",
                        mobile = "3006108302",
                        address = new
                        {
                            street = "Fake street 213",
                            city = "Medellin",
                            state = "Antioquia",
                            postalCode = "050012",
                            country = "CO",
                            phone = "4442312",
                        },
                    },
                    allowPartial = true,
                },
                expiration = "2018-05-18T21:42:21+00:00",
                ipAddress = "127.0.0.1",
                userAgent = "NUnit",
                returnUrl = "http://your-return-url.com",
                cancelUrl = "http://your-cancel-url.com",
                skipResult = true,
                noBuyerFill = true,
                captureAddress = true,
                paymentMethod = "CR_VS,_ATH_",
            });

            JObject json = JObject.Parse(data);

            var request = new RedirectRequest(json);

            Assert.AreEqual((string)json["locale"], request.Locale);
            Assert.AreEqual("EN", request.GetLanguage());
            Assert.AreEqual((string)json["payment"]["reference"], request.GetReference());
            Assert.True(request.Payment.AllowPartial);
            Assert.AreEqual((string)json["returnUrl"], request.ReturnUrl);
            Assert.AreEqual((string)json["cancelUrl"], request.CancelUrl);
        }        

        [Test]
        public void Should_Parse_Correctly_A_Subscription_Request()
        {
            string data = JsonConvert.SerializeObject(new
            {
                buyer = new
                {
                    name = "Sugeis",
                    surname = "Meza",
                    email = "sugeis@testing.com",
                    documentType = "CC",
                    document = "987654321",
                    mobile = "3006108301",
                    address = new
                    {
                        street = "Fake street 321",
                        city = "Bogota",
                        state = "Bogota",
                        postalCode = "010012",
                        country = "CO",
                        phone = "+574442311",
                    },
                },
                subscription = new
                {
                    reference = "Testing_S_2020",
                    description = "Testing payment for NUnit",
                },
                ipAddress = "127.0.0.1",
                userAgent = "NUnit",
            });

            JObject json = JObject.Parse(data);

            string additional = JsonConvert.SerializeObject(new
            {
                expiration = "2018-05-18T21:42:21+00:00",
                returnUrl = "http://your-return-url.com",
                cancelUrl = "http://your-cancel-url.com",
                skipResult = true,
                noBuyerFill = true,
                captureAddress = true,
                paymentMethod = "CR_VS,_ATH_",
                userAgent = "NUnit",
                ipAddress = "127.0.0.12",
            });

            JObject jsonAdditional = JObject.Parse(additional);

            var request = new RedirectRequest(json);

            request.SetReturnUrl((string)jsonAdditional["returnUrl"])
                .SetIpAddress((string)jsonAdditional["ipAddress"])
                .SetUserAgent((string)jsonAdditional["userAgent"])
                .SetExpiration((string)jsonAdditional["expiration"])
                .SetCancelUrl((string)jsonAdditional["cancelUrl"]);

            Assert.AreEqual((string)json["subscription"]["reference"], request.GetReference());

            Assert.AreEqual((string)jsonAdditional["returnUrl"], request.ReturnUrl);
            Assert.AreEqual((string)jsonAdditional["ipAddress"], request.IpAddress);
            Assert.AreEqual((string)jsonAdditional["userAgent"], request.UserAgent);
            Assert.AreEqual((string)jsonAdditional["expiration"], request.Expiration);
            Assert.AreEqual((string)jsonAdditional["cancelUrl"], request.CancelUrl);
        }

        [Test]
        public void Should_Parse_Correctly_A_Payment_Request_With_String_Data()
        {
            string test =
            "{  " +
            "   \"payment\":{  " +
            "      \"reference\":\"Testing_S_2020\"," +
            "      \"amount\":{  " +
            "         \"currency\":\"COP\"," +
            "         \"total\":\"10000\"," +
            "         \"taxes\":[" +
            "            {  " +
            "               \"kind\":\"valueAddedTax\"," +
            "               \"amount\":\"1.2\"," +
            "               \"base\":\"8\"" +
            "            }" +
            "         ]" +
            "      }" +
            "   }," +
            "   \"returnUrl\":\"http:\\/\\/your-return-url.com\"" +
            "}";

            var request = new RedirectRequest(test);

            Assert.AreEqual("http://your-return-url.com", request.ReturnUrl);
            Assert.AreEqual("Testing_S_2020", request.GetReference());
            Assert.IsNotNull(request.Payment);
        }

        [Test]
        public void Should_Parse_Correctly_A_Payment_Request_With_Object_Instance()
        {
            var request = new RedirectRequest(null, null, null, "http://your-return-url.com", "CR_VS");

            Assert.IsNull(request.Payer);
            Assert.AreEqual("http://your-return-url.com", request.ReturnUrl);
            Assert.AreEqual("CR_VS", request.PaymentMethod);            
        }

        [Test]
        public void Should_Parse_Correctly_A_Payment_Request_With_Full_Object_Instance()
        {
            var request = new RedirectRequest(null, null, null, null, "http://your-return-url.com", "CR_VS", null, null, null, null, false, false, false, null);

            Assert.IsNull(request.Fields);
            Assert.AreEqual("http://your-return-url.com", request.ReturnUrl);
            Assert.AreEqual("CR_VS", request.PaymentMethod);
            Assert.False(request.CaptureAddress);
            Assert.False(request.NoBuyerFill);
            Assert.False(request.SkipResult);
        }
    }
}
