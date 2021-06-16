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
    public class PlacetoPayRedirection : Gateway
    {
        /// <summary>
        /// Initializes a new instance of the PlacetoPayRedirection class.
        /// </summary>
        public PlacetoPayRedirection() { }

        /// <summary>
        /// Initializes a new instance of the PlacetoPayRedirection class.
        /// </summary>
        /// <param name="data">string</param>
        public PlacetoPayRedirection(string data) : this(JsonFormatter.ParseJObject(data)) { }

        /// <summary>
        /// Initializes a new instance of the PlacetoPayRedirection class.
        /// </summary>
        /// <param name="data">JObject</param>
        public PlacetoPayRedirection(JObject data) : base(data) { }

        /// <summary>
        /// Initializes a new instance of the PlacetoPayRedirection class.
        /// </summary>
        /// <param name="login">string</param>
        /// <param name="trankey">string</param>
        /// <param name="url">Uri</param>
        public PlacetoPayRedirection(string login, string trankey, Uri url) : base(login, trankey, url) { }

        /// <summary>
        /// Initializes a new instance of the PlacetoPayRedirection class.
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
        /// Initializes a new instance of the PlacetoPayRedirection class.
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
        /// Initializes a new instance of the PlacetoPayRedirection class.
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
        /// <returns>Carrier instance.</returns>
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
                    { "location", $"{config[URL]}soap/redirect" },
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
                    { URL, config.GetValue(URL).ToString() },
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

            if (collectRequest.GetType() == typeof(JObject))
            {
                collectRequest = new CollectRequest((JObject)collectRequest);
            }

            if (!(collectRequest.GetType() == typeof(CollectRequest)))
            {
                throw new PlacetoPayException("Wrong class request");
            }

            return Carrier().Collect((CollectRequest)collectRequest);
        }

        /// <summary>
        /// Query endpoint.
        /// </summary>
        /// <param name="requestId">string</param>
        /// <returns>RedirectInformation</returns>
        public override RedirectInformation Query(string requestId)
        {
            return Carrier().Query(requestId);
        }

        /// <summary>
        /// Request endpoint.
        /// </summary>
        /// <param name="redirectRequest">object</param>
        /// <returns>RedirectResponse</returns>
        public override RedirectResponse Request(object redirectRequest)
        {
            if (redirectRequest.GetType() == typeof(string))
            {
                redirectRequest = new RedirectRequest((string)redirectRequest);
            }

            if (redirectRequest.GetType() == typeof(JObject))
            {
                redirectRequest = new RedirectRequest((JObject)redirectRequest);
            }

            if (!(redirectRequest.GetType() == typeof(RedirectRequest)))
            {
                throw new PlacetoPayException("Wrong class request");
            }

            return Carrier().Request((RedirectRequest)redirectRequest);
        }

        /// <summary>
        /// Reverse endpoint.
        /// </summary>
        /// <param name="transactionId">string</param>
        /// <returns>ReverseResponse</returns>
        public override ReverseResponse Reverse(string transactionId)
        {
            return Carrier().Reverse(transactionId);
        }
    }
}
