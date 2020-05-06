using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Card</c>
    /// </summary>
    public class Card : Entity
    {
        public const string TP_CREDIT = "C";
        public const string TP_DEBIT_SAVINGS = "A";
        public const string TP_DEBIT_CURRENT = "R";

        protected string name;
        protected string number;
        protected string cvv;
        protected string expirationMonth;
        protected string expirationYear;
        protected int installments;
        protected string kind = TP_CREDIT;

        /// <summary>
        /// Card constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Card(JObject data)
        {
            Load(data, new JArray { "name", "number", "expirationMonth", "expirationYear", "installments", "kind", "cvv" });
        }

        /// <summary>
        /// Card constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Card(string data)
        {
            JObject json = JObject.Parse(data);

            Load(json, new JArray { "name", "number", "expirationMonth", "expirationYear", "installments", "kind", "cvv" });
        }

        /// <summary>
        /// Card constructor.
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="number">string</param>
        /// <param name="cvv">string</param>
        /// <param name="expirationMonth">string</param>
        /// <param name="expirationYear">string</param>
        /// <param name="installments">int</param>
        /// <param name="kind">string</param>
        public Card(
            string name,
            string number,
            string cvv,
            string expirationMonth,
            string expirationYear,
            int installments,
            string kind
            )
        {
            this.name = name;
            this.number = number;
            this.cvv = cvv;
            this.expirationMonth = expirationMonth;
            this.expirationYear = expirationYear;
            this.installments = installments;
            this.kind = kind;
        }

        /// <summary>
        /// Card constructor.
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="number">string</param>
        /// <param name="cvv">string</param>
        /// <param name="expirationMonth">string</param>
        /// <param name="expirationYear">string</param>
        /// <param name="installments">int</param>
        public Card(
            string name,
            string number,
            string cvv,
            string expirationMonth,
            string expirationYear,
            int installments
            )
        {
            this.name = name;
            this.number = number;
            this.cvv = cvv;
            this.expirationMonth = expirationMonth;
            this.expirationYear = expirationYear;
            this.installments = installments;
        }

        /// <summary>
        /// Name property.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Number property.
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        /// <summary>
        /// Cvv property.
        /// </summary>
        public string Cvv
        {
            get { return cvv; }
            set { cvv = value; }
        }

        /// <summary>
        /// ExpirationMonth property.
        /// </summary>
        public string ExpirationYear
        {
            get
            {
                if (expirationYear.Length == 2)
                {
                    expirationYear = "20" + expirationYear;
                }

                return expirationYear;
            }
            set { expirationYear = value; }
        }

        /// <summary>
        /// ExpirationYearShort property.
        /// </summary>
        public string ExpirationYearShort
        {
            get
            {
                if (expirationYear.Length == 4)
                {
                    expirationYear = expirationYear.Substring(2, 2);
                }

                return expirationYear;
            }
            set { expirationYear = value; }
        }

        /// <summary>
        /// ExpirationMonth property.
        /// </summary>
        public string ExpirationMonth
        {
            get
            {
                return string.Format("%1$" + 2 + "s", expirationMonth).Replace(" ", "0");
            }
            set { expirationMonth = value; }
        }

        /// <summary>
        /// Installments property.
        /// </summary>
        public int Installments
        {
            get { return installments; }
            set { installments = value; }
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
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            throw new NotImplementedException();
        }
    }
}
