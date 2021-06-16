using DotNetEnv;
using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection;
using PlacetoPay.Redirection.Contracts;

namespace PlacetoPay.RedirectionTests
{
    public class BaseTestCase
    {
        /// <summary>
        /// Get gateway.
        /// </summary>
        /// <param name="data">Additional configuration data</param>
        /// <returns>PlacetoPayRedirection instance</returns>
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
