using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Exceptions;
using PlacetoPay.Redirection.Messages;
using SoapHttpClient;
using SoapHttpClient.Enums;
using System;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;

namespace PlacetoPay.Redirection.Carriers
{
    public class SoapCarrier : Carrier
    {
        protected const string LOCATION = "location";
        protected new const string AUTH = "auth";
        protected const string TIMEOUT = "timeout";
        protected const string CONNECT_TIMEOUT = "connect_timeout";

        protected string location;
        protected static XNamespace wsdl = "http://placetopay.com/soap/redirect/";

        public SoapCarrier(Authentication auth, JObject config) : base(auth, config)
        {
            if (!config.ContainsKey(LOCATION))
            {
                throw new PlacetoPayException("Location not found for this");
            }

            location = config.GetValue(LOCATION).ToString();
        }

        public override RedirectInformation Collect(CollectRequest collectRequest)
        {
            throw new NotImplementedException();
        }

        public override RedirectInformation Query(string requestId)
        {
            XmlDocument response = new XmlDocument();
            XElement body = new XElement(wsdl.GetName("getRequestInformation"), new XElement("requestId", requestId));

            response.LoadXml(MakeRequest(body));
            response = RemoveNullFields(response);

            XmlNode data = response.SelectSingleNode("descendant::getRequestInformationResult");
            JObject json = JObject.Parse(JsonConvert.SerializeXmlNode(data));

            return new RedirectInformation(json.GetValue("getRequestInformationResult").ToString());
        }

        public override RedirectResponse Request(RedirectRequest redirectRequest)
        {
            throw new NotImplementedException();
        }

        public override ReverseResponse Reverse(string transactionId)
        {
            throw new NotImplementedException();
        }

        private string MakeRequest(XElement body)
        {
            try
            {
                SoapClient client = new SoapClient();
                XmlDocument header = new XmlDocument();

                header.LoadXml(Auth.AsSoapHeader());

                HttpResponseMessage message = client.Post(
                    new Uri(location),
                    SoapVersion.Soap12,
                    body: body,
                    header: header);

                return message.Content.ReadAsStringAsync().Result;
            }
            catch (Exception exeption)
            {
                return new JObject
                {
                    {
                        "status", new JObject
                        {
                            { "status", Status.ST_ERROR },
                            { "reason", "WR" },
                            { "message", PlacetoPayException.ReadException(exeption) },
                            { "date", (DateTime.Now).ToString("yyyy-MM-ddTHH\\:mm\\:sszzz") },
                        }
                    },
                }.ToString();
            }
        }

        private static XmlDocument RemoveNullFields(XmlDocument xmldoc)
        {
            XmlNamespaceManager mgr = new XmlNamespaceManager(xmldoc.NameTable);
            mgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            XmlNodeList nullFields = xmldoc.SelectNodes("//*[@xsi:nil='true']", mgr);

            if (nullFields != null && nullFields.Count > 0)
            {
                for (int i = 0; i < nullFields.Count; i++)
                {
                    nullFields[i].ParentNode.RemoveChild(nullFields[i]);
                }
            }

            return xmldoc;
        }
    }
}
