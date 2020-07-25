using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Message;

namespace PlacetoPay.RedirectionTests.Messages
{
    [TestFixture]
    public class NotificationTest
    {
        [Test]
        public void Should_Parse_Correctly_The_Notification()
        {
            string data = JsonConvert.SerializeObject(new
            {
                status = new
                {
                    status = "REJECTED",
                    reason = "00",
                    message = "Pago rechazado",
                    date = "2020-10-10T16:39:57-05:00",
                },
                requestId = 83,
            });

            JObject json = JObject.Parse(data);

            var notification = new Notification(json);

            Assert.True(notification.IsRejected(), notification.GetStatus().StatusText);
            Assert.AreEqual("Pago rechazado", notification.GetStatus().Message);
            Assert.IsNull(notification.Reference);
        }

        [Test]
        public void Should_Parse_Correctly_The_Notification_With_String_Data()
        {
            string data =
            "{  " +
            "   \"requestId\":1," +
            "   \"reference\":\"TEST_20201010_213937\"," +
            "   \"signature\":\"8fb4beea130ab3e75a1de956bd0213892e0f6839\"" +
            "}";

            var notification = new Notification(data, "024h1IlD");

            Assert.False(notification.IsApproved(), notification.GetStatus().StatusText);
            Assert.AreEqual(notification.RequestId, 1);
            Assert.AreEqual(notification.Reference, "TEST_20201010_213937");
        }

        [Test]
        public void Should_Parse_Correctly_The_Notification_With_Object_Instance()
        {
            Status status = new Status(new JObject {
                { "status", "APPROVED" },
                { "reason", "00" },
                { "message", "Se ha aprobado su pago, puede imprimir el recibo o volver a la pagina del comercio" },
                { "date", "2016-10-10T16:39:57-05:00" },
            });

            var notification = new Notification(status, 83, "TEST_20161010_213937", "8fb4beea130ab3e75a1de956bd0213892e0f6839", "024h1IlD");

            Assert.True(notification.IsValidNotification(), "Valid notification");
            Assert.True(notification.IsApproved(), notification.GetStatus().StatusText);
            Assert.False(notification.IsRejected(), notification.GetStatus().StatusText);
            Assert.AreEqual(notification.RequestId, 83, "Same request identifier");
            Assert.AreEqual(notification.Reference, "TEST_20161010_213937", "Same reference");
        }
    }
}
