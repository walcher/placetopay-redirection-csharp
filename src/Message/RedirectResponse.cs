using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;

namespace PlacetoPay.Redirection.Message
{
    /// <summary>
    /// Class <c>RedirectResponse</c>
    /// </summary>
    public class RedirectResponse : Entity
    {
        protected const string REQUEST_ID = "requestId";
        protected const string PROCESS_URL = "processUrl";
        protected const string STATUS = "status";

        public int requestId;
        public string processUrl;
        public Status status;

        /// <summary>
        /// RedirectResponse constructor.
        /// </summary>
        public RedirectResponse() { }

        /// <summary>
        /// RedirectResponse constructor.
        /// </summary>
        /// <param name="data">string</param>
        public RedirectResponse(string data) : this(JsonFormatter.ParseJObject(data)) { }

        /// <summary>
        /// RedirectResponse constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public RedirectResponse(JObject data)
        {
            this.Load<RedirectResponse>(data, new JArray { REQUEST_ID, PROCESS_URL });

            if (data.ContainsKey(STATUS))
            {
                SetStatus(data.GetValue(STATUS).ToObject<JObject>());
            }
        }

        /// <summary>
        /// RedirectResponse constructor.
        /// </summary>
        /// <param name="requestId">int</param>
        /// <param name="processUrl">string</param>
        /// <param name="status">Status</param>
        public RedirectResponse(int requestId, string processUrl, Status status)
        {
            this.requestId = requestId;
            this.processUrl = processUrl;
            this.status = status;
        }

        /// <summary>
        /// RequestId property
        /// </summary>
        public int RequestId
        {
            get { return requestId; }
            set { requestId = value; }
        }

        /// <summary>
        /// ProcessUrl property.
        /// </summary>
        public string ProcessUrl
        {
            get { return processUrl; }
            set { processUrl = value; }
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
        /// Check if response is successful.
        /// </summary>
        /// <returns>bool</returns>
        public bool IsSuccessful()
        {
            return Status != null && Status.StatusText == Status.ST_OK;
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject {
                { STATUS, Status?.ToJsonObject() },
                { REQUEST_ID, RequestId },
                { PROCESS_URL, ProcessUrl },
            });
        }
    }
}
