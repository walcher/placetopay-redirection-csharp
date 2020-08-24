using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Exceptions;
using PlacetoPay.Redirection.Messages;
using PlacetoPay.Redirection.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlacetoPay.Redirection.Contracts
{
    /// <summary>
    /// Class <c>Gateway</c>
    /// </summary>
    public abstract class Gateway
    {
        protected const string LOGIN = "login";
        protected const string TRANKEY = "tranKey";
        protected const string URL = "url";
        protected const string TYPE = "type";
        protected const string ADDITIONAL = "auth_additional";
        public const string TP_SOAP = "soap";
        public const string TP_REST = "rest";

        protected string requestType = TP_REST;
        protected Carrier carrier = null;
        protected JObject config;

        /// <summary>
        /// Gateway constructor.
        /// </summary>
        public Gateway() { }

        /// <summary>
        /// Gateway constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Gateway(string data) : this(JObject.Parse(data)) { }

        /// <summary>
        /// Gateway constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Gateway(JObject data)
        {
            if (!data.ContainsKey(LOGIN) || !data.ContainsKey(TRANKEY))
            {
                throw new PlacetoPayException("No login or tranKey provided on gateway");
            }

            if (!data.ContainsKey(URL) || !BaseValidator.IsValidUrl(data.GetValue(URL).ToString()))
            {
                throw new PlacetoPayException("No service URL provided to use");
            }

            if (!data.GetValue(URL).ToString().EndsWith("/"))
            {
                data.GetValue(URL).Replace(string.Concat(data.GetValue(URL).ToString(), "/"));
            }

            if (data.ContainsKey(TYPE) && IsValidType(data.GetValue(TYPE).ToString()))
            {
                requestType = data.GetValue(TYPE).ToString();
            }

            config = data;
        }

        /// <summary>
        /// Gateway constructor.
        /// </summary>
        /// <param name="login">string</param>
        /// <param name="trankey">string</param>
        /// <param name="url">Uri</param>
        public Gateway(string login, string trankey, Uri url) : this(login, trankey, url, TP_REST) { }

        /// <summary>
        /// Gateway constructor.
        /// </summary>
        /// <param name="login">string</param>
        /// <param name="trankey">string</param>
        /// <param name="url">Uri</param>
        /// <param name="requestType">string</param>
        public Gateway(
            string login,
            string trankey,
            Uri url,
            string requestType = TP_REST
            ) : this(login, trankey, url, new Dictionary<string, string>(), requestType) { }

        /// <summary>
        /// Gateway constructor.
        /// </summary>
        /// <param name="login">string</param>
        /// <param name="trankey">string</param>
        /// <param name="url">Uri</param>
        /// <param name="additional">Dictionary</param>
        /// <param name="requestType">string</param>
        public Gateway(
            string login,
            string trankey,
            Uri url,
            Dictionary<string, string> additional,
            string requestType = TP_REST
            ) : this(login, trankey, url, null, additional, requestType) { }

        /// <summary>
        /// Gateway constructor.
        /// </summary>
        /// <param name="login">string</param>
        /// <param name="trankey">string</param>
        /// <param name="url">Uri</param>
        /// <param name="auth">AuthenticationSecurity</param>
        /// <param name="additional">Dictionary</param>
        /// <param name="requestType">string</param>
        public Gateway(
            string login,
            string trankey,
            Uri url,
            AuthenticationSecurity auth,
            Dictionary<string, string> additional,
            string requestType = TP_REST
            )
        {
            if (login == null || trankey == null)
            {
                throw new PlacetoPayException("No login or tranKey provided on gateway");
            }

            if (url == null)
            {
                throw new PlacetoPayException("No service URL provided to use");
            }

            if (IsValidType(requestType))
            {
                this.requestType = requestType;
            }

            config = new JObject
            {
                { LOGIN, login },
                { TRANKEY, trankey },
                { URL, url },
                { TYPE, requestType },
            };
        }

        /// <summary>
        /// Redirect request process.
        /// </summary>
        /// <param name="redirectRequest">object</param>
        /// <returns>RedirectResponse</returns>
        public abstract RedirectResponse Request(object redirectRequest);

        /// <summary>
        /// Query request process.
        /// </summary>
        /// <param name="requestId">string</param>
        /// <returns>RedirectInformation</returns>
        public abstract RedirectInformation Query(string requestId);

        /// <summary>
        /// Collect request process.
        /// </summary>
        /// <param name="collectRequest">object</param>
        /// <returns>RedirectInformation</returns>
        public abstract RedirectInformation Collect(object collectRequest);

        /// <summary>
        /// Reverse request process.
        /// </summary>
        /// <param name="transactionId">string</param>
        /// <returns>ReverseResponse</returns>
        public abstract ReverseResponse Reverse(string transactionId);

        /// <summary>
        /// Change the web service to use for the connection.
        /// </summary>
        /// <param name="requestType">string</param>
        public void Using(string requestType)
        {
            if (IsValidType(requestType))
            {
                this.requestType = requestType;
                carrier = null;
            }
            else
            {
                throw new PlacetoPayException("The only connection methods are SOAP or REST");
            }
        }

        /// <summary>
        /// Read notification from response.
        /// </summary>
        /// <param name="content">object</param>
        /// <returns>Notification</returns>
        public Notification ReadNotification(object content)
        {
            if (content == null)
            {
                throw new PlacetoPayException("The notification content is empty");
            }

            if (content is JObject data)
            {
                return new Notification(data, config.GetValue(TRANKEY).ToString()); ;
            }

            return new Notification((string)content, config.GetValue(TRANKEY).ToString()); ;
        }

        /// <summary>
        /// Add additional data to authentication request header.
        /// </summary>
        /// <param name="data">JObject</param>
        /// <returns>Gateway</returns>
        public Gateway AddAuthenticationHeader(JObject data)
        {
            if (!config.ContainsKey(ADDITIONAL))
            {
                config.Add(ADDITIONAL, data);
            }
            else
            {
                JObject additional = (JObject)config.GetValue(ADDITIONAL);

                foreach (var item in data)
                {
                    additional.Add(item.Key, item.Value);
                }
            }

            return this;
        }

        /// <summary>
        /// Check if type is valid.
        /// </summary>
        /// <param name="requestType">string</param>
        /// <returns>bool</returns>
        public bool IsValidType(string requestType)
        {
            string[] types = new string[] { TP_REST, TP_SOAP };

            return types.Contains(requestType);
        }
    }
}
