using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Amount</c>
    /// </summary>
    public class Amount : AmountBase
    {
        protected List<TaxDetail> taxes;
        protected List<AmountDetail> details;
        protected double taxAmount;

        /// <summary>
        /// Amount constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Amount(JObject data) : base(data)
        {
            taxes = data.ContainsKey("taxes") ? SetTaxes(data.GetValue("taxes").ToObject<JArray>()) : null;
            details = data.ContainsKey("details") ? SetDetails(data.GetValue("details").ToObject<JArray>()) : null;
        }

        /// <summary>
        /// Amount constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Amount(string data) : base(data)
        {
            JObject json = JObject.Parse(data);

            taxes = json.ContainsKey("taxes") ? SetTaxes(json.GetValue("taxes").ToObject<JArray>()) : null;
            details = json.ContainsKey("details") ? SetDetails(json.GetValue("details").ToObject<JArray>()) : null;
        }

        /// <summary>
        /// Amount constructor.
        /// </summary>
        /// <param name="taxes">List</param>
        /// <param name="details"></param>
        /// <param name="total"></param>
        /// <param name="currency"></param>
        public Amount(List<TaxDetail> taxes, List<AmountDetail> details, double total, string currency) : base(currency, total)
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
        /// <param name="taxes">JArray</param>
        /// <returns></returns>
        private List<TaxDetail> SetTaxes(JArray taxes)
        {
            List<TaxDetail> list = new List<TaxDetail>();

            foreach (var tax in taxes)
            {
                JObject taxDetail = tax.ToObject<JObject>();

                list.Add(new TaxDetail(taxDetail));
                taxAmount += (double)taxDetail.GetValue("amount");
            }

            return list;
        }

        /// <summary>
        /// Set list of amount details.
        /// </summary>
        /// <param name="details">JArray</param>
        /// <returns></returns>
        private List<AmountDetail> SetDetails(JArray details)
        {
            List<AmountDetail> list = new List<AmountDetail>();

            foreach (var detail in details)
            {
                JObject amountDetail = detail.ToObject<JObject>();

                list.Add(new AmountDetail(amountDetail));
            }

            return list;
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
