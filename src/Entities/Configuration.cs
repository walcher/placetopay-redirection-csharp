using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Exceptions;
using PlacetoPay.Redirection.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlacetoPay.Redirection.Entities
{
    public class Configuration
    {
        protected const string LOGIN = "login";
        protected const string TRANKEY = "tranKey";
        protected const string URL = "url";
        protected const string TYPE = "type";
        protected const string ADDITIONAL = "auth_additional";
        protected const int SOAP_1_1 = 1;
        protected const int SOAP_1_2 = 2;

        public string login;
        protected string tranKey;
        protected Uri url;
        protected string type;
        protected Dictionary<string, string> additional;
        protected AuthenticationSecurity auth;
        protected string wsdl;
        protected string location;
        protected int soapVersion = SOAP_1_1;

        public Configuration() { }

        public Configuration(string data) : this(JsonFormatter.ParseJObject(data)) { }

        public Configuration(JObject data)
        {

        }

        public Configuration(
            string login, 
            string tranKey, 
            Uri url, 
            string type, 
            Dictionary<string, string> additional = null, 
            AuthenticationSecurity auth = null
            )
        {
            this.login = login;
            this.tranKey = tranKey;

            if (url.ToString().EndsWith("/"))
            {
                this.url = url;
            }
            else
            {
                try
                {
                    this.url = new Uri($"{url}/");
                }
                catch (UriFormatException ex)
                {
                    throw new PlacetoPayException(ex.Message);
                }
            }

            this.type = type;
            this.additional = additional;
            this.auth = auth;
        }
    }
}
