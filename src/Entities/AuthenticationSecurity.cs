using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>AuthenticationSecurity</c>
    /// </summary>
    public class AuthenticationSecurity
    {
        protected const string SEED = "seed";
        protected const string NONCE = "nonce";

        protected string seed;
        protected string nonce;

        /// <summary>
        /// AuthenticationSecurity constructor.
        /// </summary>
        public AuthenticationSecurity() { }

        /// <summary>
        /// AuthenticationSecurity constructor.
        /// </summary>
        /// <param name="data">string</param>
        public AuthenticationSecurity(string data) : this(JsonFormatter.ParseJObject(data)) { }

        /// <summary>
        /// AuthenticationSecurity constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public AuthenticationSecurity(JObject data)
        {
            this.Load<AuthenticationSecurity>(data, new JArray { SEED, NONCE });
        }

        /// <summary>
        /// AuthenticationSecurity constructor.
        /// </summary>
        /// <param name="seed">string</param>
        /// <param name="nonce">string</param>
        public AuthenticationSecurity(string seed, string nonce)
        {
            this.seed = seed;
            this.nonce = nonce;
        }

        /// <summary>
        /// Seed property.
        /// </summary>
        public string Seed
        {
            get { return seed; }
            set { seed = value; }
        }

        /// <summary>
        /// Nonce property.
        /// </summary>
        public string Nonce
        {
            get { return nonce; }
            set { nonce = value; }
        }
    }
}
