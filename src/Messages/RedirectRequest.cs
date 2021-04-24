using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;
using System;
using System.Collections.Generic;

namespace PlacetoPay.Redirection.Messages
{
    /// <summary>
    /// Class <c>RedirectRequest</c>
    /// </summary>
    public class RedirectRequest : Entity
    {
        protected const string BUYER = "buyer";
        protected const string CANCEL_URL = "cancelUrl";
        protected const string CAPTURE_ADDRESS = "captureAddress";
        protected const string DATE_FORMAT = "yyyy-MM-ddTHH\\:mm\\:sszzz";
        protected const string EXPIRATION = "expiration";
        protected const string FIELDS = "fields";
        protected const string IP_ADDRESS = "ipAddress";
        protected const string LOCALE = "locale";
        protected const string NO_BUYER_FILL = "noBuyerFill";
        protected const string PAYER = "payer";
        protected const string PAYMENT = "payment";
        protected const string PAYMENT_METHOD = "paymentMethod";
        protected const string RETURN_URL = "returnUrl";
        protected const string SKIP_RESULT = "skipResult";
        protected const string SUBSCRIPTION = "subscription";
        protected const string USER_AGENT = "userAgent";

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
        public RedirectRequest() { }

        /// <summary>
        /// RedirectRequest constructor.
        /// </summary>
        /// <param name="data">string</param>
        public RedirectRequest(string data) : this(JsonFormatter.ParseJObject(data)) { }

        /// <summary>
        /// RedirectRequest constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public RedirectRequest(JObject data)
        {
            if (!data.ContainsKey(EXPIRATION))
            {
                expiration = (DateTime.Now).AddDays(+1).ToString(DATE_FORMAT);
            }

            this.Load<RedirectRequest>(data, new JArray { RETURN_URL, PAYMENT_METHOD, CANCEL_URL, IP_ADDRESS, USER_AGENT, EXPIRATION, CAPTURE_ADDRESS, SKIP_RESULT, NO_BUYER_FILL });

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

            if (data.ContainsKey(SUBSCRIPTION))
            {
                SetSubscription(data.GetValue(SUBSCRIPTION).ToObject<JObject>());
            }

            if (data.ContainsKey(FIELDS))
            {
                this.SetFields<RedirectRequest>(data.GetValue(FIELDS).ToObject<JArray>());
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
        /// Set locale data.
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>RedirectRequest</returns>
        public RedirectRequest SetLocale(string data)
        {
            locale = data;

            return this;
        }

        /// <summary>
        /// Set subscription data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>RedirectRequest</returns>
        public RedirectRequest SetSubscription(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new Subscription((JObject)data);
                }

                if (!(data.GetType() == typeof(Subscription)))
                {
                    data = null;
                }
            }            

            subscription = (Subscription)data;

            return this;
        }

        /// <summary>
        /// Set return url data.
        /// </summary>
        /// <param name="returnUrl">string</param>
        /// <returns>RedirectRequest</returns>
        public RedirectRequest SetReturnUrl(string returnUrl)
        {
            this.returnUrl = returnUrl;

            return this;
        }

        /// <summary>
        /// Set cancel url data.
        /// </summary>
        /// <param name="cancelUrl">string</param>
        /// <returns>RedirectRequest</returns>
        public RedirectRequest SetCancelUrl(string cancelUrl)
        {
            this.cancelUrl = cancelUrl;

            return this;
        }

        /// <summary>
        /// Set expiration data.
        /// </summary>
        /// <param name="expiration">string</param>
        /// <returns>RedirectRequest</returns>
        public RedirectRequest SetExpiration(string expiration)
        {
            this.expiration = expiration;

            return this;
        }

        /// <summary>
        /// Set user agent data.
        /// </summary>
        /// <param name="userAgent">string</param>
        /// <returns>RedirectRequest</returns>
        public RedirectRequest SetUserAgent(string userAgent)
        {
            this.userAgent = userAgent;

            return this;
        }

        /// <summary>
        /// Set ip address data.
        /// </summary>
        /// <param name="ipAddress">string</param>
        /// <returns>RedirectRequest</returns>
        public RedirectRequest SetIpAddress(string ipAddress)
        {
            this.ipAddress = ipAddress;

            return this;
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
        /// Get payment/subscription reference.
        /// </summary>
        /// <returns>string</returns>
        public string GetReference()
        {
            if (Payment != null)
            {
                return Payment.Reference;
            }

            return Subscription.Reference;
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject
            {
                { LOCALE, Locale },
                { PAYER, Payer?.ToJsonObject() },
                { BUYER, Buyer?.ToJsonObject() },
                { PAYMENT, Payment?.ToJsonObject() },
                { SUBSCRIPTION, Subscription?.ToJsonObject() },
                { FIELDS, this.FieldsToJArray<RedirectRequest>() },
                { RETURN_URL, ReturnUrl },
                { PAYMENT_METHOD, PaymentMethod },
                { CANCEL_URL, CancelUrl },
                { IP_ADDRESS, IpAddress },
                { USER_AGENT, UserAgent },
                { EXPIRATION, Expiration },
                { CAPTURE_ADDRESS, CaptureAddress },
                { SKIP_RESULT, SkipResult },
                { NO_BUYER_FILL, NoBuyerFill },
            });
        }
    }
}
