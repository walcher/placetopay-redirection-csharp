using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using System;
using System.Collections.Generic;

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
        protected List<Item> items;
        protected List<NameValuePair> fields;
        protected Recurring recurring;
        protected string discount;
        protected string instrument;
        protected bool subscribe = false;
        protected string agreement;
        protected string agreementType;
        protected GDS gds;

        /// <summary>
        /// Payment constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Payment(JObject data)
        {
            Load(data, new JArray { "reference", "description", "allowPartial", "subscribe", "agreement", "agreementType" });

            amount = data.ContainsKey("amount") ? new Amount(data.GetValue("amount").ToObject<JObject>()) : null;
            recurring = data.ContainsKey("recurring") ? new Recurring(data.GetValue("recurring").ToObject<JObject>()) : null;
            shipping = data.ContainsKey("shipping") ? new Person(data.GetValue("shipping").ToObject<JObject>()) : null;
            items = data.ContainsKey("items") ? SetItem(data.GetValue("items").ToObject<JArray>()) : null;

            if (data.ContainsKey("fields"))
            {
                SetFields(data.GetValue("fields").ToObject<JArray>());
            }

            gds = data.ContainsKey("gds") ? new GDS(data.GetValue("gds").ToObject<JObject>()) : null;
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
        public GDS Gds
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
        public List<Item> Items
        {
            get { return items; }
            set { items = value; }
        }

        /// <summary>
        /// Recurring property.
        /// </summary>
        public Recurring Recurring
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
        /// Fields property.
        /// </summary>
        public List<NameValuePair> Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        /// <summary>
        /// Set list of items.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private List<Item> SetItem(JArray items)
        {
            List<Item> list = new List<Item>();

            foreach (var item in items)
            {
                JObject itemDetail = item.ToObject<JObject>();

                list.Add(new Item(itemDetail));
            }

            return list;
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
