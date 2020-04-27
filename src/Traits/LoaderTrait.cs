using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PlacetoPay.Redirection.Traits
{
    /// <summary>
    /// Class <c>LoaderTrait</c>
    /// </summary>
    public class LoaderTrait : ILoaderTrait
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
                    PropertyInfo propertyInfo = GetType().GetProperty(ToPascalCase(key));
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
        /// Convert string to pascal case.
        /// </summary>
        /// <param name="original">string</param>
        /// <returns>string</returns>
        protected string ToPascalCase(string original)
        {
            Regex invalidCharsRgx = new Regex("[^_a-zA-Z0-9]");
            Regex whiteSpace = new Regex(@"(?<=\s)");
            Regex startsWithLowerCaseChar = new Regex("^[a-z]");
            Regex firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
            Regex lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
            Regex upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");

            var pascalCase = invalidCharsRgx.Replace(whiteSpace.Replace(original, "_"), string.Empty)
                .Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => startsWithLowerCaseChar.Replace(w, m => m.Value.ToUpper()))
                .Select(w => firstCharFollowedByUpperCasesOnly.Replace(w, m => m.Value.ToLower()))
                .Select(w => lowerCaseNextToNumber.Replace(w, m => m.Value.ToUpper()))
                .Select(w => upperCaseInside.Replace(w, m => m.Value.ToLower()));

            return string.Concat(pascalCase);
        }
    }
}
