using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace PlacetoPay.Redirection.Message
{
    /// <summary>
    /// Class <c>RedirectRequest</c>
    /// </summary>
    public class RedirectRequest : Entity
    {
        protected string locale = "es_CO";
        protected Person payer;
        protected Person buyer;
        protected Payment payment;
        protected Subscription subscription;
        protected string returnUrl;
        protected string paymentMethod;
        protected string cancelUrl;
        protected string ipAddress;
        protected string userAgent;
        protected string expiration;
        protected bool captureAddress;
        protected bool skipResult = false;
        protected bool noBuyerFill = false;
        protected List<NameValuePair> fields;

        /// <summary>
        /// RedirectRequest constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public RedirectRequest(JObject data)
        {
            if (!data.ContainsKey("expiration"))
            {
                expiration = (DateTime.Now).AddDays(+1).ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            }

            this.Load<RedirectRequest>(data, new JArray { "returnUrl", "paymentMethod", "cancelUrl", "ipAddress", "userAgent", "expiration", "captureAddress", "skipResult", "noBuyerFill" });

            if (data.ContainsKey("locale"))
            {
                locale = (string)data.GetValue("locale");
            }

            payer = data.ContainsKey("payer") ? new Person(data.GetValue("payer").ToObject<JObject>()) : null;
            buyer = data.ContainsKey("buyer") ? new Person(data.GetValue("buyer").ToObject<JObject>()) : null;
            payment = data.ContainsKey("payment") ? new Payment(data.GetValue("payment").ToObject<JObject>()) : null;
            subscription = data.ContainsKey("subscription") ? new Subscription(data.GetValue("subscription").ToObject<JObject>()) : null;

            if (data.ContainsKey("fields"))
            {
                this.SetFields<RedirectRequest>(data.GetValue("fields").ToObject<JArray>());
            }
        }

        /// <summary>
        /// RedirectRequest constructor.
        /// </summary>
        /// <param name="data">string</param>
        public RedirectRequest(string data)
        {
            JsonReader reader = new JsonTextReader(new StringReader(data))
            {
                DateParseHandling = DateParseHandling.None
            };

            JObject json = JObject.Load(reader);

            if (!json.ContainsKey("expiration"))
            {
                expiration = (DateTime.Now).AddDays(+1).ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            }

            this.Load<RedirectRequest>(json, new JArray { "returnUrl", "paymentMethod", "cancelUrl", "ipAddress", "userAgent", "expiration", "captureAddress", "skipResult", "noBuyerFill" });

            if (json.ContainsKey("locale"))
            {
                locale = (string)json.GetValue("locale");
            }

            payer = json.ContainsKey("payer") ? new Person(json.GetValue("payer").ToObject<JObject>()) : null;
            buyer = json.ContainsKey("buyer") ? new Person(json.GetValue("buyer").ToObject<JObject>()) : null;
            payment = json.ContainsKey("payment") ? new Payment(json.GetValue("payment").ToObject<JObject>()) : null;
            subscription = json.ContainsKey("subscription") ? new Subscription(json.GetValue("subscription").ToObject<JObject>()) : null;

            if (json.ContainsKey("fields"))
            {
                this.SetFields<RedirectRequest>(json.GetValue("fields").ToObject<JArray>());
            }
        }

        /// <summary>
        /// RedirectRequest constructor.
        /// </summary>
        /// <param name="payer">Person</param>
        /// <param name="buyer">Person</param>
        /// <param name="payment">Payment</param>
        /// <param name="returnUrl">string</param>
        /// <param name="paymentMethod">string</param>
        public RedirectRequest(
            Person payer,
            Person buyer,
            Payment payment,
            string returnUrl,
            string paymentMethod
            )
        {
            this.payer = payer;
            this.buyer = buyer;
            this.payment = payment;
            this.returnUrl = returnUrl;
            this.paymentMethod = paymentMethod;
        }

        /// <summary>
        /// RedirectRequest constructor.
        /// </summary>
        /// <param name="payer">Person</param>
        /// <param name="buyer">Person</param>
        /// <param name="payment">Payment</param>
        /// <param name="subscription">Subscription</param>
        /// <param name="returnUrl">string</param>
        /// <param name="paymentMethod">string</param>
        /// <param name="cancelUrl">string</param>
        /// <param name="ipAddress">string</param>
        /// <param name="userAgent">string</param>
        /// <param name="expiration">string</param>
        /// <param name="captureAddress">bool</param>
        /// <param name="noBuyerFill">bool</param>
        /// <param name="skipResult">bool</param>
        /// <param name="fields">List</param>
        public RedirectRequest(
            Person payer,
            Person buyer,
            Payment payment,
            Subscription subscription,
            string returnUrl,
            string paymentMethod,
            string cancelUrl,
            string ipAddress,
            string userAgent,
            string expiration,
            bool captureAddress,
            bool noBuyerFill,
            bool skipResult,
            List<NameValuePair> fields
            )
        {
            this.payer = payer;
            this.buyer = buyer;
            this.payment = payment;
            this.subscription = subscription;
            this.returnUrl = returnUrl;
            this.paymentMethod = paymentMethod;
            this.cancelUrl = cancelUrl;
            this.ipAddress = ipAddress;
            this.userAgent = userAgent;
            this.expiration = expiration;
            this.captureAddress = captureAddress;
            this.noBuyerFill = noBuyerFill;
            this.skipResult = skipResult;
            this.fields = fields;
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
        /// ReturnUrl property.
        /// </summary>
        public string ReturnUrl
        {
            get { return returnUrl; }
            set { returnUrl = value; }
        }

        /// <summary>
        /// PaymentMethod property.
        /// </summary>
        public string PaymentMethod
        {
            get { return paymentMethod; }
            set { paymentMethod = value; }
        }

        /// <summary>
        /// CancelUrl property.
        /// </summary>
        public string CancelUrl
        {
            get { return cancelUrl; }
            set { cancelUrl = value; }
        }

        /// <summary>
        /// IpAddress property.
        /// </summary>
        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        /// <summary>
        /// UserAgent property.
        /// </summary>
        public string UserAgent
        {
            get { return userAgent; }
            set { userAgent = value; }
        }

        /// <summary>
        /// Expiration property.
        /// </summary>
        public string Expiration
        {
            get { return expiration; }
            set { expiration = value; }
        }

        /// <summary>
        /// CaptureAddress property.
        /// </summary>
        public bool CaptureAddress
        {
            get { return captureAddress; }
            set { captureAddress = value; }
        }

        /// <summary>
        /// SkipResult property.
        /// </summary>
        public bool SkipResult
        {
            get { return skipResult; }
            set { skipResult = value; }
        }

        /// <summary>
        /// NoBuyerFill property.
        /// </summary>
        public bool NoBuyerFill
        {
            get { return noBuyerFill; }
            set { noBuyerFill = value; }
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
        /// Subscription property.
        /// </summary>
        public Subscription Subscription
        {
            get { return subscription; }
            set { subscription = value; }
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
        public string Language()
        {
            return locale.Substring(0, 2).ToUpper();
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return new JObject
            {
                { "locale", locale }
            };
        }
    }
}
