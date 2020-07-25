using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;
using System;
using System.Linq;

namespace PlacetoPay.Redirection.Entities
{
    public class Status : Entity
    {
        protected const string DATE = "date";
        protected const string MESSAGE = "message";
        protected const string REASON = "reason";
        protected const string STATUS = "statusText";
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
            this.Load<Status>(data, new JArray { STATUS, REASON, MESSAGE, DATE });
        }

        /// <summary>
        /// Status constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Status(string data)
        {
            JObject json = JObject.Parse(data);

            this.Load<Status>(json, new JArray { STATUS, REASON, MESSAGE, DATE });
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

        /// <summary>
        /// StatusText property.
        /// </summary>
        public string StatusText
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Reason property.
        /// </summary>
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }

        /// <summary>
        /// Message property.
        /// </summary>
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        /// <summary>
        /// Date property.
        /// </summary>
        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>
        /// Check if is failed.
        /// </summary>
        /// <returns>bool</returns>
        public bool IsFailed()
        {
            return StatusText == ST_FAILED;
        }

        /// <summary>
        /// Check if is successful.
        /// </summary>
        /// <returns>bool</returns>
        public bool IsSuccessful()
        {
            return StatusText == ST_OK;
        }

        /// <summary>
        /// Check if is approved.
        /// </summary>
        /// <returns>bool</returns>
        public bool IsApproved()
        {
            return StatusText == ST_APPROVED;
        }

        /// <summary>
        /// Check if is rejected.
        /// </summary>
        /// <returns></returns>
        public bool IsRejected()
        {
            return StatusText == ST_REJECTED;
        }

        /// <summary>
        /// Check if is an error.
        /// </summary>
        /// <returns>bool</returns>
        public bool IsError()
        {
            return StatusText == ST_ERROR;
        }

        /// <summary>
        /// Check if is an valid status.
        /// </summary>
        /// <param name="status">string</param>
        /// <returns>bool</returns>
        public static bool ValidStatus(string status = null)
        {
            if (status != null)
            {
                return STATUSES.Contains(status);
            }

            return false;
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject {
                { StringFormatter.NormalizeProperty(STATUS), StatusText },
                { REASON, Reason },
                { MESSAGE, Message },
                { DATE, Date },
            });
        }
    }
}
