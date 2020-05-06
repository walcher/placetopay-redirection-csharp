using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Entities;
using System.Collections.Generic;

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

        /// <summary>
        /// Set 
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        void SetFields(JArray fields);
    }
}
