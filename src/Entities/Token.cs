using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Token</c>
    /// </summary>
    public class Token : Entity
    {
        protected string status;
        protected string token;
        protected string subtoken;
        protected string franchise;
        protected string franchiseName;
        protected string issuerName;
        protected string lastDigits;
        protected string validUntil;
        protected string cvv;
        protected int installments;

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
