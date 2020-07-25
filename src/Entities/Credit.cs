using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;

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

        protected int code;
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
            JObject json = JObject.Parse(data);

            this.Load<Credit>(json, new JArray { CODE, TYPE, GROUP_CODE, INSTALLMENT });
        }

        /// <summary>
        /// Credit constructor.
        /// </summary>
        /// <param name="code">double</param>
        /// <param name="type">string</param>
        /// <param name="groupCode">string</param>
        /// <param name="installment">int</param>
        public Credit(int code, string type, string groupCode, int installment)
        {
            this.code = code;
            this.type = type;
            this.groupCode = groupCode;
            this.installment = installment;
        }

        /// <summary>
        /// Code property.
        /// </summary>
        public int Code
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
            return JObjectFilter(new JObject {
                { CODE, Code },
                { TYPE, Type },
                { GROUP_CODE, GroupCode },
                { INSTALLMENT, Installment },
            });
        }
    }
}
