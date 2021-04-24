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
    /// <summary>
    /// Class <c>SoapCarrier</c>
    /// </summary>
    public class SoapCarrier : Carrier
    {
        protected const string LOCATION = "location";
        protected new const string AUTH = "auth";

        protected string location;
        protected XNamespace wsdl = "http://placetopay.com/soap/redirect/";

        /// <summary>
        /// Initializes a new instance of the SoapCarrier class.
        /// </summary>
        /// <param name="auth">authentication object.</param>
        /// <param name="config">json configuration object</param>
        public SoapCarrier(Authentication auth, JObject config) : base(auth, config)
        {
            if (!config.ContainsKey(LOCATION))
            {
                throw new PlacetoPayException("Location not found for this");
            }

            location = config.GetValue(LOCATION).ToString();
        }

        /// <summary>
        /// Set up a new http client.
        /// </summary>
        /// <returns>http client instance.</returns>
        private HttpClient HttpClientFactory()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "SoapHttpClient/2.2.1 (.net core 4.0)");

            return client;
        }

        /// <summary>
        /// Make a soap request.
        /// </summary>
        /// <param name="body">formatted xml body.</param>
        /// <returns>the response string from service.</returns>
        private string MakeRequest(XElement body)
        {
            try
            {
                SoapClient client = new SoapClient(HttpClientFactory);
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

        /// <summary>
        /// Collect endpoint.
        /// </summary>
        /// <param name="collectRequest">collect request instance object.</param>
        /// <returns>redirect information instance object.</returns>
        public override RedirectInformation Collect(CollectRequest collectRequest)
        {
            XmlDocument response = new XmlDocument();
            XmlDocument payload = JsonConvert.DeserializeXmlNode(collectRequest.ToJsonObject().ToString(), "payload");
            XElement body = new XElement(wsdl.GetName("collect"), XElement.Parse(payload.InnerXml));

            response.LoadXml(MakeRequest(body));
            response = RemoveNullFields(response);

            XmlNode data = response.SelectSingleNode("descendant::collectResult");
            JObject json = JObject.Parse(JsonConvert.SerializeXmlNode(data));

            return new RedirectInformation(json.GetValue("collectResult").ToString());
        }

        /// <summary>
        /// Query endpoint.
        /// </summary>
        /// <param name="requestId">transaction request id.</param>
        /// <returns>redirect information instance object.</returns>
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

        /// <summary>
        /// Request endpoint.
        /// </summary>
        /// <param name="redirectRequest">redirect request instance object.</param>
        /// <returns>redirect response instance object.</returns>
        public override RedirectResponse Request(RedirectRequest redirectRequest)
        {
            XmlDocument response = new XmlDocument();
            XmlDocument payload = JsonConvert.DeserializeXmlNode(redirectRequest.ToJsonObject().ToString(), "payload");
            XElement body = new XElement(wsdl.GetName("createRequest"), XElement.Parse(payload.InnerXml));

            response.LoadXml(MakeRequest(body));
            response = RemoveNullFields(response);

            XmlNode data = response.SelectSingleNode("descendant::createRequestResult");
            JObject json = JObject.Parse(JsonConvert.SerializeXmlNode(data));

            return new RedirectResponse(json.GetValue("createRequestResult").ToString());
        }

        /// <summary>
        /// Reverse enpoint.
        /// </summary>
        /// <param name="internalReference">transaction internal reference</param>
        /// <returns>reverse response instance object.</returns>
        public override ReverseResponse Reverse(string internalReference)
        {
            XmlDocument response = new XmlDocument();
            XElement body = new XElement(wsdl.GetName("reversePayment"), new XElement("internalReference", internalReference));

            response.LoadXml(MakeRequest(body));
            response = RemoveNullFields(response);

            XmlNode data = response.SelectSingleNode("descendant::reversePaymentResult");
            JObject json = JObject.Parse(JsonConvert.SerializeXmlNode(data));

            return new ReverseResponse(json.GetValue("reversePaymentResult").ToString());
        }

        /// <summary>
        /// This method remover nullable elements from xml.
        /// </summary>
        /// <param name="xmldoc">xml document to normalize.</param>
        /// <returns>normalized xml document.</returns>
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
