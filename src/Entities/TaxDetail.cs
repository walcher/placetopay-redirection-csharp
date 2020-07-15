using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>TaxDetail</c>
    /// </summary>
    public class TaxDetail : Entity
    {
        protected string kind;
        protected double amount;
        protected double baseAmount;

        /// <summary>
        /// TaxDetail constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public TaxDetail(JObject data)
        {
            this.Load<TaxDetail>(data, new JArray { "kind", "amount", "base" });
        }

        /// <summary>
        /// TaxDetail constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public TaxDetail(string data)
        {
            JObject json = JObject.Parse(data);

            this.Load<TaxDetail>(json, new JArray { "kind", "amount", "base" });
        }

        /// <summary>
        /// TaxDetail constructor.
        /// </summary>
        /// <param name="kind">string</param>
        /// <param name="amount">double</param>
        /// <param name="baseAmount">double</param>
        public TaxDetail(string kind, double amount, double baseAmount)
        {
            this.kind = kind;
            this.amount = amount;
            this.baseAmount = baseAmount;
        }


        /// <summary>
        /// Kind property.
        /// </summary>
        public string Kind
        {
            get { return kind; }
            set { kind = value; }
        }

        /// <summary>
        /// Amount property.
        /// </summary>
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        /// <summary>
        /// Base property.
        /// </summary>
        public double Base
        {
            get { return baseAmount; }
            set { baseAmount = value; }
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
