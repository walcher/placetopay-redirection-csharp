using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Entities;
using System.Collections.Generic;
using System.Reflection;

namespace PlacetoPay.Redirection.Extensions
{
    /// <summary>
    /// Class <c>FieldsExtension</c>
    /// </summary>
    public static class FieldsExtension
    {
        /// <summary>
        /// Set list of fields.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="fields">JArray</param>
        public static void SetFields<T>(this object obj, JArray fields)
        {
            List<NameValuePair> list = new List<NameValuePair>();

            foreach (var field in fields)
            {
                JObject fieldDetail = field.ToObject<JObject>();

                list.Add(new NameValuePair(fieldDetail));
            }

            PropertyInfo propertyInfo = obj.GetType().GetProperty("Fields");
            propertyInfo.SetValue(obj, list);
        }
    }
}
