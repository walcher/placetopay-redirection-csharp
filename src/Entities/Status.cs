using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using System;

namespace PlacetoPay.Redirection.Entities
{
    public class Status : Entity
    {
        public const string ST_OK = "OK";
        public const string ST_FAILED = "FAILED";
        public const string ST_APPROVED = "APPROVED";
        public const string ST_APPROVED_PARTIAL = "APPROVED_PARTIAL";
        public const string ST_REJECTED = "REJECTED";
        public const string ST_PENDING = "PENDING";
        public const string ST_PENDING_VALIDATION = "PENDING_VALIDATION";
        public const string ST_REFUNDED = "REFUNDED";
        public const string ST_ERROR = "ERROR";
        public const string ST_UNKNOWN = "UNKNOWN";

        protected string status;
        protected string reason;
        protected string message;
        protected string date;

        protected static string[] STATUSES = {
            ST_OK,
            ST_FAILED,
            ST_APPROVED,
            ST_APPROVED_PARTIAL,
            ST_REJECTED,
            ST_PENDING,
            ST_PENDING_VALIDATION,
            ST_REFUNDED,
            ST_ERROR,
            ST_UNKNOWN
        };

        /// <summary>
        /// Status constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Status(JObject data)
        {
            Load(data, new JArray { "status", "reason", "message", "date" });
        }

        /// <summary>
        /// Status constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Status(string data)
        {
            JObject json = JObject.Parse(data);

            Load(json, new JArray { "status", "reason", "message", "date" });
        }

        /// <summary>
        /// Status constructor.
        /// </summary>
        /// <param name="status">string</param>
        /// <param name="reason">string</param>
        /// <param name="message">string</param>
        /// <param name="date">string</param>
        public Status(
            string status,
            string reason,
            string message,
            string date
            )
        {
            this.status = status;
            this.reason = reason;
            this.message = message;
            this.date = date;
        }

        /// <summary>
        /// Status constructor.
        /// </summary>
        /// <param name="status">string</param>
        /// <param name="reason">string</param>
        /// <param name="message">string</param>
        public Status(
            string status,
            string reason,
            string message
            )
        {
            this.status = status;
            this.reason = reason;
            this.message = message;
        }

        public string GetStatus()
        {
            return status;
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            throw new NotImplementedException();
        }
    }
}
