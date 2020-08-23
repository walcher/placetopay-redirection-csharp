using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PlacetoPay.Redirection.Entities;

namespace PlacetoPay.RedirectionTests.Functionality
{
    [TestFixture]
    public class AuthenticationTest
    {
        [Test]
        public void Should_Create_The_Authentication_Correctly()
        {
            JObject data = new JObject
            {
                { "login", "login" },
                { "tranKey", "ABCD1234" },
                {
                    "auth", new JObject
                    {
                        { "seed", "2016-10-26T21:37:00+00:00" },
                        { "nonce", "ifYEPnAcJbpDVR1t" }
                    }
                },
            };

            var json = new Authentication(data).ToJsonObject();

            Assert.AreEqual("login", (string)json.GetValue("login"), "Login matches");
            Assert.AreEqual("2016-10-26T21:37:00+00:00", (string)json.GetValue("seed"), "Seed matches");
            Assert.AreEqual("aWZZRVBuQWNKYnBEVlIxdA==", (string)json.GetValue("nonce"), "Nonce matches");
            Assert.AreEqual("Xi5xrRwrqPU21WE2JI4hyMaCvQ8=", (string)json.GetValue("tranKey"), "Trankey matches");
        }

        [Test]
        public void Should_Parse_With_String()
        {
            string data =
            "{  \n" +
            "   \"login\":\"login\",\n" +
            "   \"tranKey\":\"ABCD1234\",\n" +
            "   \"auth\":{  \n" +
            "       \"seed\":\"2016-10-26T21:37:00+00:00\",\n" +
            "       \"nonce\":\"ifYEPnAcJbpDVR1t\"\n" +
            "   }\n" +
            "}";

            var json = new Authentication(data).ToJsonObject();

            Assert.AreEqual("login", (string)json.GetValue("login"), "Login matches");
            Assert.AreEqual("2016-10-26T21:37:00+00:00", (string)json.GetValue("seed"), "Seed matches");
            Assert.AreEqual("aWZZRVBuQWNKYnBEVlIxdA==", (string)json.GetValue("nonce"), "Nonce matches");
            Assert.AreEqual("Xi5xrRwrqPU21WE2JI4hyMaCvQ8=", (string)json.GetValue("tranKey"), "Trankey matches");
        }

        [Test]
        public void Should_Parse_With_Object()
        {
            var json = new Authentication(
                "login",
                "ABCD1234",
                new AuthenticationSecurity(
                    "2016-10-26T21:37:00+00:00",
                    "ifYEPnAcJbpDVR1t"
                )
            ).ToJsonObject();

            Assert.AreEqual("login", (string)json.GetValue("login"), "Login matches");
            Assert.AreEqual("2016-10-26T21:37:00+00:00", (string)json.GetValue("seed"), "Seed matches");
            Assert.AreEqual("aWZZRVBuQWNKYnBEVlIxdA==", (string)json.GetValue("nonce"), "Nonce matches");
            Assert.AreEqual("Xi5xrRwrqPU21WE2JI4hyMaCvQ8=", (string)json.GetValue("tranKey"), "Trankey matches");
        }

        [Test]
        public void Should_Create_The_Authentication_Without_Auth()
        {
            JObject data = new JObject
            {
                { "login", "login" },
                { "tranKey", "ABCD1234" },
            };

            var json = new Authentication(data).ToJsonObject();

            Assert.IsTrue(json.ContainsKey("seed"));
            Assert.IsTrue(json.ContainsKey("nonce"));
        }
    }
}
