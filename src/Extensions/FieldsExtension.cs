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
        private const string FIELDS_PROPERTY = "Fields";

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

            PropertyInfo propertyInfo = obj.GetType().GetProperty(FIELDS_PROPERTY);
            propertyInfo.SetValue(obj, list);
        }

        /// <summary>
        /// Convert fields list to json array.
        /// </summary>
        /// <typeparam name="T">parent class</typeparam>
        /// <param name="obj">object</param>
        /// <returns>JArray</returns>
        public static JArray FieldsToJArray<T>(this object obj)
        {
            JArray fields = new JArray();
            PropertyInfo propertyInfo = obj.GetType().GetProperty(FIELDS_PROPERTY);
            List<NameValuePair> list = (List<NameValuePair>)propertyInfo.GetValue(obj);

            if (list != null)
            {
                foreach (var data in list)
                {
                    fields.Add(data.ToJsonObject());
                }
            }

            return fields;
        }
    }
}
