using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PlacetoPay.Redirection.Entities;

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
    }
}
