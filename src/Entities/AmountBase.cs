using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;
using PlacetoPay.Redirection.Validators;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>AmountBase</c>
    /// </summary>
    public class AmountBase : Entity
    {
        protected const string CURRENCY = "currency";
        protected const string TOTAL = "total";

        protected AmountBaseValidator validator = new AmountBaseValidator();
        protected string currency = "COP";
        protected double total;

        /// <summary>
        /// AmountBase construnctor.
        /// </summary>
        public AmountBase() { }

        /// <summary>
        /// AmountBase construnctor.
        /// </summary>
        /// <param name="data">string</param>
        public AmountBase(string data) : this(JObject.Parse(data)) { }

        /// <summary>
        /// AmountBase construnctor.
        /// </summary>
        /// <param name="data">JObject</param>
        public AmountBase(JObject data)
        {
            this.Load<AmountBase>(data, new JArray { CURRENCY, TOTAL });
        }

        /// <summary>
        /// AmountBase construnctor.
        /// </summary>
        /// <param name="currency">string</param>
        /// <param name="total">double</param>
        public AmountBase(string currency, double total)
        {
            this.currency = currency;
            this.total = total;
        }

        /// <summary>
        /// Currency property.
        /// </summary>
        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        /// <summary>
        /// Total property.
        /// </summary>
        public double Total
        {
            get { return total; }
            set { total = value; }
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(
                NumberFormatter.NormalizeNumber(new JObject {
                    { CURRENCY, Currency },
                    { TOTAL, Total },
                })
            );
        }
    }
}
