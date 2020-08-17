using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Helpers;
using PlacetoPay.Redirection.Validators;
using System.Collections.Generic;
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
        protected const string VALIDATOR = "validator";

        private object validatorInstance;

        /// <summary>
        /// ToJsonObject static method.
        /// </summary>
        /// <returns>JObject</returns>
        public abstract JObject ToJsonObject();

        /// <summary>
        /// Set payer property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>object</returns>
        public object SetPayer(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new Person((JObject)data);
                }

                if (!(data.GetType() == typeof(Person)))
                {
                    data = null;
                }
            }

            PropertyInfo propertyInfo = GetType().GetProperty(PAYER_PROPERTY);
            propertyInfo.SetValue(this, data);

            return this;
        }

        /// <summary>
        /// Set buyer property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>object</returns>
        public object SetBuyer(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new Person((JObject)data);
                }

                if (!(data.GetType() == typeof(Person)))
                {
                    data = null;
                }
            }

            PropertyInfo propertyInfo = GetType().GetProperty(BUYER_PROPERTY);
            propertyInfo.SetValue(this, data);

            return this;
        }

        /// <summary>
        /// Set payment property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>object</returns>
        public object SetPayment(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new DispersionPayment((JObject)data);
                }

                if (!(data.GetType() == typeof(DispersionPayment)))
                {
                    data = null;
                }
            }

            PropertyInfo propertyInfo = GetType().GetProperty(PAYMENT_PROPERTY);
            propertyInfo.SetValue(this, data);

            return this;
        }

        /// <summary>
        /// Set status property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>object</returns>
        public object SetStatus(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new Status((JObject)data);
                }

                if (!(data.GetType() == typeof(Status)))
                {
                    data = null;
                }
            }

            PropertyInfo propertyInfo = GetType().GetProperty(STATUS_PROPERTY);
            propertyInfo.SetValue(this, data);

            return this;
        }

        /// <summary>
        /// Set amount property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>object</returns>
        public object SetAmount(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new Amount((JObject)data);
                }

                if (!(data.GetType() == typeof(Amount)))
                {
                    data = null;
                }
            }

            PropertyInfo propertyInfo = GetType().GetProperty(AMOUNT_PROPERTY);
            propertyInfo.SetValue(this, data);

            return this;
        }

        /// <summary>
        /// Set recurring property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>object</returns>
        public object SetRecurring(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new Recurring((JObject)data);
                }

                if (!(data.GetType() == typeof(Recurring)))
                {
                    data = null;
                }
            }

            PropertyInfo propertyInfo = GetType().GetProperty(RECURRING_PROPERTY);
            propertyInfo.SetValue(this, data);

            return this;
        }

        /// <summary>
        /// Set shipping property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>object</returns>
        public object SetShipping(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new Person((JObject)data);
                }

                if (!(data.GetType() == typeof(Person)))
                {
                    data = null;
                }
            }

            PropertyInfo propertyInfo = GetType().GetProperty(SHIPPING_PROPERTY);
            propertyInfo.SetValue(this, data);

            return this;
        }

        /// <summary>
        /// Set instrument property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>object</returns>
        public object SetInstrument(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new Instrument((JObject)data);
                }

                if (!(data.GetType() == typeof(Instrument)))
                {
                    data = null;
                }
            }

            PropertyInfo propertyInfo = GetType().GetProperty(INSTRUMENT_PROPERTY);
            propertyInfo.SetValue(this, data);

            return this;
        }

        /// <summary>
        /// Set bank property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>object</returns>
        public object SetBank(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new Bank((JObject)data);
                }

                if (!(data.GetType() == typeof(Bank)))
                {
                    data = null;
                }
            }

            PropertyInfo propertyInfo = GetType().GetProperty(BANK_PROPERTY);
            propertyInfo.SetValue(this, data);

            return this;
        }

        /// <summary>
        /// Set credit property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>object</returns>
        public object SetCredit(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new Credit((JObject)data);
                }

                if (!(data.GetType() == typeof(Credit)))
                {
                    data = null;
                }
            }

            PropertyInfo propertyInfo = GetType().GetProperty(CREDIT_PROPERTY);
            propertyInfo.SetValue(this, data);

            return this;
        }

        /// <summary>
        /// Set token property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>object</returns>
        public object SetToken(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new Token((JObject)data);
                }

                if (!(data.GetType() == typeof(Token)))
                {
                    data = null;
                }
            }

            PropertyInfo propertyInfo = GetType().GetProperty(TOKEN_PROPERTY);
            propertyInfo.SetValue(this, data);

            return this;
        }

        /// <summary>
        /// Set card property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>object</returns>
        public object SetCard(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new Card((JObject)data);
                }

                if (!(data.GetType() == typeof(Card)))
                {
                    data = null;
                }
            }

            PropertyInfo propertyInfo = GetType().GetProperty(CARD_PROPERTY);
            propertyInfo.SetValue(this, data);

            return this;
        }

        /// <summary>
        /// Set address property data.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>object</returns>
        public object SetAddress(object data)
        {
            if (data != null)
            {
                if (data.GetType() == typeof(JObject))
                {
                    data = new Address((JObject)data);
                }

                if (!(data.GetType() == typeof(Address)))
                {
                    data = null;
                }
            }

            PropertyInfo propertyInfo = GetType().GetProperty(ADDRESS_PROPERTY);
            propertyInfo.SetValue(this, data);

            return this;
        }

        /// <summary>
        /// Get validator for current class.
        /// </summary>
        /// <returns>dynamic</returns>
        public dynamic GetValidator()
        {
            if (validatorInstance == null)
            {
                FieldInfo fieldInfo = GetType().GetField(
                    VALIDATOR,
                    BindingFlags.NonPublic | BindingFlags.Instance
                );

                validatorInstance = fieldInfo.GetValue(this);
            }

            return validatorInstance;
        }

        /// <summary>
        /// Validates if this entity contains the required information.
        /// </summary>
        /// <param name="fields">list</param>
        /// <param name="silent">bool</param>
        /// <returns>bool</returns>
        public bool IsValid(out List<string> fields, bool silent)
        {
            return GetValidator().IsValid(this, out fields, silent);
        }

        /// <summary>
        /// Verifies if the object has all the values required, returns those who are lacking.
        /// </summary>
        /// <param name="requiredFields">list</param>
        /// <returns>list</returns>
        public List<string> CheckMissingFields(List<string> requiredFields)
        {
            List<string> missing = new List<string>();

            foreach (var field in requiredFields)
            {
                FieldInfo fieldInfo = GetType().GetField(
                    field,
                    BindingFlags.NonPublic | BindingFlags.Instance
                );

                var value = fieldInfo.GetValue(this);

                if (value == null)
                {
                    missing.Add(field);
                }
            }

            return missing;
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
