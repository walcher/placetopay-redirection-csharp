using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>AmountDetail</c>
    /// </summary>
    public class AmountDetail : Entity
    {
        protected const string AMOUNT = "amount";
        protected const string KIND = "kind";

        protected string kind;
        protected double amount;

        /// <summary>
        /// AmountDetail constructor.
        /// </summary>
        public AmountDetail() { }

        /// <summary>
        /// AmountDetail constructor.
        /// </summary>
        /// <param name="data">string</param>
        public AmountDetail(string data) : this(JObject.Parse(data)) { }

        /// <summary>
        /// AmountDetail constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public AmountDetail(JObject data)
        {
            this.Load<AmountDetail>(data, new JArray { KIND, AMOUNT });
        }

        /// <summary>
        /// AmountDetail constructor.
        /// </summary>
        /// <param name="kind">string</param>
        /// <param name="amount">double</param>
        public AmountDetail(string kind, double amount)
        {
            this.kind = kind;
            this.amount = amount;
        }

        /// <summary>
        /// Kind property.
        /// </summary>
        public string Kind
        {
            get { return kind; }
            set { kind = value; }
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
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(
                NumberFormatter.NormalizeNumber(new JObject {
                    { KIND, Kind },
                    { AMOUNT, Amount },
                })
            );
        }
    }
}
