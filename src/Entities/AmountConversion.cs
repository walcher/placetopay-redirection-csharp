using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Helpers;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>AmountConversion</c>
    /// </summary>
    public class AmountConversion : Entity
    {
        protected const string FROM = "from";
        protected const string TO = "to";
        protected const string FACTOR = "factor";

        protected AmountBase from;
        protected AmountBase to;
        protected double factor;

        /// <summary>
        /// AmountConversion constructor.
        /// </summary>
        public AmountConversion() { }

        /// <summary>
        /// AmountConversion constructor.
        /// </summary>
        /// <param name="data">string</param>
        public AmountConversion(string data) : this(JObject.Parse(data)) { }

        /// <summary>
        /// AmountConversion constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public AmountConversion(JObject data)
        {
            if (data.ContainsKey(FROM))
            {
                SetFrom(data.GetValue(FROM).ToObject<JObject>());
            }

            if (data.ContainsKey(TO))
            {
                SetTo(data.GetValue(TO).ToObject<JObject>());
            }

            if (data.ContainsKey(FACTOR))
            {
                SetFactor(data.GetValue(FACTOR).ToObject<JValue>().Value);
            }
        }

        /// <summary>
        /// AmountConversion constructor.
        /// </summary>
        /// <param name="from">AmountBase</param>
        /// <param name="to">AmountBase</param>
        /// <param name="factor">float</param>
        public AmountConversion(AmountBase from, AmountBase to, double factor)
        {
            this.from = from;
            this.to = to;
            this.factor = factor;
        }

        /// <summary>
        /// From property.
        /// </summary>
        public AmountBase From
        {
            get { return from; }
            set { from = value; }
        }

        /// <summary>
        /// To property.
        /// </summary>
        public AmountBase To
        {
            get { return to; }
            set { to = value; }
        }

        /// <summary>
        /// Factor property.
        /// </summary>
        public double Factor
        {
            get { return factor; }
            set { factor = value; }
        }

        /// <summary>
        /// Helper function to quickly set all the values.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>AmountConversion</returns>
        public AmountConversion SetAmountBase(object data)
        {
            if (data.GetType() == typeof(JObject))
            {
                data = new AmountBase((JObject)data);
            }

            SetFrom(data);
            SetTo(data);
            SetFactor(1.0d);

            return this;
        }

        /// <summary>
        /// Set from property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>AmountConversion</returns>
        public AmountConversion SetFrom(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new AmountBase((JObject)data);
                }

                if (!(data.GetType() == typeof(AmountBase)))
                {
                    data = null;
                }
            }

            from = (AmountBase)data;

            return this;
        }

        /// <summary>
        /// Set to property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>AmountConversion</returns>
        public AmountConversion SetTo(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new AmountBase((JObject)data);
                }

                if (!(data.GetType() == typeof(AmountBase)))
                {
                    data = null;
                }
            }

            to = (AmountBase)data;

            return this;
        }

        /// <summary>
        /// Set factor property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>AmountConversion</returns>
        public AmountConversion SetFactor(object data)
        {
            factor = (double)data;

            return this;
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(
                NumberFormatter.NormalizeNumber(new JObject {
                    { FROM, From?.ToJsonObject() },
                    { TO, To?.ToJsonObject() },
                    { FACTOR, Factor },
                })
            );
        }
    }
}
