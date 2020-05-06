using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Instrument</c>
    /// </summary>
    public class Instrument : Entity
    {
        protected Bank bank;
        protected Card card;
        protected string token;
        protected string pin;
        protected string password;

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            throw new NotImplementedException();
        }
    }
}
