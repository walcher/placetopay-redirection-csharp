using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Discount</c>
    /// </summary>
    public class Discount : Entity
    {
        protected const string CODE = "code";
        protected const string TYPE = "type";
        protected const string AMOUNT = "amount";
        protected const string BASE = "base";
        protected const string PERCENT = "percent";

        protected string code;
        protected string type;
        protected double amount;
        protected double baseAmount;
        protected double percent;

        /// <summary>
        /// Discount constructor.
        /// </summary>
        public Discount() { }

        /// <summary>
        /// Discount constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Discount(string data) : this(JObject.Parse(data)) { }

        /// <summary>
        /// Discount constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Discount(JObject data)
        {
            this.Load<Discount>(data, new JArray { CODE, TYPE, AMOUNT, BASE, PERCENT });
        }

        /// <summary>
        /// Discount constructor.
        /// </summary>
        /// <param name="code">string</param>
        /// <param name="type">string</param>
        /// <param name="amount">double</param>
        /// <param name="baseAmount">double</param>
        /// <param name="percent">double</param>
        public Discount(
            string code,
            string type,
            double amount,
            double baseAmount,
            double percent
            )
        {
            this.code = code;
            this.type = type;
            this.amount = amount;
            this.baseAmount = baseAmount;
            this.percent = percent;
        }

        /// <summary>
        /// Code property.
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <summary>
        /// Type property.
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Amount property.
        /// </summary>
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        /// <summary>
        /// Base property.
        /// </summary>
        public double Base
        {
            get { return baseAmount; }
            set { baseAmount = value; }
        }

        /// <summary>
        /// Percent property.
        /// </summary>
        public double Percent
        {
            get { return percent; }
            set { percent = value; }
        }

        // <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(
                NumberFormatter.NormalizeNumber(new JObject {
                    { CODE, Code },
                    { TYPE, Type },
                    { AMOUNT, Amount },
                    { BASE, Base },
                    { PERCENT, Percent },
                })
            );
        }
    }
}
