using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Validators;
using System.Collections.Generic;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Amount</c>
    /// </summary>
    public class Amount : AmountBase
    {
        protected const string AMOUNT = "amount";
        protected const string DETAILS = "details";
        protected const string TAXES = "taxes";

        protected new AmountValidator validator = new AmountValidator();
        protected List<TaxDetail> taxes;
        protected List<AmountDetail> details;
        protected double taxAmount;

        /// <summary>
        /// Amount constructor.
        /// </summary>
        public Amount() { }

        /// <summary>
        /// Amount constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Amount(string data) : this(JObject.Parse(data)) { }

        /// <summary>
        /// Amount constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Amount(JObject data) : base(data)
        {
            if (data.ContainsKey(TAXES))
            {
                SetTaxes(data.GetValue(TAXES).ToObject<JArray>());
            }

            if (data.ContainsKey(DETAILS))
            {
                SetDetails(data.GetValue(DETAILS).ToObject<JArray>());
            }
        }

        /// <summary>
        /// Amount constructor.
        /// </summary>
        /// <param name="taxes">List</param>
        /// <param name="details">List</param>
        /// <param name="total">double</param>
        /// <param name="currency">double</param>
        public Amount(
            List<TaxDetail> taxes,
            List<AmountDetail> details,
            double total,
            string currency
            ) : base(currency, total)
        {
            this.taxes = taxes;
            this.details = details;
        }

        /// <summary>
        /// Taxes property.
        /// </summary>
        public List<TaxDetail> Taxes
        {
            get { return taxes; }
            set { taxes = value; }
        }

        /// <summary>
        /// Details property.
        /// </summary>
        public List<AmountDetail> Details
        {
            get { return details; }
            set { details = value; }
        }

        /// <summary>
        /// TaxtAmount property.
        /// </summary>
        public double TaxAmount
        {
            get { return taxAmount; }
            set { taxAmount = value; }
        }

        /// <summary>
        /// Set list of taxes.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>Amount</returns>
        private Amount SetTaxes(object data)
        {
            if (data != null && data.GetType() == typeof(JArray))
            {
                List<TaxDetail> list = new List<TaxDetail>();

                foreach (var tax in (JArray)data)
                {
                    JObject taxDetail = tax.ToObject<JObject>();

                    list.Add(new TaxDetail(taxDetail));
                    taxAmount += (double)taxDetail.GetValue(AMOUNT);
                }

                data = list;
            }

            taxes = (List<TaxDetail>)data;

            return this;
        }

        /// <summary>
        /// Set list of amount details.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>Amount</returns>
        private Amount SetDetails(object data)
        {
            if (data != null)
            {
                List<AmountDetail> list = new List<AmountDetail>();

                foreach (var detail in (JArray)data)
                {
                    JObject amountDetail = detail.ToObject<JObject>();

                    list.Add(new AmountDetail(amountDetail));
                }

                data = list;
            }

            details = (List<AmountDetail>)data;

            return this;
        }

        /// <summary>
        /// Convert taxes list to json array.
        /// </summary>
        /// <returns>JArray</returns>
        private JArray TaxesToJArray()
        {
            JArray taxes = new JArray();

            if (Taxes != null)
            {
                foreach (var taxe in Taxes)
                {
                    taxes.Add(taxe.ToJsonObject());
                }
            }

            return taxes;
        }

        /// <summary>
        /// Convert details list to json array.
        /// </summary>
        /// <returns>JArray</returns>
        private JArray DetailsToJArray()
        {
            JArray details = new JArray();

            if (Details != null)
            {
                foreach (var detail in Details)
                {
                    details.Add(detail.ToJsonObject());
                }
            }

            return details;
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            JObject jsonBase = base.ToJsonObject();

            jsonBase.Merge(new JObject
            {
                { TAXES, TaxesToJArray() },
                { DETAILS, DetailsToJArray() },
            }, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });

            return JObjectFilter(jsonBase);
        }
    }
}
