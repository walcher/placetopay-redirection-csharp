using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Exceptions;
using System.Collections.Generic;

namespace PlacetoPay.RedirectionTests.Validators
{
    [TestFixture]
    public class AddressValidatorTest
    {
        [Test]
        public void Should_Passe_When_All_Ok()
        {
            string data = JsonConvert.SerializeObject(new 
            {
                street = "707 Evergreen",
                city = "Medellín",
                country = "CO",
                state = "Antioquia",
                postalCode = "050012",
                phone = "4442310",
            });

            JObject json = JObject.Parse(data);

            var address = new Address(json);

            Assert.AreEqual((string)json["street"], address.Street);
            Assert.AreEqual((string)json["city"], address.City);
            Assert.AreEqual((string)json["country"], address.Country);
            Assert.AreEqual((string)json["state"], address.State);
            Assert.AreEqual((string)json["postalCode"], address.PostalCode);
            Assert.AreEqual((string)json["phone"], address.Phone);

            CollectionAssert.AreEquivalent(json, address.ToJsonObject());
        }

        [Test]
        public void Should_Allow_Empty_Instantiation()
        {
            var address = new Address();

            Assert.IsNull(address.Street);
        }

        [Test]
        public void Should_Fail_When_No_Required_Provided()
        {
            string data = JsonConvert.SerializeObject(new
            {
                state = "Antioquia",
                postalCode = "050012",
                phone = "+5744442310 ext 1503",
            });

            JObject json = JObject.Parse(data);

            try
            {
                var address = new Address(json).IsValid(out List<string> fields, false);
            }
            catch(EntityValidationFailException ex)
            {
                Assert.AreEqual(new List<string>() { "street", "city", "country" }, ex.Fields);
                Assert.AreEqual("Address", ex.From);
            }
        }

        [Test]
        public void Should_Fail_When_Wrong_Country_Provided()
        {
            string data = JsonConvert.SerializeObject(new
            {
                street = "707 Evergreen",
                city = "Medellín",
                country = "Colombia",
                phone = "4442310",
            });

            JObject json = JObject.Parse(data);

            try
            {
                var address = new Address(json).IsValid(out List<string> fields, false);
            }
            catch (EntityValidationFailException ex)
            {
                Assert.AreEqual(new List<string>() { "country" }, ex.Fields);
                Assert.AreEqual("Address", ex.From);
            }
        }
    }
}
