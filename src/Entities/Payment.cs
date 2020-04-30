using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Payment</c>
    /// </summary>
    public class Payment : Entity
    {
        protected string reference;
        protected string description;
        protected Amount amount;
        protected bool allowPartial = false;
        protected Person shipping;
        protected string items;
        protected string fields;
        protected string recurring;
        protected string discount;
        protected string instrument;
        protected bool subscribe = false;
        protected string agreement;
        protected string agreementType;
        protected string gds;

        /// <summary>
        /// Payment constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Payment(JObject data)
        {
            Load(data, new JArray { "reference", "description", "allowPartial", "subscribe", "items", "agreement", "agreementType" });

            amount = data.ContainsKey("amount") ? new Amount(data.GetValue("amount").ToObject<JObject>()) : null;
            recurring = data.ContainsKey("recurring") ? data.GetValue("recurring").ToString() : null;
            shipping = data.ContainsKey("shipping") ? new Person(data.GetValue("shipping").ToObject<JObject>()) : null;
            items = data.ContainsKey("items") ? data.GetValue("items").ToString() : null;
            fields = data.ContainsKey("fields") ? data.GetValue("fields").ToString() : null;
            gds = data.ContainsKey("gds") ? data.GetValue("gds").ToString() : null;
        }

        /// <summary>
        /// Reference property.
        /// </summary>
        public string Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        /// <summary>
        /// Description property.
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Amount property.
        /// </summary>
        public Amount Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        /// <summary>
        /// Agreement property.
        /// </summary>
        public string Agreement
        {
            get { return agreement; }
            set { agreement = value; }
        }

        /// <summary>
        /// AgreementType property.
        /// </summary>
        public string AgreementType
        {
            get { return agreementType; }
            set { agreementType = value; }
        }

        /// <summary>
        /// Gds property.
        /// </summary>
        public string Gds
        {
            get { return gds; }
            set { gds = value; }
        }

        /// <summary>
        /// AllowPartial property.
        /// </summary>
        public bool AllowPartial
        {
            get { return allowPartial; }
            set { allowPartial = value; }
        }

        /// <summary>
        /// Shipping property.
        /// </summary>
        public Person Shipping
        {
            get { return shipping; }
            set { shipping = value; }
        }

        /// <summary>
        /// Items property.
        /// </summary>
        public string Items
        {
            get { return items; }
            set { items = value; }
        }

        /// <summary>
        /// Recurring property.
        /// </summary>
        public string Recurring
        {
            get { return recurring; }
            set { recurring = value; }
        }

        /// <summary>
        /// Subscribe property.
        /// </summary>
        public bool Subscribe
        {
            get { return subscribe; }
            set { subscribe = value; }
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
