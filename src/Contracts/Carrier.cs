using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Messages;

namespace PlacetoPay.Redirection.Contracts
{
    /// <summary>
    /// Class <c>Carrier</c>
    /// </summary>
    public abstract class Carrier
    {
        protected const string AUTH = "auth";
        protected const string CONFIG = "config";

        protected Authentication auth;
        protected JObject config;

        /// <summary>
        /// Carrier constructor.
        /// </summary>
        /// <param name="auth">Authentication</param>
        /// <param name="config">JObject</param>
        public Carrier(Authentication auth, JObject config)
        {
            this.auth = auth;
            this.config = config;
        }

        /// <summary>
        /// Auth property.
        /// </summary>
        public Authentication Auth
        {
            get { return auth; }
            set { auth = value; }
        }

        /// <summary>
        /// Config property.
        /// </summary>
        public JObject Config
        {
            get { return config; }
            set { config = value; }
        }

        /// <summary>
        /// Collect endpoint.
        /// </summary>
        /// <param name="collectRequest">CollectRequest</param>
        /// <returns>RedirectInformation</returns>
        public abstract RedirectInformation Collect(CollectRequest collectRequest);

        /// <summary>
        /// Query endpoint.
        /// </summary>
        /// <param name="requestId">string</param>
        /// <returns>RedirectInformation</returns>
        public abstract RedirectInformation Query(string requestId);

        /// <summary>
        /// Request endpoint.
        /// </summary>
        /// <param name="redirectRequest">RedirectRequest</param>
        /// <returns>RedirectResponse</returns>
        public abstract RedirectResponse Request(RedirectRequest redirectRequest);

        /// <summary>
        /// Reverse endpoint.
        /// </summary>
        /// <param name="transactionId">string</param>
        /// <returns>ReverseResponse</returns>
        public abstract ReverseResponse Reverse(string transactionId);
    }
}
