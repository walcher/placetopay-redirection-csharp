using Newtonsoft.Json.Linq;

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
        /// <param name="token">JToken</param>
        /// <returns>JToken</returns>
        public static JToken RemoveEmptyChildren(JToken token)
        {
            if (token.Type == JTokenType.Object)
            {
                JObject copy = new JObject();

                foreach (JProperty prop in token.Children<JProperty>())
                {
                    JToken child = prop.Value;

                    if (child.HasValues)
                    {
                        child = RemoveEmptyChildren(child);
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
                        child = RemoveEmptyChildren(child);
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
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues);
        }
    }
}
