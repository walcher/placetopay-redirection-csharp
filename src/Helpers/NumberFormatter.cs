using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text.RegularExpressions;

namespace PlacetoPay.Redirection.Helpers
{
    /// <summary>
    /// Class <c>NumberFormatter</c>
    /// </summary>
    public class NumberFormatter
    {
        /// <summary>
        /// Convert between double and long numbers.
        /// </summary>
        /// <param name="source">JObject</param>
        /// <returns>JObject</returns>
        public static JObject NormalizeNumber(JObject source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new JObject(source);

            var properties = target.DescendantsAndSelf()
                .Where(x => x.Type == JTokenType.Float)
                .Select(x => x.Parent)
                .OfType<JProperty>();

            foreach (var property in properties.Where(x => Regex.IsMatch(x.Value.ToObject<string>(), @"^\d*$")))
            {
                property.Value = property.Value.ToObject<long>();
            }

            return target;
        }
    }
}
