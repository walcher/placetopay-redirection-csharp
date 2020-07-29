using NUnit.Framework;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Message;

namespace PlacetoPay.RedirectionTests.Messages
{
    [TestFixture]
    public class RedirectInformationTest
    {
        [Test]
        public void Should_Parse_A_Rest_Updated_Response()
        {
            string result = 
            "{ " +
            "   \"requestId\":371," +
            "   \"status\":" +
            "   {" +
            "       \"status\":\"PENDING\"," +
            "       \"reason\":\"PT\"," +
            "       \"message\":\"La petición se encuentra pendiente\"," +
            "       \"date\":\"2017-05-17T15:57:44-05:00\"" +
            "   }," +
            "   \"request\":" +
            "   {" +
            "       \"locale\":\"es_CO\"," +
            "       \"payer\":" +
            "       {" +
            "           \"document\":\"1040035000\"," +
            "           \"documentType\":\"CC\"," +
            "           \"name\":\"Simon\"," +
            "           \"surname\":\"Godoy\"," +
            "           \"email\":\"test@gmail.com\"," +
            "           \"mobile\":\"3006108399\"," +
            "           \"address\":" +
            "           {" +
            "               \"street\":\"123 Main Street\"," +
            "               \"city\":\"Chesterfield\"," +
            "               \"postalCode\":\"63017\"," +
            "               \"country\":\"US\"" +
            "           }" +
            "       }," +
            "       \"buyer\":" +
            "       {" +
            "           \"document\":\"1040035000\"," +
            "           \"documentType\":\"CC\"," +
            "           \"name\":\"Sugeis\"," +
            "           \"surname\":\"Meza\"," +
            "           \"email\":\"test@msn.com\"," +
            "           \"mobile\":\"3002566565\"" +
            "       }," +
            "       \"payment\":" +
            "       {" +
            "           \"reference\":\"TEST_20200517_205552\"," +
            "           \"description\":\"Payment test.\"," +
            "           \"amount\":" +
            "           {" +
            "               \"currency\":\"USD\"," +
            "               \"total\":\"178\"" +
            "           }," +
            "           \"allowPartial\":false" +
            "       }," +
            "       \"subscription\":null," +
            "       \"fields\":null," +
            "       \"returnUrl\":\"http:\\/\\/redirect.p2p.dev\\/client\"," +
            "       \"paymentMethod\":null," +
            "       \"cancelUrl\":null," +
            "       \"ipAddress\":\"127.0.0.1\"," +
            "       \"userAgent\":\"Mozilla\\/5.0 (X11; Linux x86_64) AppleWebKit\\/537.36 (KHTML, like Gecko) Chrome\\/57.0.2987.98 Safari\\/537.36\"," +
            "       \"expiration\":\"2017-05-18T20:55:52+00:00\"," +
            "       \"captureAddress\":false," +
            "       \"skipResult\":false," +
            "       \"noBuyerFill\":false" +
            "   }," +
            "   \"payment\":" +
            "   [" +
            "       {" +
            "           \"status\":" +
            "           {" +
            "               \"status\":\"REJECTED\"," +
            "               \"reason\":\"01\"," +
            "               \"message\":\"Negada, Transacción declinada\"," +
            "               \"date\":\"2017-05-17T15:56:37-05:00\"" +
            "           }," +
            "           \"internalReference\":1447498827," +
            "           \"paymentMethod\":\"masterpass\"," +
            "           \"paymentMethodName\":\"MasterCard\"," +
            "           \"amount\":" +
            "           {" +
            "               \"from\":" +
            "               {" +
            "                   \"currency\":\"USD\"," +
            "                   \"total\":178" +
            "               }," +
            "               \"to\":" +
            "               {" +
            "                   \"currency\":\"COP\"," +
            "                   \"total\":511433.16" +
            "               }," +
            "               \"factor\":2873.22" +
            "           }," +
            "           \"authorization\":\"000000\"," +
            "           \"reference\":\"TEST_20200517_205552\"," +
            "           \"receipt\":\"1495054597\"," +
            "           \"franchise\":\"RM_MC\"," +
            "           \"refunded\":false," +
            "           \"processorFields\":" +
            "           [" +
            "               {" +
            "                   \"keyword\":\"lastDigits\"," +
            "                   \"value\":\"****0206\"," +
            "                   \"displayOn\":\"none\"" +
            "               }," +
            "               {" +
            "                   \"keyword\":\"id\"," +
            "                   \"value\":\"e6bc23b9f16980bc3e5422dbb6218f59\"," +
            "                   \"displayOn\":\"none\"" +
            "               }" +
            "           ]" +
            "       }" +
            "   ]," +
            "   \"subscription\":null" +
            "}";

            var information = new RedirectInformation(result);

            Assert.AreEqual(371, information.RequestId);
            Assert.AreEqual(Status.ST_PENDING, information.Status.StatusText);

            Assert.True(information.IsSuccessful());
            Assert.False(information.IsApproved());

            Assert.AreEqual("TEST_20200517_205552", information.Request.Payment.Reference);
            Assert.AreEqual("1040035000", information.Request.Buyer.Document);

            Assert.Null(information.LastApprovedTransaction());
            Assert.AreEqual(1495054597, information.LastTransaction().Receipt);
            Assert.Null(information.LastAuthorization());
        }
    }
}
