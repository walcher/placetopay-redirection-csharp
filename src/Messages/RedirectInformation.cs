using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace PlacetoPay.Redirection.Messages
{
    /// <summary>
    /// Class <c>RedirectInformation</c> get parsed response data from PlacetoPay service.
    /// </summary>
    public class RedirectInformation : Entity
    {
        protected const string REQUEST_ID = "requestId";
        protected const string STATUS = "status";
        protected const string REQUEST = "request";
        protected const string PAYMENT = "payment";
        protected const string SUBSCRIPTION = "subscription";
        protected const string TRANSACTION = "transaction";

        public int requestId;
        public RedirectRequest request;
        public List<Transaction> payment;
        public SubscriptionInformation subscription;
        protected Status status;

        /// <summary>
        /// Initializes a new instance of the RedirectInformation class.
        /// </summary>
        public RedirectInformation() { }

        /// <summary>
        /// Initializes a new instance of the RedirectInformation class.
        /// </summary>
        /// <param name="data">string data.</param>
        public RedirectInformation(string data) : this(JsonFormatter.ParseJObject(data)) { }

        /// <summary>
        /// Initializes a new instance of the RedirectInformation class.
        /// </summary>
        /// <param name="data">json object data.</param>
        public RedirectInformation(JObject data)
        {
            this.Load<Notification>(data, new JArray { REQUEST_ID });

            if (data.ContainsKey(STATUS))
            {
                SetStatus(data.GetValue(STATUS).ToObject<JObject>());
            }

            if (data.ContainsKey(REQUEST))
            {
                SetRequest(data.GetValue(REQUEST).ToObject<JObject>());
            }

            if (data.ContainsKey(PAYMENT))
            {
                SetPayment(JsonConvert.DeserializeObject(data.GetValue(PAYMENT).ToString()));
            }

            if (data.ContainsKey(SUBSCRIPTION))
            {
                SetSubscription(data.GetValue(SUBSCRIPTION).ToObject<JObject>());
            }
        }

        /// <summary>
        /// Initializes a new instance of the RedirectInformation class.
        /// </summary>
        /// <param name="requestId">int request id.</param>
        /// <param name="request">RedirectRequest object.</param>
        /// <param name="payment">List of Transaction objects.</param>
        /// <param name="subscription">SubscriptionInformation object.</param>
        /// <param name="status">Status object.</param>
        public RedirectInformation(
            int requestId,
            RedirectRequest request,
            List<Transaction> payment,
            SubscriptionInformation subscription,
            Status status
            )
        {
            this.requestId = requestId;
            this.request = request;
            this.payment = payment;
            this.subscription = subscription;
            this.status = status;
        }

        /// <summary>
        /// RequestId property.
        /// </summary>
        public int RequestId
        {
            get { return requestId; }
            set { requestId = value; }
        }

        /// <summary>
        /// Request property.
        /// </summary>
        public RedirectRequest Request
        {
            get { return request; }
            set { request = value; }
        }

        /// <summary>
        /// Payment property.
        /// </summary>
        public List<Transaction> Payment
        {
            get { return payment; }
            set { payment = value; }
        }

        /// <summary>
        /// Subscription property.
        /// </summary>
        public SubscriptionInformation Subscription
        {
            get { return subscription; }
            set { subscription = value; }
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
        /// Set request property data.
        /// </summary>
        /// <param name="data">request data.</param>
        /// <returns>object</returns>
        public RedirectInformation SetRequest(object data)
        {
            if (data != null && data.GetType() == typeof(JObject))
            {
                data = new RedirectRequest((JObject)data);
            }

            request = (RedirectRequest)data;

            return this;
        }

        /// <summary>
        /// Set payment property data.
        /// </summary>
        /// <param name="payments">payments data, can be JObject or JArray.</param>
        /// <returns>RedirectInformation current instance.</returns>
        public new RedirectInformation SetPayment(object payments)
        {
            if (payments != null)
            {
                payment = new List<Transaction>();

                if (payments.GetType() == typeof(JObject))
                {
                    JObject item = (JObject)payments;

                    if (item.ContainsKey(TRANSACTION) && item.GetValue(TRANSACTION) != null)
                    {
                        payments = JsonConvert.DeserializeObject(item.GetValue(TRANSACTION).ToString());

                        if (payments.GetType() == typeof(JObject))
                        {
                            payments = new JArray(payments);
                        }
                    }
                }

                foreach (JObject payment in (JArray)payments)
                {
                    this.payment.Add(new Transaction(payment));
                }
            }

            return this;
        }

        /// <summary>
        /// Set subscription property data.
        /// </summary>
        /// <param name="data">subscription data.</param>
        /// <returns>RedirectInformation current instance.</returns>
        public RedirectInformation SetSubscription(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new SubscriptionInformation((JObject)data);
                }

                if (!(data.GetType() == typeof(SubscriptionInformation)))
                {
                    data = null;
                }
            }

            subscription = (SubscriptionInformation)data;

            return this;
        }

        /// <summary>
        /// Parce payment data to JArray.
        /// </summary>
        /// <returns>list of transactions.</returns>
        public JArray PaymentToJArray()
        {
            if (Payment == null || Payment.GetType() != typeof(List<Transaction>))
            {
                return null;
            }

            JArray payments = new JArray();

            foreach (var payment in Payment)
            {
                payments.Add(payment.ToJsonObject());
            }

            return payments;
        }

        /// <summary>
        /// Check if transaction is successful.
        /// </summary>
        /// <returns>true or false, depends on transaction status.</returns>
        public bool IsSuccessful()
        {
            string[] status = { Status.ST_ERROR, Status.ST_FAILED };

            return !status.Contains(Status.StatusText);
        }

        /// <summary>
        /// Check if transaction is approved.
        /// </summary>
        /// <returns>true or false, depends on transaction status.</returns>
        public bool IsApproved()
        {
            return Status.StatusText == Status.ST_APPROVED;
        }

        /// <summary>
        /// Obtains the last transaction made to the session.
        /// </summary>
        /// <returns>Transaction object.</returns>
        public Transaction LastApprovedTransaction()
        {
            return LastTransaction(true);
        }

        /// <summary>
        /// Obtains the last transaction made to the session.
        /// </summary>
        /// <param name="approved">optional check param</param>
        /// <returns>Transaction object.</returns>
        public Transaction LastTransaction(bool approved = false)
        {
            List<Transaction> transactions = Payment;

            if (transactions != null && transactions.Count() > 0)
            {
                if (approved)
                {
                    for (int i = 0; i < transactions.Count(); i++)
                    {
                        Transaction transaction = transactions[i];

                        if (transaction.IsApproved())
                        {
                            return transaction;
                        }
                    }
                }
                else
                {
                    return transactions[0];
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the last authorization associated with the session.
        /// </summary>
        /// <returns>authorization code.</returns>
        public string LastAuthorization()
        {
            Transaction transaction = LastApprovedTransaction();

            if (transaction != null)
            {
                return transaction.Authorization;
            }

            return null;
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>parsed data as JObject.</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject {
                { REQUEST_ID, RequestId },
                { STATUS, Status?.ToJsonObject() },
                { REQUEST, Request?.ToJsonObject() },
                { PAYMENT, PaymentToJArray() },
                { SUBSCRIPTION, Subscription?.ToJsonObject() },
            });
        }
    }
}
