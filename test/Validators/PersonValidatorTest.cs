using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Exceptions;
using PlacetoPay.Redirection.Validators;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PlacetoPay.RedirectionTests.Validators
{
    [TestFixture]
    public class PersonValidatorTest
    {
        [Test]
        public void Should_Pass_When_All_Ok()
        {
            string data = JsonConvert.SerializeObject(new
            {
                name = "John",
                surname = "Doe",
                email = "johndoe@example.com",
                document = "1040035000",
                documentType = "CC",
                company = "Acme S.A.S.",
                address = new
                {
                    street = "707 Evergreen",
                    city = "Medellín",
                    country = "CO",
                },
            });

            JObject json = JObject.Parse(data);

            var person = new Person(json);

            Assert.AreEqual((string)json["name"], person.Name);
            Assert.AreEqual((string)json["surname"], person.Surname);
            Assert.AreEqual((string)json["email"], person.Email);
            Assert.AreEqual((string)json["document"], person.Document);
            Assert.AreEqual((string)json["documentType"], person.DocumentType);
            Assert.AreEqual((string)json["company"], person.Company);

            CollectionAssert.AreEquivalent(json, person.ToJsonObject());
        }

        [Test]
        public void Should_Pass_When_Street_Ok()
        {
            Assert.AreEqual(true, new Regex(PersonValidator.GetPattern("STREET"), RegexOptions.IgnoreCase)
                .IsMatch("Calle 5ta No 24 - 34, Unidad Bolivariana (Torre 24 apto 202)"));
        }

        [Test]
        public void Shoup_Fail_When_Street_Is_Invalid()
        {
            Assert.AreEqual(false, new Regex(PersonValidator.GetPattern("STREET"), RegexOptions.IgnoreCase)
                .IsMatch("<> Calle 5ta No 24 - 34, Unidad Bolivariana (Torre 24 apto 202)"));
        }

        [Test]
        public void Shoul_Allow_Empty_Instantiation()
        {
            var person = new Person();

            Assert.IsNull(person.Name);
        }

        [Test]
        public void Should_Pass_A_Portuguese_Name()
        {
            string data = JsonConvert.SerializeObject(new
            {
                name = "ASSUNÇÃO",
                surname = "DoÑe",
                email = "johndoe@example.com",
                document = "1040035000",
                documentType = "CC",
                company = "Acme S.A.S.",
            });

            JObject json = JObject.Parse(data);

            try
            {
                var person = new Person(json);
                person.IsValid(out List<string> _, false);

                Assert.AreEqual((string)json["name"], person.Name);
            }
            catch (EntityValidationFailException)
            {
                Assert.Fail("There should be no exception here");
            }
        }

        [Test]
        public void Should_Fail_If_Document_Is_Invalid()
        {
            string data = JsonConvert.SerializeObject(new
            {
                name = "John",
                surname = "Doe",
                email = "johndoe@example.com",
                document = "104003500000000000000000000",
                documentType = "CC",
                company = "Acme S.A.S.",
            });

            JObject json = JObject.Parse(data);

            try
            {
                var person = new Person(json).IsValid(out List<string> fields, false);
            }
            catch (EntityValidationFailException ex)
            {
                Assert.AreEqual(new List<string>() { "documentType", "document" }, ex.Fields);
                Assert.AreEqual("Person", ex.From);
            }
        }

        [Test]
        public void Should_Fail_If_DocumentType_Is_Invalid()
        {
            string data = JsonConvert.SerializeObject(new
            {
                name = "John",
                surname = "Doe",
                email = "johndoe@example.com",
                document = "1040035000",
                documentType = "Cedula",
                company = "Acme S.A.S.",
            });

            JObject json = JObject.Parse(data);

            try
            {
                var person = new Person(json).IsValid(out List<string> fields, false);
            }
            catch (EntityValidationFailException ex)
            {
                Assert.AreEqual(new List<string>() { "documentType", "document" }, ex.Fields);
                Assert.AreEqual("Person", ex.From);
            }
        }

        [Test]
        public void Should_Fail_If_Email_Is_Invalid()
        {
            string data = JsonConvert.SerializeObject(new
            {
                name = "John",
                surname = "Doe",
                email = "INVALID",
                document = "1040035000",
                documentType = "CC",
                company = "Acme S.A.S.",
            });

            JObject json = JObject.Parse(data);

            try
            {
                var person = new Person(json).IsValid(out List<string> fields, false);
            }
            catch (EntityValidationFailException ex)
            {
                Assert.AreEqual(new List<string>() { "email" }, ex.Fields);
                Assert.AreEqual("Person", ex.From);
            }
        }
    }
}
