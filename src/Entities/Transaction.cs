using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;
using System.Collections.Generic;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Transaction</c> get parsed transaction data.
    /// </summary>
    public class Transaction : Entity
    {
        protected const string STATUS = "status";
        protected const string REFERENCE = "reference";
        protected const string INTERNAL_REFERENCE = "internalReference";
        protected const string PAYMENT_METHOD = "paymentMethod";
        protected const string PAYMENT_METHOD_NAME = "paymentMethodName";
        protected const string ISSUER_NAME = "issuerName";
        protected const string DISCOUNT = "discount";
        protected const string AMOUNT = "amount";
        protected const string AUTHORIZATION = "authorization";
        protected const string RECEIPT = "receipt";
        protected const string FRANCHISE = "franchise";
        protected const string REFUNDED = "refunded";
        protected const string PROCESSOR_FIELDS = "processorFields";
        protected const string ITEM = "item";

        protected Status status;
        protected string reference;
        protected int internalReference;
        protected string paymentMethod;
        protected string paymentMethodName;
        protected string issuerName;
        protected Discount discount;
        protected AmountConversion amount;
        protected string authorization;
        protected long receipt;
        protected string franchise;
        protected bool refunded = false;
        protected List<NameValuePair> processorFields;

        /// <summary>
        /// Initializes a new instance of the Transaction class.
        /// </summary>
        public Transaction() { }

        /// <summary>
        /// Initializes a new instance of the Transaction class.
        /// </summary>
        /// <param name="data">string data.</param>
        public Transaction(string data) : this(JObject.Parse(data)) { }

        /// <summary>
        /// Initializes a new instance of the Transaction class.
        /// </summary>
        /// <param name="data">json object data.</param>
        public Transaction(JObject data)
        {
            this.Load<Transaction>(data, new JArray { REFERENCE, INTERNAL_REFERENCE, PAYMENT_METHOD, PAYMENT_METHOD_NAME, ISSUER_NAME, AUTHORIZATION, RECEIPT, FRANCHISE, REFUNDED });

            if (data.ContainsKey(STATUS))
            {
                SetStatus(data.GetValue(STATUS).ToObject<JObject>());
            }

            if (data.ContainsKey(AMOUNT))
            {
                SetAmount(data.GetValue(AMOUNT).ToObject<JObject>());
            }

            if (data.ContainsKey(PROCESSOR_FIELDS))
            {
                SetProcessorFields(JsonConvert.DeserializeObject(data.GetValue(PROCESSOR_FIELDS).ToString()));
            }

            if (data.ContainsKey(DISCOUNT))
            {
                SetDiscount(data.GetValue(DISCOUNT).ToObject<JObject>());
            }
        }

        /// <summary>
        /// Initializes a new instance of the Transaction class.
        /// </summary>
        /// <param name="status">Status onject.</param>
        /// <param name="reference">string reference.</param>
        /// <param name="internalReference">int internal reference.</param>
        /// <param name="paymentMethod">string payment method.</param>
        /// <param name="paymentMethodName">string paymen method name.</param>
        /// <param name="issuerName">string issuer name.</param>
        /// <param name="amount">AmountConversion object.</param>
        /// <param name="authorization">string authorization code.</param>
        /// <param name="receipt">long receip number.</param>
        /// <param name="franchise">string franchise.</param>
        /// <param name="refunded">bool refunded.</param>
        /// <param name="processorFields">List of NameValuePair objects.</param>
        public Transaction(
            Status status,
            string reference,
            int internalReference,
            string paymentMethod,
            string paymentMethodName,
            string issuerName,
            AmountConversion amount,
            string authorization,
            long receipt,
            string franchise,
            bool refunded,
            List<NameValuePair> processorFields
            )
        {
            this.status = status;
            this.reference = reference;
            this.internalReference = internalReference;
            this.paymentMethod = paymentMethod;
            this.paymentMethodName = paymentMethodName;
            this.issuerName = issuerName;
            this.amount = amount;
            this.authorization = authorization;
            this.receipt = receipt;
            this.franchise = franchise;
            this.refunded = refunded;
            this.processorFields = processorFields;
        }

        /// <summary>
        /// Status property.
        /// </summary>
        public Status Status
        {
            get { return status; }
            set { status = value; }
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
        /// InternalReference property.
        /// </summary>
        public int InternalReference
        {
            get { return internalReference; }
            set { internalReference = value; }
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
        /// PaymentMethodName property.
        /// </summary>
        public string PaymentMethodName
        {
            get { return paymentMethodName; }
            set { paymentMethodName = value; }
        }

        /// <summary>
        /// IssuerName property.
        /// </summary>
        public string IssuerName
        {
            get { return issuerName; }
            set { issuerName = value; }
        }

        /// <summary>
        /// Discount property.
        /// </summary>
        public Discount Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        /// <summary>
        /// Amount property.
        /// </summary>
        public AmountConversion Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        /// <summary>
        /// Authorization property.
        /// </summary>
        public string Authorization
        {
            get { return authorization; }
            set { authorization = value; }
        }

        /// <summary>
        /// Receipt property.
        /// </summary>
        public long Receipt
        {
            get { return receipt; }
            set { receipt = value; }
        }

        /// <summary>
        /// Franchise property.
        /// </summary>
        public string Franchise
        {
            get { return franchise; }
            set { franchise = value; }
        }

        /// <summary>
        /// Refunded property.
        /// </summary>
        public bool Refunded
        {
            get { return refunded; }
            set { refunded = value; }
        }

        /// <summary>
        /// ProcessorFields property.
        /// </summary>
        public List<NameValuePair> ProcessorFields
        {
            get { return processorFields; }
            set { processorFields = value; }
        }

        /// <summary>
        /// Set amount property data.
        /// </summary>
        /// <param name="data">amount object data.</param>
        /// <returns>Transaction current instance.</returns>
        public new Transaction SetAmount(object data)
        {
            if (data.GetType() == typeof(JObject))
            {
                data = new AmountConversion((JObject)data);
            }

            if (!(data.GetType() == typeof(AmountConversion)))
            {
                data = null;
            }

            amount = (AmountConversion)data;

            return this;
        }

        /// <summary>
        /// Set processor fields property data.
        /// </summary>
        /// <param name="data">processor fields data, can be JObject or JArray.</param>
        /// <returns>Transaction current instance.</returns>
        public Transaction SetProcessorFields(object data)
        {
            processorFields = new List<NameValuePair>();

            if (data.GetType() == typeof(JObject))
            {
                JObject item = (JObject)data;

                if (item.ContainsKey(ITEM))
                {
                    data = JsonConvert.DeserializeObject(item.GetValue(ITEM).ToString());

                    if (data.GetType() == typeof(JObject))
                    {
                        data = new JArray(data);
                    }
                }
            }

            if (data.GetType() == typeof(JArray))
            {
                JArray fields = (JArray)data;

                foreach (JObject nvp in fields)
                {
                    processorFields.Add(new NameValuePair(nvp));
                }
            }

            return this;
        }

        /// <summary>
        /// Set discount property data.
        /// </summary>
        /// <param name="data">discount object data.</param>
        /// <returns>Transaction current instance.</returns>
        public Transaction SetDiscount(object data)
        {
            if (data.GetType() == typeof(JObject))
            {
                data = new Discount((JObject)data);
            }

            if (!(data.GetType() == typeof(Discount)))
            {
                data = null;
            }

            discount = (Discount)data;

            return this;
        }

        /// <summary>
        /// Check if status is successful.
        /// </summary>
        /// <returns>true or false, depends on transaction status.</returns>
        public bool IsSuccessful()
        {
            return Status != null && Status.StatusText != Status.ST_ERROR;
        }

        /// <summary>
        /// Check if status is approved.
        /// </summary>
        /// <returns>true or false, depends on transaction status.</returns>
        public bool IsApproved()
        {
            return Status != null && Status.StatusText == Status.ST_APPROVED;
        }

        /// <summary>
        /// Set amount base data.
        /// </summary>
        /// <param name="data">AmountBase object data.</param>
        /// <returns>Transaction current instance.</returns>
        public Transaction SetAmountBase(object data)
        {
            if (data.GetType() == typeof(JObject))
            {
                data = new AmountBase((JObject)data);
            }

            if (!(data.GetType() == typeof(AmountBase)))
            {
                data = null;
            }

            amount = new AmountConversion().SetAmountBase(data);

            return this;
        }

        /// <summary>
        /// Conver processor field data to JArray
        /// </summary>
        /// <returns>list of processor fields.</returns>
        public JArray ProcessorFieldsToJArray()
        {
            JArray processorFields = new JArray();

            if (ProcessorFields != null)
            {
                foreach (var pair in ProcessorFields)
                {
                    processorFields.Add(pair.ToJsonObject());
                }
            }

            return processorFields;
        }

        /// <summary>
        /// Parses the processorFields as a key value JObject.
        /// </summary>
        /// <returns>additional data object.</returns>
        public JObject AdditionalData()
        {
            if (ProcessorFields != null)
            {
                JObject data = new JObject();

                foreach (var pair in ProcessorFields)
                {
                    data.Add(pair.Keyword, pair.Value);
                }

                return data;
            }

            return new JObject();
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>parsed data as JObject.</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(
                NumberFormatter.NormalizeNumber(new JObject {
                    { STATUS, Status?.ToJsonObject() },
                    { INTERNAL_REFERENCE, InternalReference },
                    { PAYMENT_METHOD, PaymentMethod },
                    { PAYMENT_METHOD_NAME, PaymentMethodName },
                    { ISSUER_NAME, IssuerName },
                    { AMOUNT, Amount?.ToJsonObject() },
                    { AUTHORIZATION, Authorization },
                    { REFERENCE, Reference },
                    { RECEIPT, Receipt },
                    { FRANCHISE, Franchise },
                    { REFUNDED, Refunded },
                    { DISCOUNT, Discount?.ToJsonObject() },
                    { PROCESSOR_FIELDS, ProcessorFieldsToJArray() },
                })
            );
        }
    }
}
