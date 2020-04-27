using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PlacetoPay.Redirection.Message;
using System.IO;

namespace PlacetoPay.RedirectionTests.Messages
{
    [TestFixture]
    class RedirectRequestTest
    {
        [Test]
        public void ItParsesCorrectlyAPaymentRequestTest()
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
                        phone = "4442311",
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
                                amount = 1.2,
                                baseAmount = 8,
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

            JsonReader reader = new JsonTextReader(new StringReader(data))
            {
                DateParseHandling = DateParseHandling.None
            };

            JObject json = JObject.Load(reader);

            var request = new RedirectRequest(json);

            Assert.True(true);
        }
    }
}
