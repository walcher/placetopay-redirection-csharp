using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Messages;
using System;

namespace PlacetoPay.Redirection.Carriers
{
    public class SoapCarrier : Carrier
    {
        public SoapCarrier(Authentication auth, JObject config) : base(auth, config) { }

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
