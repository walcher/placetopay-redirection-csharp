using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Account</c>
    /// </summary>
    public class Account : Entity
    {
        protected const string STATUS = "status";
        protected const string BANK_CODE = "bankCode";
        protected const string BANK_NAME = "bankName";
        protected const string ACCOUNT_TYPE = "accountType";
        protected const string ACCOUNT_NUMBER = "accountNumber";
        protected const string ACCOUNT = "account";
        protected const string FRANCHISE = "franchise";

        public Status status;
        public string bankCode;
        public string bankName;
        public string accountType;
        public string accountNumber;

        /// <summary>
        /// Account constructor.
        /// </summary>
        public Account() { }

        /// <summary>
        /// Account constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Account(string data) : this(JsonFormatter.ParseJObject(data)) { }

        /// <summary>
        /// Account constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Account(JObject data)
        {
            this.Load<Account>(data, new JArray { BANK_CODE, BANK_NAME, ACCOUNT_TYPE, ACCOUNT_NUMBER });

            if (data.ContainsKey(STATUS))
            {
                SetStatus(data.GetValue(STATUS).ToObject<JObject>());
            }
        }

        /// <summary>
        /// Account constructor.
        /// </summary>
        /// <param name="status">Status</param>
        /// <param name="bankCode">string</param>
        /// <param name="bankName">string</param>
        /// <param name="accountType">string</param>
        /// <param name="accountNumber">string</param>
        public Account(
            Status status,
            string bankCode,
            string bankName,
            string accountType,
            string accountNumber
            )
        {
            this.status = status;
            this.bankCode = bankCode;
            this.bankName = bankName;
            this.accountType = accountType;
            this.accountNumber = accountNumber;
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
        /// BankCode property.
        /// </summary>
        public string BankCode
        {
            get { return bankCode; }
            set { bankCode = value; }
        }

        /// <summary>
        /// BankName property.
        /// </summary>
        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }

        /// <summary>
        /// AccountType property.
        /// </summary>
        public string AccountType
        {
            get { return accountType; }
            set { accountType = value; }
        }

        /// <summary>
        /// AccountNumber property.
        /// </summary>
        public string AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        /// <summary>
        /// Get the subscription franchise code (CR_VS, RM_MC).
        /// </summary>
        /// <returns>string</returns>
        public string GetFranchise()
        {
            return "_" + BankCode + "_";
        }

        /// <summary>
        /// Get class name.
        /// </summary>
        /// <returns></returns>
        public string GetClassType()
        {
            return ACCOUNT;
        }

        /// <summary>
        /// Get the subscription franchise name (VISA, Mastercard, Bancolombia).
        /// </summary>
        /// <returns>string</returns>
        public string GetFranchiseName()
        {
            return BankName;
        }

        /// <summary>
        /// Last digits for the instrument subscribed in order to display to the user.
        /// </summary>
        /// <returns>string</returns>
        public string GetLastDigits()
        {
            return AccountNumber.Substring(AccountNumber.Length - 4);
        }

        /// <summary>
        /// Parses this entity as Name Value Pairs for the response.
        /// </summary>
        /// <returns>JObject</returns>
        public JObject AsNameValuePairJObject()
        {
            return JObjectFilter(new JObject {
                { BANK_CODE, BankCode },
                { BANK_NAME, BankName },
                { ACCOUNT_TYPE, AccountType },
                { ACCOUNT_NUMBER, AccountNumber },
            });
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject {
                { STATUS, Status?.ToJsonObject() },
                { BANK_CODE, BankCode },
                { BANK_NAME, BankName },
                { ACCOUNT_TYPE, AccountType },
                { ACCOUNT_NUMBER, AccountNumber },
                { FRANCHISE, GetFranchise() }
            });
        }
    }
}
