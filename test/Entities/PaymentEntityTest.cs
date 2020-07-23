using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PlacetoPay.Redirection.Message;
using System;
using System.IO;

namespace PlacetoPay.RedirectionTests.Entities
{
    [TestFixture]
    class PaymentEntityTest
    {
        [Test]
        public void Should_Parse_Correctly_A_Dispersion()
        {
            string data = JsonConvert.SerializeObject(new
            {
                buyer = new
                {
                    name = "Simon",
                    email = "simon.godoy@placetopay.com",
                },
                payment = new
                {
                    reference = "TEST_3",
                    description = "Testing Payment",
                    amount = new
                    {
                        currency = "COP",
                        total = 243590,
                    },
                    dispersion = new[]
                    {
                        new
                        {
                            agreement = 29,
                            agreementType = "AIRLINE",
                            amount = new
                            {
                                taxes = new[]
                                {
                                    new
                                    {
                                        kind = "valueAddedTax",
                                        amount = 30590,
                                    },
                                    new
                                    {
                                        kind = "airportTax",
                                        amount = 16300,
                                    },
                                },
                                currency = "COP",
                                total = 207890,
                            },
                        },
                        new
                        {
                            agreement = 0,
                            agreementType = "MERCHANT",
                            amount = new
                            {
                                taxes = new[]
                                {
                                    new
                                    {
                                        kind = "valueAddedTax",
                                        amount = 5700,
                                    },
                                },
                                currency = "COP",
                                total = 35700,
                            },
                        },
                    },
                },
                expiration = (DateTime.Now).AddDays(+1).ToString("yyyy-MM-ddTHH\\:mm\\:sszzz"),
                returnUrl = "https://dnetix.co/ping/rtest",
                ipAddress = "127.0.0.1",
                userAgent = "NUnit",
            });

            JsonReader reader = new JsonTextReader(new StringReader(data))
            {
                DateParseHandling = DateParseHandling.None
            };

            JObject json = JObject.Load(reader);

            var request = new RedirectRequest(json).ToJsonObject();

            Assert.True(true);
        }
    }
}
