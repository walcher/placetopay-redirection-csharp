using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using System.Collections.Generic;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Payment</c>
    /// </summary>
    public class Payment : Entity
    {
        protected const string AGREEMENT = "agreement";
        protected const string AGREEMENT_TYPE = "agreementType";
        protected const string ALLOW_PARTIAL = "allowPartial";
        protected const string AMOUNT = "amount";
        protected const string DESCRIPTION = "description";
        protected const string DISCOUNT = "discount";
        protected const string FIELDS = "fields";
        protected const string GDS = "gds";
        protected const string INSTRUMENT = "instrument";
        protected const string ITEMS = "items";
        protected const string RECURRING = "recurring";
        protected const string REFERENCE = "reference";
        protected const string SHIPPING = "shipping";
        protected const string SUBSCRIBE = "subscribe";

        protected string reference;
        protected string description;
        protected Amount amount;
        protected bool allowPartial = false;
        protected Person shipping;
        protected List<Item> items;
        protected List<NameValuePair> fields;
        protected Recurring recurring;
        protected string discount;
        protected Instrument instrument;
        protected bool subscribe = false;
        protected string agreement;
        protected string agreementType;
        protected GDS gds;

        /// <summary>
        /// Payment constructor.
        /// </summary>
        public Payment() { }

        /// <summary>
        /// Payment constructor.
        /// </summary>
        /// <param name="data"></param>
        public Payment(string data) : this(JObject.Parse(data)) { }

        /// <summary>
        /// Payment constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Payment(JObject data)
        {
            this.Load<Payment>(data, new JArray { REFERENCE, DESCRIPTION, ALLOW_PARTIAL, SUBSCRIBE, AGREEMENT, AGREEMENT_TYPE });

            if (data.ContainsKey(AMOUNT))
            {
                SetAmount(data.GetValue(AMOUNT).ToObject<JObject>());
            }

            if (data.ContainsKey(RECURRING))
            {
                SetRecurring(data.GetValue(RECURRING).ToObject<JObject>());
            }

            if (data.ContainsKey(SHIPPING))
            {
                SetShipping(data.GetValue(SHIPPING).ToObject<JObject>());
            }

            if (data.ContainsKey(ITEMS))
            {
                SetItem(data.GetValue(ITEMS).ToObject<JArray>());
            }

            if (data.ContainsKey(FIELDS))
            {
                this.SetFields<Payment>(data.GetValue(FIELDS).ToObject<JArray>());
            }

            if (data.ContainsKey(GDS))
            {
                SetGDS(data.GetValue(GDS).ToObject<JObject>());
            }
        }

        /// <summary>
        /// Payment constructor.
        /// </summary>
        /// <param name="reference">string</param>
        /// <param name="description">string</param>
        /// <param name="allowPartial">bool</param>
        /// <param name="subscribe">bool</param>
        /// <param name="agreement">string</param>
        /// <param name="agreementType">string</param>
        /// <param name="amount">Amount</param>
        /// <param name="recurring">Recurring</param>
        /// <param name="shipping">Person</param>
        /// <param name="items">List of Item</param>
        /// <param name="fields">List of NameValuePair</param>
        /// <param name="instrument">Instrument</param>
        public Payment(
            string reference,
            string description,
            bool allowPartial,
            bool subscribe,
            string agreement,
            string agreementType,
            Amount amount,
            Recurring recurring,
            Person shipping,
            List<Item> items,
            List<NameValuePair> fields,
            Instrument instrument
            )
        {
            this.reference = reference;
            this.description = description;
            this.allowPartial = allowPartial;
            this.subscribe = subscribe;
            this.agreement = agreement;
            this.agreementType = agreementType;
            this.amount = amount;
            this.recurring = recurring;
            this.shipping = shipping;
            this.items = items;
            this.fields = fields;
            this.instrument = instrument;
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
        /// Set reference property data.
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>Payment</returns>
        public Payment SetReference(string data)
        {
            reference = data;

            return this;
        }

        /// <summary>
        /// Set description property data.
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>Payment</returns>
        public Payment SetDescription(string data)
        {
            description = data;

            return this;
        }

        /// <summary>
        /// Set gds property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>Payment</returns>
        public Payment SetGDS(object data)
        {
            if (data != null && data.GetType() == typeof(JObject))
            {
                data = new GDS((JObject)data);
            }

            gds = (GDS)data;

            return this;
        }

        /// <summary>
        /// Set list of items.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>Payment</returns>
        private Payment SetItem(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    JObject item = (JObject)data;

                    if (item.ContainsKey(ITEMS))
                    {
                        data = item.GetValue(ITEMS).ToObject<JArray>();
                    }
                }

                if (data.GetType() == typeof(JArray))
                {
                    List<Item> list = new List<Item>();

                    foreach (var item in (JArray)data)
                    {
                        JObject itemDetail = item.ToObject<JObject>();

                        list.Add(new Item(itemDetail));
                    }

                    data = list;
                }

                items = (List<Item>)data;
            }

            return this;
        }

        /// <summary>
        /// Convert items list to json array.
        /// </summary>
        /// <returns>JArray</returns>
        private JArray ItemsToJArray()
        {
            JArray items = new JArray();

            if (Items != null)
            {
                foreach (var item in Items)
                {
                    items.Add(item.ToJsonObject());
                }
            }

            return items;
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject {
                { REFERENCE, Reference },
                { DESCRIPTION, Description },
                { AMOUNT, Amount?.ToJsonObject() },
                { ALLOW_PARTIAL, AllowPartial },
                { SHIPPING, Shipping?.ToJsonObject() },
                { ITEMS, ItemsToJArray() },
                { RECURRING, Recurring?.ToJsonObject() },
                { SUBSCRIBE, Subscribe },
                { FIELDS, this.FieldsToJArray<Payment>() },
                { AGREEMENT, Agreement },
                { AGREEMENT_TYPE, AgreementType },
                { GDS, Gds?.ToJsonObject() },
            });
        }
    }
}
