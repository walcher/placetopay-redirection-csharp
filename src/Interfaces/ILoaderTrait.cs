using Newtonsoft.Json.Linq;

namespace PlacetoPay.Redirection.Interfaces
{
    /// <summary>
    /// Interface <c>ILoaderTrait</c>
    /// </summary>
    public interface ILoaderTrait
    {
        /// <summary>
        /// Load static method.
        /// </summary>
        /// <param name="jsonData">JObject.</param>
        /// <param name="keys">JArray.</param>
        void Load(JObject jsonData, JArray keys);
    }
}
