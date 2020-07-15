using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Address</c>
    /// </summary>
    public class Address : Entity
    {
        protected string street;
        protected string city;
        protected string state;
        protected string postalCode;
        protected string country;
        protected string phone;

        /// <summary>
        /// Address constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Address(JObject data)
        {
            this.Load<Address>(data, new JArray { "street", "city", "state", "postalCode", "phone", "country" });
        }

        /// <summary>
        /// Address constructor.
        /// </summary>
        /// <param name="data"></param>
        public Address(string data)
        {
            JObject json = JObject.Parse(data);

            this.Load<Address>(json, new JArray { "street", "city", "state", "postalCode", "phone", "country" });
        }

        /// <summary>
        /// Address constructor.
        /// </summary>
        /// <param name="street">string</param>
        /// <param name="city">string</param>
        /// <param name="state">string</param>
        /// <param name="postalCode">string</param>
        /// <param name="country">string</param>
        /// <param name="phone">string</param>
        public Address(
            string street,
            string city,
            string state,
            string postalCode,
            string country,
            string phone
            )
        {
            this.street = street;
            this.city = city;
            this.state = state;
            this.postalCode = postalCode;
            this.country = country;
            this.phone = phone;
        }

        /// <summary>
        /// Street property.
        /// </summary>
        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        /// <summary>
        /// City property.
        /// </summary>
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        /// <summary>
        /// State property.
        /// </summary>
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// PostalCode property.
        /// </summary>
        public string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; }
        }

        /// <summary>
        /// Country property.
        /// </summary>
        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        /// <summary>
        /// Phone property.
        /// </summary>
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
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
