using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>DispersionPayment</c>
    /// </summary>
    public class DispersionPayment : Payment
    {
        protected const string DISPERSION = "dispersion";

        protected List<Payment> dispersion;

        /// <summary>
        /// DispersionPayment constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public DispersionPayment(JObject data) : base(data)
        {
            dispersion = data.ContainsKey(DISPERSION) ? SetDispersion(data.GetValue(DISPERSION).ToObject<JArray>()) : null;
        }

        /// <summary>
        /// DispersionPayment constructor.
        /// </summary>
        /// <param name="data">string</param>
        public DispersionPayment(string data) : base(data)
        {
            JObject json = JObject.Parse(data);

            dispersion = json.ContainsKey(DISPERSION) ? SetDispersion(json.GetValue(DISPERSION).ToObject<JArray>()) : null;
        }

        /// <summary>
        /// DispersionPayment constructor.
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
        /// <param name="dispersion">List of Payments</param>
        public DispersionPayment(
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
            Instrument instrument,
            List<Payment> dispersion
            ) : base(
                reference,
                description,
                allowPartial,
                subscribe,
                agreement,
                agreementType,
                amount,
                recurring,
                shipping,
                items,
                fields,
                instrument
                )
        {
            this.dispersion = dispersion;
        }

        /// <summary>
        /// Dispersion property.
        /// </summary>
        public List<Payment> Dispersion
        {
            get { return dispersion; }
            set { dispersion = value; }
        }

        /// <summary>
        /// Set list of payments.
        /// </summary>
        /// <param name="payments">JArray</param>
        /// <returns>List</returns>
        public List<Payment> SetDispersion(JArray payments)
        {
            List<Payment> list = new List<Payment>();

            foreach (var payment in payments)
            {
                JObject entity = payment.ToObject<JObject>();

                entity.Add(new JProperty(REFERENCE, reference));
                entity.Add(new JProperty(DESCRIPTION, description));

                list.Add(new Payment(entity));
            }

            return list;
        }

        /// <summary>
        /// Convert dispersion list to json array.
        /// </summary>
        /// <returns>JArray</returns>
        private JArray DispersionToJArray()
        {
            JArray dispersions = new JArray();

            if (Dispersion != null)
            {
                foreach (var dispersion in Dispersion)
                {
                    dispersions.Add(dispersion.ToJsonObject());
                }
            }

            return dispersions;
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            JObject jsonBase = base.ToJsonObject();

            jsonBase.Merge(new JObject
            {
                { DISPERSION, DispersionToJArray() },
            }, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Replace
            });

            return JObjectFilter(jsonBase);
        }
    }
}
