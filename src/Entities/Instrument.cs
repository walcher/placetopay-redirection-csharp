using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Instrument</c>
    /// </summary>
    public class Instrument : Entity
    {
        protected const string BANK = "bank";
        protected const string CARD = "card";
        protected const string CREDIT = "credit";
        protected const string PASSWORD = "password";
        protected const string PIN = "pin";
        protected const string TOKEN = "token";

        protected Bank bank;
        protected Card card;
        protected Token token;
        protected Credit credit;
        protected string pin;
        protected string password;

        /// <summary>
        /// Instrument constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Instrument(JObject data)
        {
            this.Load<Instrument>(data, new JArray { PIN, PASSWORD });

            if (data.ContainsKey(BANK))
            {
                SetBank(data.GetValue(BANK).ToObject<JObject>());
            }

            if (data.ContainsKey(CARD))
            {
                SetCard(data.GetValue(CARD).ToObject<JObject>());
            }

            if (data.ContainsKey(CREDIT))
            {
                SetCredit(data.GetValue(CREDIT).ToObject<JObject>());
            }

            if (data.ContainsKey(TOKEN))
            {
                SetToken(data.GetValue(TOKEN).ToObject<JObject>());
            }
        }

        /// <summary>
        /// Instrument constructor.
        /// </summary>
        /// <param name="data"></param>
        public Instrument(string data)
        {
            JObject json = JObject.Parse(data);

            this.Load<Instrument>(json, new JArray { PIN, PASSWORD });

            if (json.ContainsKey(BANK))
            {
                SetBank(json.GetValue(BANK).ToObject<JObject>());
            }

            if (json.ContainsKey(CARD))
            {
                SetCard(json.GetValue(CARD).ToObject<JObject>());
            }

            if (json.ContainsKey(CREDIT))
            {
                SetCredit(json.GetValue(CREDIT).ToObject<JObject>());
            }

            if (json.ContainsKey(TOKEN))
            {
                SetToken(json.GetValue(TOKEN).ToObject<JObject>());
            }
        }

        /// <summary>
        /// Instrument constructor.
        /// </summary>
        /// <param name="bank">Bank</param>
        /// <param name="card">Card</param>
        /// <param name="token">Token</param>
        /// <param name="credit">Credit</param>
        /// <param name="pin">string</param>
        /// <param name="password">string</param>
        public Instrument(
            Bank bank,
            Card card,
            Token token,
            Credit credit,
            string pin,
            string password
            )
        {
            this.bank = bank;
            this.card = card;
            this.token = token;
            this.credit = credit;
            this.pin = pin;
            this.password = password;
        }

        /// <summary>
        /// Bank property.
        /// </summary>
        public Bank Bank
        {
            get { return bank; }
            set { bank = value; }
        }

        /// <summary>
        /// Card property.
        /// </summary>
        public Card Card
        {
            get { return card; }
            set { card = value; }
        }

        /// <summary>
        /// Token property.
        /// </summary>
        public Token Token
        {
            get { return token; }
            set { token = value; }
        }

        /// <summary>
        /// Credit property.
        /// </summary>
        public Credit Credit
        {
            get { return credit; }
            set { credit = value; }
        }

        /// <summary>
        /// Pin property.
        /// </summary>
        public string Pin
        {
            get { return pin; }
            set { pin = value; }
        }

        /// <summary>
        /// Password property.
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject {
                { BANK, Bank?.ToJsonObject() },
                { CARD, Card?.ToJsonObject() },
                { CREDIT, Credit?.ToJsonObject() },
                { TOKEN, Token?.ToJsonObject() },
                { PIN, Pin },
                { PASSWORD, Password },
            });
        }
    }
}
