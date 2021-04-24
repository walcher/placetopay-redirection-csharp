using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;
using System.Collections.Generic;

namespace PlacetoPay.Redirection.Messages
{
    /// <summary>
    /// Class <c>CollectRequest</c>
    /// </summary>
    public class CollectRequest : Entity
    {
        protected const string BUYER = "buyer";
        protected const string FIELDS = "fields";
        protected const string INSTRUMENT = "instrument";
        protected const string LOCALE = "locale";
        protected const string PAYER = "payer";
        protected const string PAYMENT = "payment";

        protected string locale = "es_CO";
        protected Person payer;
        protected Person buyer;
        protected Payment payment;
        protected Instrument instrument;
        protected List<NameValuePair> fields;

        /// <summary>
        /// CollectRequest constructor.
        /// </summary>
        public CollectRequest() { }

        /// <summary>
        /// CollectRequest constructor.
        /// </summary>
        /// <param name="data">string</param>
        public CollectRequest(string data) : this(JsonFormatter.ParseJObject(data)) { }

        /// <summary>
        /// CollectRequest constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public CollectRequest(JObject data)
        {
            if (data.ContainsKey(LOCALE))
            {
                locale = (string)data.GetValue(LOCALE);
            }

            if (data.ContainsKey(PAYER))
            {
                SetPayer(data.GetValue(PAYER).ToObject<JObject>());
            }

            if (data.ContainsKey(BUYER))
            {
                SetBuyer(data.GetValue(BUYER).ToObject<JObject>());
            }

            if (data.ContainsKey(PAYMENT))
            {
                SetPayment(data.GetValue(PAYMENT).ToObject<JObject>());
            }

            if (data.ContainsKey(INSTRUMENT))
            {
                SetInstrument(data.GetValue(INSTRUMENT).ToObject<JObject>());
            }

            if (data.ContainsKey(FIELDS))
            {
                this.SetFields<CollectRequest>(data.GetValue(FIELDS).ToObject<JArray>());
            }
        }

        /// <summary>
        /// CollectRequest constructor.
        /// </summary>
        /// <param name="payer">Person</param>
        /// <param name="buyer">Person</param>
        /// <param name="payment">Payment</param>
        /// <param name="instrument">Instrument</param>
        public CollectRequest(
            Person payer,
            Person buyer,
            Payment payment,
            Instrument instrument
            )
        {
            this.payer = payer;
            this.buyer = buyer;
            this.payment = payment;
            this.instrument = instrument;
        }

        /// <summary>
        /// CollectRequest constructor.
        /// </summary>
        /// <param name="locale">string</param>
        /// <param name="payer">Person</param>
        /// <param name="buyer">Person</param>
        /// <param name="payment">Payment</param>
        /// <param name="instrument">Instrument</param>
        public CollectRequest(
            string locale,
            Person payer,
            Person buyer,
            Payment payment,
            Instrument instrument
            )
        {
            this.locale = locale;
            this.payer = payer;
            this.buyer = buyer;
            this.payment = payment;
            this.instrument = instrument;
        }

        /// <summary>
        /// Locale property.
        /// </summary>
        public string Locale
        {
            get { return locale; }
            set { locale = value; }
        }

        /// <summary>
        /// Payer property.
        /// </summary>
        public Person Payer
        {
            get { return payer; }
            set { payer = value; }
        }

        /// <summary>
        /// Buyer property.
        /// </summary>
        public Person Buyer
        {
            get { return buyer; }
            set { buyer = value; }
        }

        /// <summary>
        /// Payment property.
        /// </summary>
        public Payment Payment
        {
            get { return payment; }
            set { payment = value; }
        }

        /// <summary>
        /// Instrument property.
        /// </summary>
        public Instrument Instrument
        {
            get { return instrument; }
            set { instrument = value; }
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
        /// Get language.
        /// </summary>
        /// <returns></returns>
        public string GetLanguage()
        {
            return locale.Substring(0, 2).ToUpper();
        }

        /// <summary>
        /// Get payment reference.
        /// </summary>
        /// <returns>string</returns>
        public string GetReference()
        {
            return Payment.Reference;
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject {
                { LOCALE, Locale },
                { PAYER, Payer?.ToJsonObject() },
                { BUYER, Buyer?.ToJsonObject() },
                { PAYMENT, Payment?.ToJsonObject() },
                { INSTRUMENT, Instrument?.ToJsonObject() },
                { FIELDS, this.FieldsToJArray<RedirectRequest>() },
            });
        }
    }
}
