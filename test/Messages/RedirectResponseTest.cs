using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PlacetoPay.Redirection.Message;
using System;

namespace PlacetoPay.RedirectionTests.Messages
{
    [TestFixture]
    public class RedirectResponseTest
    {
        [Test]
        public void Redirection_Response_Without_Status()
        {
            var carrierResponse = new RedirectResponse(new JObject {
                { "requestId", new Random().Next(0, 100000) },
                { "processUrl", "http://localhost/payment/process" },
            });

            Assert.False(carrierResponse.IsSuccessful());
        }

        [Test]
        public void Redirection_Response_With_Status()
        {
            var carrierResponse = new RedirectResponse(new JObject {
                { "requestId", new Random().Next(0, 100000) },
                { "processUrl", "http://localhost/payment/process" },
                { "status", new JObject { { "status", "OK" }, { "reason", 2 }, { "message", "Aprobada" }, { "date", "2019-03-10T12:36:36-05:00" } } }
            });

            Assert.True(carrierResponse.IsSuccessful());
        }
    }
}
