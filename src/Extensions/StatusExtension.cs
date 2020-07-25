using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Entities;
using System.Reflection;

namespace PlacetoPay.Redirection.Extensions
{
    /// <summary>
    /// Class <c>StatusExtension</c>
    /// </summary>
    public static class StatusExtension
    {
        private const string STATUS_PROPERTY = "Status";

        /// <summary>
        /// Get status object.
        /// </summary>
        /// <typeparam name="T">class</typeparam>
        /// <param name="obj">object</param>
        /// <returns>Status</returns>
        public static Status GetStatus<T>(this object obj)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(STATUS_PROPERTY);
            Status status = (Status)propertyInfo.GetValue(obj);

            if (status != null)
            {
                return status;
            }

            return new Status(new JObject { 
                { "status", Status.ST_ERROR },
                { "message", "No response status was provisioned" },
                { "reason", "" }
            });
        }
    }
}
