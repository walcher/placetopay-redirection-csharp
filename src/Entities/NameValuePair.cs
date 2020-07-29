using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>NameValuePair</c>
    /// </summary>
    public class NameValuePair : Entity
    {
        protected const string DISPLAY_ON = "displayOn";
        protected const string KEYWORD = "keyword";
        protected const string VALUE = "value";

        protected string keyword;
        protected string valueField;
        protected string displayOn = "none";

        /// <summary>
        /// NameValuePair constructor.
        /// </summary>
        public NameValuePair() { }

        /// <summary>
        /// NameValuePair constructor.
        /// </summary>
        /// <param name="data">string</param>
        public NameValuePair(string data) : this(JObject.Parse(data)) { }

        /// <summary>
        /// NameValuePair constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public NameValuePair(JObject data)
        {
            this.Load<NameValuePair>(data, new JArray { KEYWORD, VALUE, DISPLAY_ON });
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
            return JObjectFilter(new JObject {
                { KEYWORD, Keyword },
                { VALUE, Value },
                { DISPLAY_ON, DisplayOn },
            });
        }
    }
}
