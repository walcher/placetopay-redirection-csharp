using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Helpers;
using System.Reflection;

namespace PlacetoPay.Redirection.Extensions
{
    /// <summary>
    /// Class <c>LoaderExtension</c>
    /// </summary>
    public static class LoaderExtension
    {
        /// <summary>
        /// Set object properties.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="jsonData">JObject</param>
        /// <param name="keys">JArray</param>
        public static void Load<T>(this object obj, JObject jsonData, JArray keys)
        {
            foreach (string key in keys)
            {
                if (jsonData.ContainsKey(StringFormatter.NormalizeProperty(key)))
                {
                    JObject data = JObject.Parse(jsonData.ToString());
                    PropertyInfo propertyInfo = obj.GetType().GetProperty(StringFormatter.ToPascalCase(key));
                    JToken value = data.GetValue(StringFormatter.NormalizeProperty(key));

                    if (propertyInfo.PropertyType == typeof(int))
                    {
                        propertyInfo.SetValue(obj, (int)value);
                    }
                    else if (propertyInfo.PropertyType == typeof(double))
                    {
                        propertyInfo.SetValue(obj, (double)value);
                    }
                    else if (propertyInfo.PropertyType == typeof(bool))
                    {
                        propertyInfo.SetValue(obj, (bool)value);
                    }
                    else
                    {
                        propertyInfo.SetValue(obj, (string)value);
                    }
                }
            }
        }
    }
}
