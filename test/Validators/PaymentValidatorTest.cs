using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Exceptions;
using System.Collections.Generic;

namespace PlacetoPay.RedirectionTests.Validators
{
    [TestFixture]
    public class PaymentValidatorTest
    {
        [Test]
        public void Should_Pass_When_All_Ok()
        {
            string data = JsonConvert.SerializeObject(new
            {
                reference = "1234567890",
                amount = new
                {
                    currency = "COP",
                    total = 1000,
                },
                allowPartial = true,
                subscribe = false,
            });

            JObject json = JObject.Parse(data);

            var payment = new Payment(json);

            Assert.AreEqual((string)json["reference"], payment.Reference);
            Assert.AreEqual((bool)json["allowPartial"], payment.AllowPartial);

            CollectionAssert.AreEquivalent(json, payment.ToJsonObject());
        }

        [Test]
        public void Should_Allow_Empty_Instantiation()
        {
            var payment = new Payment();

            Assert.IsNull(payment.Amount);
        }

        [Test]
        public void Should_Receive_All_The_Entities()
        {
            string data = JsonConvert.SerializeObject(new
            {
                reference = "1234567890",
                description = "Testing payment",
                amount = new
                {
                    currency = "COP",
                    total = 1000,
                },
                recurring = new
                {
                    periodicity = "D",
                    interval = 4,
                    nextPayment = "2020-01-01",
                    maxPeriods = 4,
                    dueDate = "2022-01-01",
                },
                shipping = new
                {
                    name = "James",
                    email = "james@example.com",
                    address = new
                    {
                        street = "706 Evergreen",
                        city = "Villa de Nuestra Señora de la Candelaria de Medellín",
                        country = "CO",
                    },
                },
                fields = new[]
                {
                    new
                    {
                        keyword = "testing",
                        value = "Testing value",
                        displayOn = "both",
                    }
                },
                allowPartial = true,
                subscribe = false,
            });

            JObject json = JObject.Parse(data);

            var payment = new Payment(json);

            Assert.AreEqual((string)json["reference"], payment.Reference);
            Assert.AreEqual((double)json["amount"]["total"], payment.Amount.Total);
            Assert.AreEqual((string)json["recurring"]["periodicity"], payment.Recurring.Periodicity);
            Assert.AreEqual((string)json["shipping"]["name"], payment.Shipping.Name);
            Assert.AreEqual((string)json["shipping"]["address"]["street"], payment.Shipping.Address.Street);

            CollectionAssert.AreEquivalent(json, payment.ToJsonObject());
        }

        [Test]
        public void Should_Fail_When_Description_Is_Invalid()
        {
            string data = JsonConvert.SerializeObject(new
            {
                reference = "1234567890",
                description = "<script> </script>",
                amount = new
                {
                    currency = "COP",
                    total = 1000,
                },
                allowPartial = true,
                subscribe = false,
            });

            JObject json = JObject.Parse(data);

            try
            {
                var payment = new Payment(json).IsValid(out List<string> fields, false);
            }
            catch (EntityValidationFailException ex)
            {
                Assert.AreEqual(new List<string>() { "description" }, ex.Fields);
                Assert.AreEqual("Payment", ex.From);
            }
        }

        [Test]
        public void Should_Pass_When_Description_Ok()
        {
            string data = JsonConvert.SerializeObject(new
            {
                reference = "1234567890",
                description = "Pago de prueba para la factura #2321 con ref. 43242342-3424_32. ($50.000) 25/feb",
                amount = new
                {
                    currency = "COP",
                    total = 1000,
                },
                allowPartial = true,
                subscribe = false,
            });

            JObject json = JObject.Parse(data);

            var payment = new Payment(json);

            payment.IsValid(out List<string> _, false);

            Assert.AreEqual((string)json["reference"], payment.Reference);
            Assert.AreEqual((string)json["description"], payment.Description);
            Assert.AreEqual(new Amount((JObject)json["amount"]).ToJsonObject(), payment.Amount.ToJsonObject());

            CollectionAssert.AreEquivalent(json, payment.ToJsonObject());
        }
    }
}
