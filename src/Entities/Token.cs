using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Token</c>
    /// </summary>
    public class Token : Entity
    {
        protected const string STATUS = "status";
        protected const string TOKEN = "tokenText";
        protected const string SUBTOKEN = "subtoken";
        protected const string FRANCHISE = "franchise";
        protected const string FRANCHISE_NAME = "franchiseName";
        protected const string ISSUER_NAME = "issuerName";
        protected const string LAST_DIGITS = "lastDigits";
        protected const string VALID_UNTIL = "validUntil";
        protected const string CVV = "cvv";
        protected const string INSTALLMENTS = "installments";
        protected const string DATE_FORMAT = "M/y";

        protected Status status;
        protected string token;
        protected string subtoken;
        protected string franchise;
        protected string franchiseName;
        protected string issuerName;
        protected string lastDigits;
        protected string validUntil;
        protected string cvv;
        protected int installments;

        /// <summary>
        /// Token constructor.
        /// </summary>
        public Token() { }

        /// <summary>
        /// Token constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Token(string data) : this(JObject.Parse(data)) { }

        /// <summary>
        /// Token constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Token(JObject data)
        {
            this.Load<Token>(data, new JArray { TOKEN, SUBTOKEN, FRANCHISE, FRANCHISE_NAME, ISSUER_NAME, LAST_DIGITS, VALID_UNTIL, CVV, INSTALLMENTS });

            if (data.ContainsKey(STATUS))
            {
                SetStatus(data.GetValue(STATUS).ToObject<JObject>());
            }
        }

        /// <summary>
        /// Token constructor.
        /// </summary>
        /// <param name="status">Status</param>
        /// <param name="token">string</param>
        /// <param name="subtoken">string</param>
        /// <param name="franchise">string</param>
        /// <param name="franchiseName">string</param>
        /// <param name="issuerName">string</param>
        /// <param name="lastDigits">string</param>
        /// <param name="validUntil">string</param>
        /// <param name="cvv">string</param>
        /// <param name="installments">int</param>
        public Token(
            Status status,
            string token,
            string subtoken,
            string franchise,
            string franchiseName,
            string issuerName,
            string lastDigits,
            string validUntil,
            string cvv,
            int installments
            )
        {
            this.status = status;
            this.token = token;
            this.subtoken = subtoken;
            this.franchise = franchise;
            this.franchiseName = franchiseName;
            this.issuerName = issuerName;
            this.lastDigits = lastDigits;
            this.validUntil = validUntil;
            this.cvv = cvv;
            this.installments = installments;
        }

        /// <summary>
        /// Status property.
        /// </summary>
        public Status Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// TokenText property.
        /// </summary>
        public string TokenText
        {
            get { return token; }
            set { token = value; }
        }

        /// <summary>
        /// Subtoken property.
        /// </summary>
        public string Subtoken
        {
            get { return subtoken; }
            set { subtoken = value; }
        }

        /// <summary>
        /// Franchise property.
        /// </summary>
        public string Franchise
        {
            get { return franchise; }
            set { franchise = value; }
        }

        /// <summary>
        /// FranchiseName property.
        /// </summary>
        public string FranchiseName
        {
            get { return franchiseName; }
            set { franchiseName = value; }
        }

        /// <summary>
        /// IssuerName property.
        /// </summary>
        public string IssuerName
        {
            get { return issuerName; }
            set { issuerName = value; }
        }

        /// <summary>
        /// LastDigits property.
        /// </summary>
        public string LastDigits
        {
            get { return lastDigits; }
            set { lastDigits = value; }
        }

        /// <summary>
        /// ValidUntil property.
        /// </summary>
        public string ValidUntil
        {
            get { return validUntil; }
            set { validUntil = value; }
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
        /// Installments property.
        /// </summary>
        public int Installments
        {
            get { return installments; }
            set { installments = value; }
        }

        /// <summary>
        /// Get expiration date.
        /// </summary>
        /// <returns>string</returns>
        public string GetExpiration()
        {
            return DateTime.Parse(ValidUntil).ToString(DATE_FORMAT);
        }

        /// <summary>
        /// Check if is successful.
        /// </summary>
        /// <returns>bool</returns>
        public bool IsSuccessful()
        {
            return Status.StatusText == Status.ST_OK;
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject {
                { STATUS, Status?.ToJsonObject() },
                { StringFormatter.NormalizeProperty(TOKEN), TokenText },
                { SUBTOKEN, Subtoken },
                { FRANCHISE, Franchise },
                { FRANCHISE_NAME, FranchiseName },
                { ISSUER_NAME, IssuerName },
                { LAST_DIGITS, LastDigits },
                { VALID_UNTIL, ValidUntil },
                { CVV, Cvv },
                { INSTALLMENTS, Installments },
            });
        }
    }
}
