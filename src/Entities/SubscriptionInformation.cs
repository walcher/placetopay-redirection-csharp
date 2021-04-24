using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;
using System.Collections.Generic;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c></c>
    /// </summary>
    public class SubscriptionInformation : Entity
    {
        protected const string TYPE = "type";
        protected const string STATUS = "status";
        protected const string INSTRUMENT = "instrument";
        protected const string ITEM = "item";
        protected const string TOKEN = "token";
        protected const string ACCOUNT = "account";

        public string type;
        public Status status;
        public List<NameValuePair> instrument;

        /// <summary>
        /// SubscriptionInformation constructor.
        /// </summary>
        public SubscriptionInformation() { }

        /// <summary>
        /// SubscriptionInformation constructor.
        /// </summary>
        /// <param name="data">string</param>
        public SubscriptionInformation(string data) : this(JsonFormatter.ParseJObject(data)) { }

        /// <summary>
        /// SubscriptionInformation constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public SubscriptionInformation(JObject data)
        {
            this.Load<SubscriptionInformation>(data, new JArray { TYPE });

            if (data.ContainsKey(STATUS))
            {
                SetStatus(data.GetValue(STATUS).ToObject<JObject>());
            }

            if (data.ContainsKey(INSTRUMENT))
            {
                SetInstrument(data.GetValue(INSTRUMENT).ToObject<JArray>());
            }
        }

        /// <summary>
        /// SubscriptionInformation constructor.
        /// </summary>
        /// <param name="type">string</param>
        /// <param name="status">Status</param>
        /// <param name="instrument">List</param>
        public SubscriptionInformation(string type, Status status, List<NameValuePair> instrument)
        {
            this.type = type;
            this.status = status;
            this.instrument = instrument;
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
        /// Status property.
        /// </summary>
        public Status Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Instrument property.
        /// </summary>
        public List<NameValuePair> Instrument
        {
            get { return instrument; }
            set { instrument = value; }
        }

        /// <summary>
        /// Set instrument property data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public new SubscriptionInformation SetInstrument(object data)
        {
            if (data.GetType() == typeof(JObject))
            {
                JObject item = (JObject)data;

                if (item.ContainsKey(ITEM))
                {
                    data = item.GetValue(ITEM).ToObject<JArray>();
                }
            }

            if (data.GetType() == typeof(JArray))
            {
                List<NameValuePair> list = new List<NameValuePair>();

                foreach (var nvp in (JArray)data)
                {
                    JObject item = nvp.ToObject<JObject>();

                    list.Add(new NameValuePair(item));
                }

                data = list;
            }

            instrument = (List<NameValuePair>)data;

            return this;
        }

        /// <summary>
        /// Convert instrument list to JArray
        /// </summary>
        /// <returns></returns>
        public JArray InstrumentToArray()
        {
            JArray instruments = new JArray();

            if (Instrument != null)
            {
                foreach (var pair in Instrument)
                {
                    instruments.Add(pair.ToJsonObject());
                }
            }

            return instruments;
        }

        /// <summary>
        /// Parses the instrument as the proper entity, Keep in mind that can be null if no instrument its provided.
        /// </summary>
        /// <returns>Account|Token|null</returns>
        public object ParseInstrument()
        {
            List<NameValuePair> instrumentNVP = instrument;

            if (instrument == null)
            {
                return null;
            }

            JObject data = new JObject { { STATUS, Status?.ToJsonObject() } };

            foreach (var nvp in instrumentNVP)
            {
                data.Add(nvp.Keyword, nvp.Value);
            }

            if (Type.Equals(TOKEN))
            {
                return new Token(data);
            }
            else if (Type.Equals(ACCOUNT))
            {
                return new Account(data);
            }

            return null;
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject {
                { TYPE, Type },
                { STATUS, Status?.ToJsonObject() },
                { INSTRUMENT, InstrumentToArray() }
            });
        }
    }
}
