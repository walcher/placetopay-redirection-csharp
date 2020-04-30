using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Amount</c>
    /// </summary>
    public class Amount : Entity
    {
        protected string taxes;
        protected string details;
        protected string taxAmount;

        /// <summary>
        /// Amount constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Amount(JObject data)
        {
            taxes = data.ContainsKey("taxes") ? data.GetValue("taxes").ToString() : null;
            details = data.ContainsKey("details") ? data.GetValue("details").ToString() : null;
        }

        /// <summary>
        /// Taxes property.
        /// </summary>
        public string Taxes
        {
            get { return taxes; }
            set { taxes = value; }
        }

        /// <summary>
        /// Details property;
        /// </summary>
        public string Details
        {
            get { return details; }
            set { details = value; }
        }

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
