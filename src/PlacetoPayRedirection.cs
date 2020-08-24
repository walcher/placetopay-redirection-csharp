using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Carriers;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Exceptions;
using PlacetoPay.Redirection.Helpers;
using PlacetoPay.Redirection.Messages;
using System;
using System.Collections.Generic;

namespace PlacetoPay.Redirection
{
    /// <summary>
    /// Class <c>PlacetoPay</c>
    /// </summary>
    public class PlacetoPayRedirection : Gateway
    {
        /// <summary>
        /// PlacetoPayRedirection constructor.
        /// </summary>
        public PlacetoPayRedirection() { }

        /// <summary>
        /// PlacetoPayRedirection constructor.
        /// </summary>
        /// <param name="data">string</param>
        public PlacetoPayRedirection(string data) : this(JsonFormatter.ParseJObject(data)) { }

        /// <summary>
        /// PlacetoPayRedirection constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public PlacetoPayRedirection(JObject data) : base(data) { }

        /// <summary>
        /// PlacetoPayRedirection constructor.
        /// </summary>
        /// <param name="login">string</param>
        /// <param name="trankey">string</param>
        /// <param name="url">Uri</param>
        public PlacetoPayRedirection(string login, string trankey, Uri url) : base(login, trankey, url) { }

        /// <summary>
        /// PlacetoPayRedirection constructor.
        /// </summary>
        /// <param name="login">string</param>
        /// <param name="trankey">string</param>
        /// <param name="url">Uri</param>
        /// <param name="requestType">string</param>
        public PlacetoPayRedirection(
            string login,
            string trankey,
            Uri url,
            string requestType
            ) : base(login, trankey, url, requestType) { }

        /// <summary>
        /// PlacetoPayRedirection constructor.
        /// </summary>
        /// <param name="login">string</param>
        /// <param name="trankey">string</param>
        /// <param name="url">Uri</param>
        /// <param name="additional">Dictionary</param>
        /// <param name="requestType">string</param>
        public PlacetoPayRedirection(
            string login,
            string trankey,
            Uri url,
            Dictionary<string, string> additional,
            string requestType
            ) : base(login, trankey, url, additional, requestType) { }

        /// <summary>
        /// PlacetoPayRedirection constructor.
        /// </summary>
        /// <param name="login">string</param>
        /// <param name="trankey">string</param>
        /// <param name="url">Uri</param>
        /// <param name="auth">AuthenticationSecurity</param>
        /// <param name="additional">Dictionary</param>
        /// <param name="requestType">string</param>
        public PlacetoPayRedirection(
            string login,
            string trankey,
            Uri url,
            AuthenticationSecurity auth,
            Dictionary<string, string> additional,
            string requestType
            ) : base(login, trankey, url, auth, additional, requestType) { }

        /// <summary>
        /// Get carrier instance.
        /// </summary>
        /// <returns></returns>
        private Carrier Carrier()
        {
            if (carrier is Carrier)
            {
                return carrier;
            }

            JObject config = this.config;
            Authentication auth = new Authentication(config);
            string requestType = this.requestType;

            JObject typeConfig = config.ContainsKey(requestType)
                ? config.GetValue(requestType).ToObject<JObject>()
                : new JObject();

            if (requestType == TP_SOAP)
            {
                var carrierConfig = new JObject
                {
                    { "wsdl", $"{config["url"]}soap/redirect?wsdl" },
                    { "location", $"{config["url"]}soap/redirect" },
                };

                carrierConfig.Merge(typeConfig, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Union
                });

                carrier = new SoapCarrier(auth, carrierConfig);
            }
            else
            {
                var carrierConfig = new JObject
                {
                    { "url", config.GetValue("url").ToString() },
                };

                carrierConfig.Merge(typeConfig, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Union
                });

                carrier = new RestCarrier(auth, carrierConfig);
            }

            return carrier;
        }

        /// <summary>
        /// Collect endpoint.
        /// </summary>
        /// <param name="collectRequest">object</param>
        /// <returns>RedirectInformation</returns>
        public override RedirectInformation Collect(object collectRequest)
        {
            if (collectRequest.GetType() == typeof(string))
            {
                collectRequest = new CollectRequest((string)collectRequest);
            }

            if (collectRequest.GetType() == typeof(string))
            {
                collectRequest = new CollectRequest((JObject)collectRequest);
            }

            if (!(collectRequest.GetType() == typeof(CollectRequest)))
            {
                throw new PlacetoPayException("Wrong class request");
            }

            return Carrier().Collect((CollectRequest)collectRequest);
        }

        public override RedirectInformation Query(string requestId)
        {
            throw new NotImplementedException();
        }

        public override RedirectResponse Request(object redirectRequest)
        {
            throw new NotImplementedException();
        }

        public override ReverseResponse Reverse(string transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
