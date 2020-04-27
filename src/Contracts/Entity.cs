using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Traits;

namespace PlacetoPay.Redirection.Contracts
{
    /// <summary>
    /// Class <c>Entity</c>
    /// </summary>
    public abstract class Entity : LoaderTrait
    {
        /// <summary>
        /// ToJsonObject static method.
        /// </summary>
        /// <returns>JObject</returns>
        public abstract JObject ToJsonObject();
    }
}
