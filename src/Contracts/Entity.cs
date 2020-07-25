using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Helpers;
using System.Reflection;

namespace PlacetoPay.Redirection.Contracts
{
    /// <summary>
    /// Class <c>Entity</c>
    /// </summary>
    public abstract class Entity
    {
        protected const string ADDRESS_PROPERTY = "Address";
        protected const string AMOUNT_PROPERTY = "Amount";
        protected const string BANK_PROPERTY = "Bank";
        protected const string BUYER_PROPERTY = "Buyer";
        protected const string CARD_PROPERTY = "Card";
        protected const string CREDIT_PROPERTY = "Credit";
        protected const string INSTRUMENT_PROPERTY = "Instrument";
        protected const string PAYER_PROPERTY = "Payer";
        protected const string PAYMENT_PROPERTY = "Payment";
        protected const string RECURRING_PROPERTY = "Recurring";
        protected const string SHIPPING_PROPERTY = "Shipping";
        protected const string STATUS_PROPERTY = "Status";
        protected const string TOKEN_PROPERTY = "Token";

        /// <summary>
        /// ToJsonObject static method.
        /// </summary>
        /// <returns>JObject</returns>
        public abstract JObject ToJsonObject();

        /// <summary>
        /// Set de payer data.
        /// </summary>
        /// <param name="data">JObject</param>
        public void SetPayer(JObject data)
        {
            Person payer = null;

            if (data is JObject)
            {
                payer = new Person(data);
            }

            if (!(payer.GetType() == typeof(Person)))
            {
                payer = null;
            }

            PropertyInfo propertyInfo = GetType().GetProperty(PAYER_PROPERTY);
            propertyInfo.SetValue(this, payer);
        }

        /// <summary>
        /// Set buyer data.
        /// </summary>
        /// <param name="data">JObject</param>
        public void SetBuyer(JObject data)
        {
            Person buyer = null;

            if (data is JObject)
            {
                buyer = new Person(data);
            }

            if (!(buyer.GetType() == typeof(Person)))
            {
                buyer = null;
            }

            PropertyInfo propertyInfo = GetType().GetProperty(BUYER_PROPERTY);
            propertyInfo.SetValue(this, buyer);
        }

        /// <summary>
        /// Set payment data.
        /// </summary>
        /// <param name="data">JObject</param>
        public void SetPayment(JObject data)
        {
            Payment payment = null;

            if (data is JObject)
            {
                payment = new DispersionPayment(data);
            }

            if (!(payment.GetType() == typeof(DispersionPayment)))
            {
                payment = null;
            }

            PropertyInfo propertyInfo = GetType().GetProperty(PAYMENT_PROPERTY);
            propertyInfo.SetValue(this, payment);
        }

        /// <summary>
        /// Set status data.
        /// </summary>
        /// <param name="data">JObject</param>
        public void SetStatus(JObject data)
        {
            Status status = null;

            if (data is JObject)
            {
                status = new Status(data);
            }

            if (!(status.GetType() == typeof(Status)))
            {
                status = null;
            }

            PropertyInfo propertyInfo = GetType().GetProperty(STATUS_PROPERTY);
            propertyInfo.SetValue(this, status);
        }

        /// <summary>
        /// Set amount data.
        /// </summary>
        /// <param name="data">JObject</param>
        public void SetAmount(JObject data)
        {
            Amount amount = null;

            if (data is JObject)
            {
                amount = new Amount(data);
            }

            if (!(amount.GetType() == typeof(Amount)))
            {
                amount = null;
            }

            PropertyInfo propertyInfo = GetType().GetProperty(AMOUNT_PROPERTY);
            propertyInfo.SetValue(this, amount);
        }

        /// <summary>
        /// Set recurring data.
        /// </summary>
        /// <param name="data">JObject</param>
        public void SetRecurring(JObject data)
        {
            Recurring recurring = null;

            if (data is JObject)
            {
                recurring = new Recurring(data);
            }

            if (!(recurring.GetType() == typeof(Recurring)))
            {
                recurring = null;
            }

            PropertyInfo propertyInfo = GetType().GetProperty(RECURRING_PROPERTY);
            propertyInfo.SetValue(this, recurring);
        }

        /// <summary>
        /// Set shipping data.
        /// </summary>
        /// <param name="data">JObject</param>
        public void SetShipping(JObject data)
        {
            Person shipping = null;

            if (data is JObject)
            {
                shipping = new Person(data);
            }

            if (!(shipping.GetType() == typeof(Person)))
            {
                shipping = null;
            }

            PropertyInfo propertyInfo = GetType().GetProperty(SHIPPING_PROPERTY);
            propertyInfo.SetValue(this, shipping);
        }

        /// <summary>
        /// Set instrument data.
        /// </summary>
        /// <param name="data">JObject</param>
        public void SetInstrument(JObject data)
        {
            Instrument instrument = null;

            if (data is JObject)
            {
                instrument = new Instrument(data);
            }

            if (!(instrument.GetType() == typeof(Instrument)))
            {
                instrument = null;
            }

            PropertyInfo propertyInfo = GetType().GetProperty(INSTRUMENT_PROPERTY);
            propertyInfo.SetValue(this, instrument);
        }

        /// <summary>
        /// Set bank data.
        /// </summary>
        /// <param name="data">JObject</param>
        public void SetBank(JObject data)
        {
            Bank bank = null;

            if (data is JObject)
            {
                bank = new Bank(data);
            }

            if (!(bank.GetType() == typeof(Bank)))
            {
                bank = null;
            }

            PropertyInfo propertyInfo = GetType().GetProperty(BANK_PROPERTY);
            propertyInfo.SetValue(this, bank);
        }

        /// <summary>
        /// Set credit data.
        /// </summary>
        /// <param name="data">JObject</param>
        public void SetCredit(JObject data)
        {
            Credit credit = null;

            if (data is JObject)
            {
                credit = new Credit(data);
            }

            if (!(credit.GetType() == typeof(Credit)))
            {
                credit = null;
            }

            PropertyInfo propertyInfo = GetType().GetProperty(CREDIT_PROPERTY);
            propertyInfo.SetValue(this, credit);
        }

        /// <summary>
        /// Set token data.
        /// </summary>
        /// <param name="data">JObject</param>
        public void SetToken(JObject data)
        {
            Token token = null;

            if (data is JObject)
            {
                token = new Token(data);
            }

            if (!(token.GetType() == typeof(Token)))
            {
                token = null;
            }

            PropertyInfo propertyInfo = GetType().GetProperty(TOKEN_PROPERTY);
            propertyInfo.SetValue(this, token);
        }

        /// <summary>
        /// Set card data.
        /// </summary>
        /// <param name="data">JObject</param>
        public void SetCard(JObject data)
        {
            Card card = null;

            if (data is JObject)
            {
                card = new Card(data);
            }

            if (!(card.GetType() == typeof(Card)))
            {
                card = null;
            }

            PropertyInfo propertyInfo = GetType().GetProperty(CARD_PROPERTY);
            propertyInfo.SetValue(this, card);
        }

        /// <summary>
        /// Set address data.
        /// </summary>
        /// <param name="data">JObject</param>
        public void SetAddress(JObject data)
        {
            Address address = null;

            if (data is JObject)
            {
                address = new Address(data);
            }

            if (!(address.GetType() == typeof(Address)))
            {
                address = null;
            }

            PropertyInfo propertyInfo = GetType().GetProperty(ADDRESS_PROPERTY);
            propertyInfo.SetValue(this, address);
        }

        /// <summary>
        /// Filter json data.
        /// </summary>
        /// <param name="data">JObject</param>
        /// <returns>JObject</returns>
        public static JObject JObjectFilter(JObject data)
        {
            string json = JsonFormatter.RemoveNullOrEmpty(JToken.Parse(data.ToString())).ToString();

            return JObject.Parse(json);
        }
    }
}
