using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using System;
using System.IO;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Credit</c>
    /// </summary>
    public class Credit : Entity
    {
        protected const string CODE = "code";
        protected const string TYPE = "type";
        protected const string GROUP_CODE = "groupCode";
        protected const string INSTALLMENT = "installment";

        protected double code;
        protected string type;
        protected string groupCode;
        protected int installment;

        /// <summary>
        /// Credit constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Credit(JObject data)
        {
            this.Load<Credit>(data, new JArray { CODE, TYPE, GROUP_CODE, INSTALLMENT });
        }

        /// <summary>
        /// Credit constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Credit(string data)
        {
            JsonReader reader = new JsonTextReader(new StringReader(data))
            {
                DateParseHandling = DateParseHandling.None
            };

            JObject json = JObject.Load(reader);

            this.Load<Credit>(json, new JArray { CODE, TYPE, GROUP_CODE, INSTALLMENT });
        }

        /// <summary>
        /// Credit constructor.
        /// </summary>
        /// <param name="code">double</param>
        /// <param name="type">string</param>
        /// <param name="groupCode">string</param>
        /// <param name="installment">int</param>
        public Credit(double code, string type, string groupCode, int installment)
        {
            this.code = code;
            this.type = type;
            this.groupCode = groupCode;
            this.installment = installment;
        }

        /// <summary>
        /// Code property.
        /// </summary>
        public double Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <summary>
        /// Type property.
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// GroupCode property.
        /// </summary>
        public string GroupCode
        {
            get { return groupCode; }
            set { groupCode = value; }
        }

        /// <summary>
        /// Installment property.
        /// </summary>
        public int Installment
        {
            get { return installment; }
            set { installment = value; }
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
