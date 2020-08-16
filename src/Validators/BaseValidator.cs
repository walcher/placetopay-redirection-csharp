using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PlacetoPay.Redirection.Validators
{
    /// <summary>
    /// Class <c>BaseValidator</c>
    /// </summary>
    public class BaseValidator
    {
        public const string PATTERN_REFERENCE = @"/^[\d\w\-\.,\$#\/\\\'!]{1,32}$/";
        public const string PATTERN_DESCRIPTION = @"/^[a-zñáéíóúäëïöüàèìòùÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙÇçÃã\s\d\.,\$#\&\-\_(\)\/\%\+\\\']{2,250}$/i";

        /// <summary>
        /// Check if the IP is valid.
        /// </summary>
        /// <param name="ip">string</param>
        /// <returns>bool</returns>
        public static bool IsValidIp(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
            {
                return false;
            }

            string[] splitValues = ip.Split('.');

            if (splitValues.Length != 4)
            {
                return false;
            }

            return splitValues.All(r => byte.TryParse(r, out byte temp));
        }

        /// <summary>
        /// Get the value type and check if valid.
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>bool</returns>
        public static bool IsAnyNullOrEmpty(object obj)
        {
            if (obj is null)
            {
                return true;
            }

            return obj.GetType().GetProperties().Any(x => IsNullOrEmpty(x.GetValue(obj)));
        }

        /// <summary>
        /// Check if the value is null or empty.
        /// </summary>
        /// <param name="value">object</param>
        /// <returns></returns>
        private static bool IsNullOrEmpty(object value)
        {
            if (value is null)
            {
                return true;
            }

            var type = value.GetType();

            return type.IsValueType && Equals(value, Activator.CreateInstance(type));
        }

        /// <summary>
        /// Check if is a valid pattern.
        /// </summary>
        /// <param name="value">string</param>
        /// <param name="pattern">string</param>
        /// <returns>bool</returns>
        public static bool MatchPattern(string value, string pattern)
        {
            Regex re = new Regex(pattern);

            return re.IsMatch(value);
        }

        /// <summary>
        /// Check if the value is a valid string.
        /// </summary>
        /// <param name="value">string</param>
        /// <param name="min">int</param>
        /// <param name="required">bool</param>
        /// <returns>bool</returns>
        public static bool IsValidString(string value, int min, bool required)
        {
            if (required && IsAnyNullOrEmpty(value))
            {
                return false;
            }

            if (value.Length < min)
            {
                return false;
            }

            return true;
        }
    }
}
