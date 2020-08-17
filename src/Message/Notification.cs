using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;
using System.Text;

namespace PlacetoPay.Redirection.Message
{
    /// <summary>
    /// Class <c>Notification</c>
    /// </summary>
    public class Notification : Entity
    {
        protected const string REQUEST_ID = "requestId";
        protected const string REFERENCE = "reference";
        protected const string SIGNATURE = "signature";
        protected const string STATUS = "status";

        protected int requestId;
        protected string reference;
        protected string signature;
        protected Status status;
        private readonly string tranKey;

        /// <summary>
        /// Notification constructor.
        /// </summary>
        public Notification() { }

        /// <summary>
        /// Notification constructor.
        /// </summary>
        /// <param name="data">string</param>
        /// <param name="tranKey">string</param>
        public Notification(string data, string tranKey = "") : this(JObject.Parse(data), tranKey) { }

        /// <summary>
        /// Notification constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        /// <param name="tranKey">string</param>
        public Notification(JObject data, string tranKey = "")
        {
            this.Load<Notification>(data, new JArray { REQUEST_ID, REFERENCE, SIGNATURE });

            if (data.ContainsKey(STATUS))
            {
                SetStatus(data.GetValue(STATUS).ToObject<JObject>());
            }

            this.tranKey = tranKey;
        }

        /// <summary>
        /// Notification constructor.
        /// </summary>
        /// <param name="status">Status</param>
        /// <param name="requestId">int</param>
        /// <param name="reference">string</param>
        /// <param name="signature">string</param>
        /// <param name="tranKey">string</param>
        public Notification(
            Status status,
            int requestId,
            string reference,
            string signature,
            string tranKey
            )
        {
            this.status = status;
            this.requestId = requestId;
            this.reference = reference;
            this.signature = signature;
            this.tranKey = tranKey;
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
        /// Reference property.
        /// </summary>
        public string Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        /// <summary>
        /// Signature property.
        /// </summary>
        public string Signature
        {
            get { return signature; }
            set { signature = value; }
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
        /// Get status.
        /// </summary>
        public Status GetStatus()
        {
            return this.GetStatus<Notification>();
        }

        /// <summary>
        /// Make signature.
        /// </summary>
        /// <returns>string</returns>
        public string MakeSignature()
        {
            return CryptoHelper.MakeSHA1(new StringBuilder().Append(RequestId)
                .Append(this.GetStatus<Notification>().StatusText)
                .Append(this.GetStatus<Notification>().Date)
                .Append(tranKey)
                .ToString());
        }

        /// <summary>
        /// Check if signature is valid.
        /// </summary>
        /// <returns>bool</returns>
        public bool IsValidNotification()
        {
            return Signature == MakeSignature();
        }

        /// <summary>
        /// Check if notification is approved.
        /// </summary>
        /// <returns></returns>
        public bool IsApproved()
        {
            return this.GetStatus<Notification>().StatusText == Status.ST_APPROVED;
        }

        /// <summary>
        /// Check if notification is rejected.
        /// </summary>
        /// <returns></returns>
        public bool IsRejected()
        {
            return this.GetStatus<Notification>().StatusText == Status.ST_REJECTED;
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject {
                { STATUS, GetStatus()?.ToJsonObject() },
                { REQUEST_ID, RequestId },
                { REFERENCE, Reference },
                { SIGNATURE, Signature },
            });
        }
    }
}
