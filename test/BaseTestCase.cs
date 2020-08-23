using DotNetEnv;
using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection;
using PlacetoPay.Redirection.Contracts;

namespace PlacetoPay.RedirectionTests
{
    /// <summary>
    /// Class <c>BaseTestCase</c>
    /// </summary>
    public class BaseTestCase
    {
        /// <summary>
        /// Get gateway.
        /// </summary>
        /// <param name="data">JObject</param>
        /// <returns>Gateway</returns>
        public Gateway GetGateway(JObject data)
        {
            Env.Load();

            JObject config = new JObject {
                { "login", Env.GetString("P2P_LOGIN") },
                { "tranKey", Env.GetString("P2P_TRANKEY") },
                { "url", Env.GetString("P2P_URL") },
            };

            config.Merge(data, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });

            return new PlacetoPayRedirection(config);
        }
    }
}
