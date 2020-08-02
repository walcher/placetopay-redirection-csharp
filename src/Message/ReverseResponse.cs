using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Entities;

namespace PlacetoPay.Redirection.Message
{
    /// <summary>
    /// Class <c>ReverseResponse</c>
    /// </summary>
    public class ReverseResponse : Entity
    {
        protected const string STATUS = "status";
        protected const string PAYMENT = "payment";

        public Status status;
        public DispersionPayment payment;

        /// <summary>
        /// ReverseResponse constructor.
        /// </summary>
        public ReverseResponse() { }

        /// <summary>
        /// ReverseResponse constructor.
        /// </summary>
        /// <param name="data">string</param>
        public ReverseResponse(string data) : this(JObject.Parse(data)) { }

        /// <summary>
        /// ReverseResponse constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public ReverseResponse(JObject data)
        {
            if (data.ContainsKey(STATUS))
            {
                SetStatus(data.GetValue(STATUS).ToObject<JObject>());
            }

            if (data.ContainsKey(PAYMENT))
            {
                SetPayment(data.GetValue(PAYMENT).ToObject<JObject>());
            }
        }

        /// <summary>
        /// ReverseResponse constructor.
        /// </summary>
        /// <param name="payment">DispersionPayment</param>
        /// <param name="status">Status</param>
        public ReverseResponse(DispersionPayment payment, Status status)
        {
            this.payment = payment;
            this.status = status;
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
        /// Payment property.
        /// </summary>
        public DispersionPayment Payment
        {
            get { return payment; }
            set { payment = value; }
        }

        /// <summary>
        /// Check if response is successful.
        /// </summary>
        /// <returns>bool</returns>
        public bool IsSuccessful()
        {
            return Status != null && Status.StatusText != Status.ST_ERROR;
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject {
                { STATUS, Status?.ToJsonObject() },
                { PAYMENT, Payment?.ToJsonObject() },
            });
        }
    }
}
