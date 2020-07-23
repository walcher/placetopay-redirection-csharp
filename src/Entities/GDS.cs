using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>GDS</c>
    /// </summary>
    public class GDS : Entity
    {
        protected const string AIRLINE = "airline";
        protected const string CODE = "code";
        protected const string PNR = "pnr";
        protected const string SESSION = "session";

        protected string code;
        protected string session;
        protected string pnr;
        protected string airline;

        /// <summary>
        /// GDS constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public GDS(JObject data)
        {
            this.Load<GDS>(data, new JArray { CODE, SESSION, PNR, AIRLINE });
        }

        /// <summary>
        /// GDS constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public GDS(string data)
        {
            JObject json = JObject.Parse(data);

            this.Load<GDS>(json, new JArray { CODE, SESSION, PNR, AIRLINE });
        }

        /// <summary>
        /// GDS constructor.
        /// </summary>
        /// <param name="code">string</param>
        /// <param name="session">string</param>
        /// <param name="pnr">string</param>
        /// <param name="airline">string</param>
        public GDS(
            string code,
            string session,
            string pnr,
            string airline
            )
        {
            this.code = code;
            this.session = session;
            this.pnr = pnr;
            this.airline = airline;
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
        /// Session property.
        /// </summary>
        public string Session
        {
            get { return session; }
            set { session = value; }
        }

        /// <summary>
        /// Pnr property.
        /// </summary>
        public string Pnr
        {
            get { return pnr; }
            set { pnr = value; }
        }

        /// <summary>
        /// Airline property.
        /// </summary>
        public string Airline
        {
            get { return airline; }
            set { airline = value; }
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject {
                { CODE, Code },
                { SESSION, Session },
                { PNR, Pnr },
                { AIRLINE, Airline },
            });
        }
    }
}
