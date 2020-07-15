using Newtonsoft.Json.Linq;

namespace PlacetoPay.Redirection.Contracts
{
    /// <summary>
    /// Class <c>Entity</c>
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// ToJsonObject static method.
        /// </summary>
        /// <returns>JObject</returns>
        public abstract JObject ToJsonObject();
    }
}
