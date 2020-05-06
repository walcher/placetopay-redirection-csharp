using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Recurring</c>
    /// </summary>
    public class Recurring : Entity
    {
        protected string periodicity;
        protected int interval;
        protected string nextPayment;
        protected int maxPeriods;
        protected string dueDate;
        protected string notificationUrl;

        /// <summary>
        /// Recurring constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Recurring(JObject data)
        {
            Load(data, new JArray { "periodicity", "interval", "maxPeriods", "notificationUrl" });

            if (data.ContainsKey("nextPayment"))
            {
                nextPayment = DateTime.Parse((string)data.GetValue("nextPayment")).ToString("yyyy-MM-dd");
            }

            if (data.ContainsKey("dueDate"))
            {
                dueDate = DateTime.Parse((string)data.GetValue("dueDate")).ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// Recurring constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Recurring(string data)
        {
            JObject json = JObject.Parse(data);

            Load(json, new JArray { "periodicity", "interval", "maxPeriods", "notificationUrl" });

            if (json.ContainsKey("nextPayment"))
            {
                nextPayment = DateTime.Parse((string)json.GetValue("nextPayment")).ToString("Y-m-d");
            }

            if (json.ContainsKey("dueDate"))
            {
                dueDate = DateTime.Parse((string)json.GetValue("dueDate")).ToString("Y-m-d");
            }
        }

        /// <summary>
        /// Recurring constructor.
        /// </summary>
        /// <param name="periodicity">string</param>
        /// <param name="interval">int</param>
        /// <param name="nextPayment">string</param>
        /// <param name="maxPeriods">int</param>
        /// <param name="dueDate">string</param>
        /// <param name="notificationUrl">string</param>
        public Recurring(
            string periodicity,
            int interval,
            string nextPayment,
            int maxPeriods,
            string dueDate,
            string notificationUrl
            )
        {
            this.periodicity = periodicity;
            this.interval = interval;
            this.nextPayment = nextPayment;
            this.maxPeriods = maxPeriods;
            this.dueDate = dueDate;
            this.notificationUrl = notificationUrl;
        }

        /// <summary>
        /// Periodicity property.
        /// </summary>
        public string Periodicity
        {
            get { return periodicity; }
            set { periodicity = value; }
        }

        /// <summary>
        /// Interval property.
        /// </summary>
        public int Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        /// <summary>
        /// NextPayment property.
        /// </summary>
        public string NextPayment
        {
            get { return nextPayment; }
            set { nextPayment = value; }
        }

        /// <summary>
        /// MaxPeriods property.
        /// </summary>
        public int MaxPeriods
        {
            get { return maxPeriods; }
            set { maxPeriods = value; }
        }

        /// <summary>
        /// DueDate property.
        /// </summary>
        public string DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }

        /// <summary>
        /// NotificationUrl property.
        /// </summary>
        public string NotificationUrl
        {
            get { return notificationUrl; }
            set { notificationUrl = value; }
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
