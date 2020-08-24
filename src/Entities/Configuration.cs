using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Helpers;
using System;
using System.Collections.Generic;

namespace PlacetoPay.Redirection.Entities
{
    public class Configuration
    {
        protected const string LOGIN = "login";
        protected const string TRANKEY = "tranKey";
        protected const string URL = "url";
        protected const string TYPE = "type";
        protected const string ADDITIONAL = "auth_additional";
        protected const string AUTH = "auth";
        protected const int SOAP_1_1 = 1;
        protected const int SOAP_1_2 = 2;

        public string login;
        protected string tranKey;
        protected Uri url;
        protected string requestType;
        protected Dictionary<string, string> additional;
        protected AuthenticationSecurity auth;
        protected string wsdl;
        protected string location;
        protected int soapVersion = SOAP_1_1;

        public Configuration() { }

        public Configuration(string data) : this(JsonFormatter.ParseJObject(data)) { }

        public Configuration(JObject data)
        {
            login = data.GetValue(LOGIN).ToString();
            tranKey = data.GetValue(TRANKEY).ToString();

            if (data.GetValue(URL).ToString().EndsWith("/"))
            {
                url = new Uri(data.GetValue(URL).ToString());
            }
            else
            {
                url = new Uri($"{data.GetValue(URL)}/");
            }

            requestType = data.GetValue(TYPE).ToString();

            if (data.ContainsKey(ADDITIONAL))
            {
                var additionData = data.GetValue(ADDITIONAL).ToObject<JObject>();

                foreach (var item in additionData)
                {
                    additional.Add(item.Key, (string)item.Value);
                }
            }

            if (data.ContainsKey(AUTH))
            {
                auth = new AuthenticationSecurity(data.GetValue(AUTH).ToString());
            }
        }

        public Configuration(
            string login,
            string tranKey,
            Uri url,
            string requestType,
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
                this.url = new Uri($"{url}/");
            }

            this.requestType = requestType;
            this.additional = additional;
            this.auth = auth;
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        public string TranKey
        {
            get { return tranKey; }
            set { tranKey = value; }
        }
        public Uri Url
        {
            get { return url; }
            set { url = value; }
        }
        public string RequestType
        {
            get { return requestType; }
            set { requestType = value; }
        }
        public Dictionary<string, string> Additional
        {
            get { return additional; }
            set { additional = value; }
        }
        public AuthenticationSecurity Auth
        {
            get { return auth; }
            set { auth = value; }
        }
        public string Wsdl
        {
            get { return wsdl; }
            set { wsdl = value; }
        }
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        public int SoapVersion
        {
            get { return soapVersion; }
            set { soapVersion = value; }
        }
    }
}
