using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace PlacetoPay.Redirection.Helpers
{
    /// <summary>
    /// Class <c>JsonFormatter</c>
    /// </summary>
    public static class JsonFormatter
    {
        /// <summary>
        /// Remove null and empty data from json.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>JToken</returns>
        public static JToken RemoveNullOrEmpty(object data)
        {
            JsonReader reader = new JsonTextReader(new StringReader(data.ToString()))
            {
                DateParseHandling = DateParseHandling.None
            };

            JToken token = JToken.ReadFrom(reader);

            if (token.Type == JTokenType.Object)
            {
                JObject copy = new JObject();

                foreach (JProperty prop in token.Children<JProperty>())
                {
                    JToken child = prop.Value;

                    if (child.HasValues)
                    {
                        child = RemoveNullOrEmpty(child);
                    }

                    if (!IsEmpty(child))
                    {
                        copy.Add(prop.Name, child);
                    }
                }

                return copy;
            }
            else if (token.Type == JTokenType.Array)
            {
                JArray copy = new JArray();

                foreach (JToken item in token.Children())
                {
                    JToken child = item;

                    if (child.HasValues)
                    {
                        child = RemoveNullOrEmpty(child);
                    }

                    if (!IsEmpty(child))
                    {
                        copy.Add(child);
                    }
                }

                return copy;
            }

            return token;
        }

        /// <summary>
        /// Validate json key.
        /// </summary>
        /// <param name="token">JToken</param>
        /// <returns>bool</returns>
        public static bool IsEmpty(JToken token)
        {
            return (token.Type == JTokenType.Null) ||
                (token.Type == JTokenType.Integer && (int)token == 0) ||
                (token.Type == JTokenType.Array && !token.HasValues) ||
                (token.Type == JTokenType.Object && !token.HasValues);
        }

        /// <summary>
        /// Parse json from string.
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>JObject</returns>
        public static JObject ParseJObject(string data)
        {
            JsonReader reader = new JsonTextReader(new StringReader(data))
            {
                DateParseHandling = DateParseHandling.None
            };

            return JObject.Load(reader);
        }
    }
}
