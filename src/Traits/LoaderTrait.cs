using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PlacetoPay.Redirection.Traits
{
    /// <summary>
    /// Class <c>LoaderTrait</c>
    /// </summary>
    public class LoaderTrait
    {
        /// <summary>
        /// Set object properties.
        /// </summary>
        /// <param name="jsonData">JObject.</param>
        /// <param name="keys">JArray.</param>
        public void Load(JObject jsonData, JArray keys)
        {
            foreach (string key in keys)
            {
                if (jsonData.ContainsKey(key))
                {
                    JsonReader reader = new JsonTextReader(new StringReader(jsonData.ToString()))
                    {
                        DateParseHandling = DateParseHandling.None
                    };

                    JObject data = JObject.Load(reader);
                    PropertyInfo propertyInfo = GetType().GetProperty(StringFormatter.ToPascalCase(key));
                    JToken value = data.GetValue(key);

                    if (propertyInfo.PropertyType == typeof(int))
                    {
                        propertyInfo.SetValue(this, (int)value);
                    }
                    else if (propertyInfo.PropertyType == typeof(double))
                    {
                        propertyInfo.SetValue(this, (double)value);
                    }
                    else if (propertyInfo.PropertyType == typeof(bool))
                    {
                        propertyInfo.SetValue(this, (bool)value);
                    }
                    else
                    {
                        propertyInfo.SetValue(this, (string)value);
                    }
                }
            }
        }

        /// <summary>
        /// Set list of fields.
        /// </summary>
        /// <param name="fields">JArray</param>
        public void SetFields(JArray fields)
        {
            List<NameValuePair> list = new List<NameValuePair>();

            foreach (var field in fields)
            {
                JObject fieldDetail = field.ToObject<JObject>();

                list.Add(new NameValuePair(fieldDetail));
            }

            PropertyInfo propertyInfo = GetType().GetProperty("Fields");
            propertyInfo.SetValue(this, list);
        }
    }
}
