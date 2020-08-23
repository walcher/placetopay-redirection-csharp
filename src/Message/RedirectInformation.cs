using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace PlacetoPay.Redirection.Message
{
    /// <summary>
    /// Class <c>RedirectInformation</c>
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
        /// RedirectInformation constructor.
        /// </summary>
        public RedirectInformation() { }

        /// <summary>
        /// RedirectInformation constructor.
        /// </summary>
        /// <param name="data">string</param>
        public RedirectInformation(string data) : this(JsonFormatter.ParseJObject(data)) { }

        /// <summary>
        /// RedirectInformation constructor.
        /// </summary>
        /// <param name="data">JObject</param>
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
                SetPayment(data.GetValue(PAYMENT).ToObject<JArray>());
            }

            if (data.ContainsKey(SUBSCRIPTION))
            {
                SetSubscription(data.GetValue(SUBSCRIPTION).ToObject<JObject>());
            }
        }

        /// <summary>
        /// RedirectInformation constructor.
        /// </summary>
        /// <param name="requestId">int</param>
        /// <param name="request">RedirectRequest</param>
        /// <param name="payment">List</param>
        /// <param name="subscription">SubscriptionInformation</param>
        /// <param name="status">Status</param>
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
        /// <param name="data">object</param>
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
        /// Set payment property data
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>RedirectInformation</returns>
        public new RedirectInformation SetPayment(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    JObject item = (JObject)data;

                    if (item.ContainsKey(TRANSACTION) && item.GetValue(TRANSACTION) != null)
                    {
                        data = item.GetValue(TRANSACTION).ToObject<JArray>();
                    }
                }

                if (data.GetType() == typeof(JArray))
                {
                    List<Transaction> list = new List<Transaction>();

                    foreach (var payment in (JArray)data)
                    {
                        JObject item = payment.ToObject<JObject>();

                        list.Add(new Transaction(item));
                    }

                    data = list;
                }

                payment = (List<Transaction>)data;
            }

            return this;
        }

        /// <summary>
        /// Set subscription property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>RedirectInformation</returns>
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
        /// <returns>JArray</returns>
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
        /// <returns>bool</returns>
        public bool IsSuccessful()
        {
            string[] status = { Status.ST_ERROR, Status.ST_FAILED };

            return !status.Contains(Status.StatusText);
        }

        /// <summary>
        /// Check if transaction is approved.
        /// </summary>
        /// <returns>bool</returns>
        public bool IsApproved()
        {
            return Status.StatusText == Status.ST_APPROVED;
        }

        /// <summary>
        /// Obtains the last transaction made to the session.
        /// </summary>
        /// <returns>Transaction</returns>
        public Transaction LastApprovedTransaction()
        {
            return LastTransaction(true);
        }

        /// <summary>
        /// Obtains the last transaction made to the session.
        /// </summary>
        /// <param name="approved">bool</param>
        /// <returns>Transaction</returns>
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
        /// <returns>string</returns>
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
        /// <returns>JsonObject</returns>
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
