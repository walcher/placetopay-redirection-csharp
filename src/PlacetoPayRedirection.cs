using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Helpers;
using PlacetoPay.Redirection.Message;
using System;

namespace PlacetoPay.Redirection
{
    /// <summary>
    /// Class <c>PlacetoPay</c>
    /// </summary>
    public class PlacetoPayRedirection : Gateway
    {
        public PlacetoPayRedirection() { }

        public PlacetoPayRedirection(string data) : this(JsonFormatter.ParseJObject(data)) { }

        public PlacetoPayRedirection(JObject data) : base(data) { }

        public override RedirectInformation Collect(CollectRequest collectRequest)
        {
            throw new NotImplementedException();
        }

        public override RedirectInformation Query(string requestId)
        {
            throw new NotImplementedException();
        }

        public override RedirectResponse Request(RedirectRequest redirectRequest)
        {
            throw new NotImplementedException();
        }

        public override ReverseResponse Reverse(string transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
