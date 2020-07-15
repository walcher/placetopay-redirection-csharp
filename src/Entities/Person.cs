using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Person</c>
    /// </summary>
    public class Person : Entity
    {
        protected string document;
        protected string documentType;
        protected string name;
        protected string surname;
        protected string company;
        protected string email;
        protected Address address;
        protected string mobile;

        /// <summary>
        /// Person constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Person(JObject data)
        {
            this.Load<Person>(data, new JArray { "document", "documentType", "name", "surname", "company", "email", "mobile" });

            address = data.ContainsKey("address") ? new Address(data.GetValue("address").ToObject<JObject>()) : null;
        }

        /// <summary>
        /// Person constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Person(string data)
        {
            JObject json = JObject.Parse(data);

            this.Load<Person>(json, new JArray { "document", "documentType", "name", "surname", "company", "email", "mobile" });

            address = json.ContainsKey("address") ? new Address(json.GetValue("address").ToObject<JObject>()) : null;
        }

        /// <summary>
        /// Person constructor.
        /// </summary>
        /// <param name="document">string</param>
        /// <param name="documentType">string</param>
        /// <param name="name">string</param>
        /// <param name="surname">string</param>
        /// <param name="company">string</param>
        /// <param name="email">string</param>
        /// <param name="address">Address</param>
        /// <param name="mobile">string</param>
        public Person(string document, string documentType, string name, string surname, string company, string email, Address address, string mobile)
        {
            this.document = document;
            this.documentType = documentType;
            this.name = name;
            this.surname = surname;
            this.company = company;
            this.email = email;
            this.address = address;
            this.mobile = mobile;
        }

        /// <summary>
        /// Document property.
        /// </summary>
        public string Document
        {
            get { return document; }
            set { document = value; }
        }

        /// <summary>
        /// DocumentType property.
        /// </summary>
        public string DocumentType
        {
            get { return documentType; }
            set { documentType = value; }
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
        /// Surname property.
        /// </summary>
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        /// <summary>
        /// Company property.
        /// </summary>
        public string Company
        {
            get { return company; }
            set { company = value; }
        }

        /// <summary>
        /// Email property.
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        /// <summary>
        /// Address property.
        /// </summary>
        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        /// <summary>
        /// Mobile property.
        /// </summary>
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
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
