using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;
using PlacetoPay.Redirection.Validators;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Recurring</c>
    /// </summary>
    public class Recurring : Entity
    {
        protected const string DATE_FORMAT = "yyyy-MM-dd";
        protected const string DUE_DATE = "dueDate";
        protected const string INTERVAL = "interval";
        protected const string MAX_PERIODS = "maxPeriods";
        protected const string NEXT_PAYMENT = "nextPayment";
        protected const string NOTIFICATION_URL = "notificationUrl";
        protected const string PERIODICITY = "periodicity";

        protected RecurringValidator validator = new RecurringValidator();
        protected string periodicity;
        protected int interval;
        protected string nextPayment;
        protected int maxPeriods;
        protected string dueDate;
        protected string notificationUrl;

        /// <summary>
        /// Recurring constructor.
        /// </summary>
        public Recurring() { }

        /// <summary>
        /// Recurring constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Recurring(string data) : this(JsonFormatter.ParseJObject(data)) { }

        /// <summary>
        /// Recurring constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Recurring(JObject data)
        {
            this.Load<Recurring>(data, new JArray { PERIODICITY, INTERVAL, MAX_PERIODS, NOTIFICATION_URL });

            if (data.ContainsKey(NEXT_PAYMENT))
            {
                nextPayment = BaseValidator.ParseDate((string)data.GetValue(NEXT_PAYMENT), DATE_FORMAT);
            }

            if (data.ContainsKey(DUE_DATE))
            {
                dueDate = BaseValidator.ParseDate((string)data.GetValue(DUE_DATE), DATE_FORMAT);
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
            return JObjectFilter(new JObject {
                { PERIODICITY, Periodicity},
                { INTERVAL, Interval },
                { NEXT_PAYMENT, NextPayment },
                { MAX_PERIODS, MaxPeriods },
                { DUE_DATE, DueDate },
                { NOTIFICATION_URL, NotificationUrl },
            });
        }
    }
}
