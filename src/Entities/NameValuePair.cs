using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>NameValuePair</c>
    /// </summary>
    public class NameValuePair : Entity
    {
        protected string keyword;
        protected string valueField;
        protected string displayOn = "none";

        /// <summary>
        /// NameValuePair constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public NameValuePair(JObject data)
        {
            this.Load<NameValuePair>(data, new JArray { "keyword", "value", "displayOn" });
        }

        /// <summary>
        /// NameValuePair constructor.
        /// </summary>
        /// <param name="data">string</param>
        public NameValuePair(string data)
        {
            JObject json = JObject.Parse(data);

            this.Load<NameValuePair>(json, new JArray { "keyword", "value", "displayOn" });
        }

        /// <summary>
        /// NameValuePair constructor.
        /// </summary>
        /// <param name="keyword">string</param>
        /// <param name="valueField">string</param>
        /// <param name="displayOn">string</param>
        public NameValuePair(string keyword, string valueField, string displayOn)
        {
            this.keyword = keyword;
            this.valueField = valueField;
            this.displayOn = displayOn;
        }

        /// <summary>
        /// Keyword property.
        /// </summary>
        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }

        /// <summary>
        /// Value property.
        /// </summary>
        public string Value
        {
            get { return valueField; }
            set { valueField = value; }
        }

        /// <summary>
        /// DisplayOn property.
        /// </summary>
        public string DisplayOn
        {
            get { return displayOn; }
            set { displayOn = value; }
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
