using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Exceptions;
using PlacetoPay.Redirection.Messages;
using RestSharp;
using System;

namespace PlacetoPay.Redirection.Carriers
{
    /// <summary>
    /// Class <c>RestCarrier</c>
    /// </summary>
    public class RestCarrier : Carrier
    {
        protected const string URL = "url";
        protected new const string AUTH = "auth";
        protected const string TIMEOUT = "timeout";
        protected const string CONNECT_TIMEOUT = "connect_timeout";

        protected string baseUrl;

        /// <summary>
        /// RestCarrier constructor.
        /// </summary>
        /// <param name="auth">Authentication</param>
        /// <param name="config">JObject</param>
        public RestCarrier(Authentication auth, JObject config) : base(auth, config)
        {
            if (!config.ContainsKey(URL))
            {
                throw new PlacetoPayException("Base URL not found for this");
            }

            baseUrl = config.GetValue(URL).ToString();
        }

        /// <summary>
        /// Make rest request.
        /// </summary>
        /// <param name="method">string</param>
        /// <param name="uri">string</param>
        /// <param name="arguments">JObject</param>
        /// <returns>string</returns>
        private string MakeRequest(string method, string uri, JObject arguments)
        {
            try
            {
                RestClient client = new RestClient(baseUrl);
                RestRequest request = null;
                IRestResponse response = null;

                if (config.ContainsKey(TIMEOUT))
                {
                    client.Timeout = (int)config.GetValue(TIMEOUT);
                }

                if (config.ContainsKey(CONNECT_TIMEOUT))
                {
                    client.ReadWriteTimeout = (int)config.GetValue(CONNECT_TIMEOUT);
                }

                arguments.Merge(new JObject
                {
                    { AUTH, Auth.ToJsonObject() }
                }, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Union
                });

                if (method == "POST")
                {
                    request = new RestRequest(uri, Method.POST);

                    request.AddParameter("application/json", arguments, ParameterType.RequestBody);

                    response = client.Execute(request);
                }
                else if (method == "GET")
                {
                    request = new RestRequest(uri, Method.GET);

                    response = client.Execute(request);
                }
                else if (method == "PUT")
                {
                    request = new RestRequest(uri, Method.PUT);

                    request.AddParameter("application/json", arguments, ParameterType.RequestBody);

                    response = client.Execute(request);
                }
                else
                {
                    throw new PlacetoPayException("No valid method for this request");
                }

                if (!response.IsSuccessful && response.ErrorMessage != null)
                {
                    throw new PlacetoPayException(response.ErrorException.Message);
                }
                else
                {
                    return response.Content;
                }
            }
            catch (Exception ex)
            {
                return new JObject
                {
                    {
                        "status", new JObject
                        {
                            { "status", Status.ST_ERROR },
                            { "reason", "WR" },
                            { "message", PlacetoPayException.ReadException(ex) },
                            { "date", (DateTime.Now).ToString("yyyy-MM-ddTHH\\:mm\\:sszzz") },
                        }
                    },
                }.ToString();
            }
        }

        /// <summary>
        /// Make collect endpoint.
        /// </summary>
        /// <param name="collectRequest">CollectRequest</param>
        /// <returns>RedirectInformation</returns>
        public override RedirectInformation Collect(CollectRequest collectRequest)
        {
            string result = MakeRequest("POST", "api/collect", collectRequest.ToJsonObject());

            return new RedirectInformation(result);
        }

        /// <summary>
        /// Make query endpoint.
        /// </summary>
        /// <param name="requestId">string</param>
        /// <returns>RedirectInformation</returns>
        public override RedirectInformation Query(string requestId)
        {
            string result = MakeRequest("POST", $"api/session/{requestId}", new JObject());

            return new RedirectInformation(result);
        }

        /// <summary>
        /// Make request endpoint.
        /// </summary>
        /// <param name="redirectRequest">RedirectRequest</param>
        /// <returns>RedirectResponse</returns>
        public override RedirectResponse Request(RedirectRequest redirectRequest)
        {
            string result = MakeRequest("POST", "api/session", redirectRequest.ToJsonObject());

            return new RedirectResponse(result);
        }

        /// <summary>
        /// Make reverse endpoint.
        /// </summary>
        /// <param name="transactionId">string</param>
        /// <returns>ReverseResponse</returns>
        public override ReverseResponse Reverse(string transactionId)
        {
            string result = MakeRequest("POST", "api/reverse", new JObject { { "internalReference", transactionId } });

            return new ReverseResponse(result);
        }
    }
}
