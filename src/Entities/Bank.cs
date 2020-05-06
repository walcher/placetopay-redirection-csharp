using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Bank</c>
    /// </summary>
    public class Bank : Entity
    {
        public const int INT_PERSON = 0;
        public const int INT_BUSINESS = 1;

        protected int bankInterface = 0;
        protected string code;
        protected string name;

        /// <summary>
        /// Bank constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Bank(JObject data)
        {
            Load(data, new JArray { "interface", "code", "name" });
        }

        /// <summary>
        /// Bank constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Bank(string data)
        {
            JObject json = JObject.Parse(data);

            Load(json, new JArray { "interface", "code", "name" });
        }

        /// <summary>
        /// Bank constructor.
        /// </summary>
        /// <param name="bankInterface">int</param>
        /// <param name="code">string</param>
        /// <param name="name">string</param>
        public Bank(
            int bankInterface, 
            string code, 
            string name
            )
        {
            this.bankInterface = bankInterface;
            this.code = code;
            this.name = name;
        }

        /// <summary>
        /// Interface property.
        /// </summary>
        public int Interface
        {
            get { return bankInterface; }
            set { bankInterface = value; }
        }

        /// <summary>
        /// Code property.
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; }
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
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            throw new NotImplementedException();
        }
    }
}
