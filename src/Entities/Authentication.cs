using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Exceptions;
using PlacetoPay.Redirection.Helpers;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Authentication</c>
    /// </summary>
    public class Authentication : Entity
    {
        public const string WSU = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
        public const string WSSE = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
        public const string LOGIN = "login";
        public const string TRANKEY = "tranKey";
        public const string AUTH = "auth";
        public const string AUTH_TYPE = "auth_type";
        public const string AUTH_ADDITIONAL = "auth_additional";
        public const string ADDITIONAL = "additional";
        public const string ALGORITHM = "algorithm";
        public const string SEED = "seed";
        public const string NONCE = "nonce";

        protected string login;
        protected string tranKey;
        protected AuthenticationSecurity auth;
        protected bool overrided = false;
        protected string type = "full";
        protected JObject additional;
        protected string algorithm = "sha1";

        /// <summary>
        /// Authentication constructor.
        /// </summary>
        public Authentication() { }

        /// <summary>
        /// Authentication constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Authentication(string data) : this(JsonFormatter.ParseJObject(data)) { }

        /// <summary>
        /// Authentication constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Authentication(JObject data)
        {
            if (!data.ContainsKey(LOGIN) || !data.ContainsKey(TRANKEY))
            {
                throw new PlacetoPayException("No login or tranKey provided on authentication");
            }

            login = data.GetValue(LOGIN).ToString();
            tranKey = data.GetValue(TRANKEY).ToString();

            if (data.ContainsKey(AUTH))
            {
                if (!data.GetValue(AUTH).ToObject<JObject>().ContainsKey(SEED)
                    || !data.GetValue(AUTH).ToObject<JObject>().ContainsKey(NONCE))
                {
                    throw new PlacetoPayException("Bad definition for the override");
                }

                auth = new AuthenticationSecurity(data.GetValue(AUTH).ToString());
                overrided = true;
            }

            if (data.ContainsKey(AUTH_TYPE))
            {
                type = data.GetValue(AUTH_TYPE).ToString();
            }

            if (data.ContainsKey(AUTH_ADDITIONAL))
            {
                additional = data.GetValue(AUTH_ADDITIONAL).ToObject<JObject>();
            }

            if (data.ContainsKey(ALGORITHM))
            {
                algorithm = data.GetValue(ALGORITHM).ToString();
            }

            Generate();
        }

        /// <summary>
        /// Authentication constructor.
        /// </summary>
        /// <param name="login">string</param>
        /// <param name="trankey">string</param>
        /// <param name="auth">AuthenticationSecurity</param>
        public Authentication(string login, string trankey, AuthenticationSecurity auth) : this(login, trankey, auth, null) { }

        /// <summary>
        /// Authentication constructor.
        /// </summary>
        /// <param name="login">string</param>
        /// <param name="tranKey">string</param>
        /// <param name="auth">AuthenticationSecurity</param>
        /// <param name="additional">JObject</param>
        public Authentication(string login, string tranKey, AuthenticationSecurity auth, JObject additional)
        {
            if (login == null || tranKey == null)
            {
                throw new PlacetoPayException("No login or tranKey provided on authentication");
            }
            this.login = login;
            this.tranKey = tranKey;

            if (auth != null)
            {
                if (auth.Seed == null || auth.Nonce == null)
                {
                    throw new PlacetoPayException("Bad definition for the override");
                }

                this.auth = auth;
                overrided = true;
            }

            this.additional = additional;
            Generate();
        }

        /// <summary>
        /// Login property.
        /// </summary>
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        /// <summary>
        /// TranKey property.
        /// </summary>
        public string TranKey
        {
            get { return tranKey; }
            set { tranKey = value; }
        }

        /// <summary>
        /// Auth property.
        /// </summary>
        public AuthenticationSecurity Auth
        {
            get { return auth; }
            set { auth = value; }
        }

        /// <summary>
        /// Additional property.
        /// </summary>
        public JObject Additional
        {
            get { return additional; }
            set { additional = value; }
        }

        /// <summary>
        /// Get seed.
        /// </summary>
        /// <returns>string</returns>
        public string GetSeed()
        {
            if (auth != null)
            {
                return auth.Seed;
            }

            return (DateTime.Now).ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
        }

        /// <summary>
        /// Get nonce.
        /// </summary>
        /// <param name="encoded">bool</param>
        /// <returns>string</returns>
        public string GetNonce(bool encoded = true)
        {
            string nonce;

            if (auth != null)
            {
                nonce = auth.Nonce;
            }
            else
            {
                nonce = CryptoHelper.MakeRandom();
            }

            if (encoded)
            {
                nonce = CryptoHelper.MakeBase64(nonce);
            }

            return nonce;
        }

        /// <summary>
        /// Get digest.
        /// </summary>
        /// <param name="encoded">bool</param>
        /// <returns>string</returns>
        public string Digest(bool encoded = true)
        {
            object digest;

            if (type == "full")
            {
                digest = CryptoHelper.ComputeHash($"{GetNonce(false)}{GetSeed()}{tranKey}", algorithm, true);
            }
            else
            {
                digest = CryptoHelper.ComputeHash($"{GetSeed()}{tranKey}", algorithm);
            }

            if (encoded)
            {
                return CryptoHelper.MakeBase64(digest);
            }

            return (string)digest;
        }

        /// <summary>
        /// Generate auth data.
        /// </summary>
        /// <returns>Authentication</returns>
        public Authentication Generate()
        {
            if (!overrided)
            {
                auth = new AuthenticationSecurity(GetSeed(), GetNonce());
            }

            return this;
        }

        /// <summary>
        /// Set additional data.
        /// </summary>
        /// <param name="data">JObject</param>
        /// <returns>Authentication</returns>
        public Authentication SetAdditional(JObject data)
        {
            additional = data;

            return this;
        }

        /// <summary>
        /// Convert data to soap header.
        /// </summary>
        /// <returns>String soap security header</returns>
        public string AsSoapHeader()
        {
            string header = @"
            <wsse:Security xml:mustUnderstand=""1"" xmlns:wsse=""{1}"">
                <wsse:UsernameToken wsu:Id=""UsernameToken"" xmlns:wsu=""{0}"">
                    <wsse:Username>{2}</wsse:Username>
        		    <wsse:Password>{3}</wsse:Password>
        		    <wsse:Nonce>{4}</wsse:Nonce>
        		    <wsu:Created>{5}</wsu:Created>
                </wsse:UsernameToken>
            </wsse:Security>";
            
            return string.Format(header, WSU, WSSE, Login, Digest(), GetNonce(), GetSeed());
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject {
                { LOGIN, Login },
                { TRANKEY, Digest() },
                { NONCE, GetNonce() },
                { SEED, GetSeed() },
                { ADDITIONAL, Additional },
            });
        }
    }
}
