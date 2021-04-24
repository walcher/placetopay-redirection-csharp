using PlacetoPay.Redirection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PlacetoPay.Redirection.Validators
{
    /// <summary>
    /// Class <c>BaseValidator</c>
    /// </summary>
    public class BaseValidator
    {
        public const string PATTERN_REFERENCE = @"^[\d\w\-\.,\$#\/\\\'!]{1,32}$/";
        public const string PATTERN_DESCRIPTION = @"^[a-zñáéíóúäëïöüàèìòùÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙÇçÃã\s\d\.,\$#\&\-\(_)(\)\/\%\+\\\']{2,250}$";

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
            Regex re = new Regex(pattern, RegexOptions.IgnoreCase);

            return !string.IsNullOrEmpty(value) && re.IsMatch(value);
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

        /// <summary>
        /// Check if the string length is valid.
        /// </summary>
        /// <param name="value">string</param>
        /// <param name="min">int</param>
        /// <param name="max">int</param>
        /// <param name="required">bool</param>
        /// <returns>bool</returns>
        public static bool IsValidLengthString(string value, int min, int max, bool required = false)
        {
            if (required && IsAnyNullOrEmpty(value))
            {
                return false;
            }

            if (value.Length < min || value.Length > max)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if value is int.
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>bool</returns>
        public static bool IsInteger(string value)
        {
            return int.TryParse(value, out _);
        }

        /// <summary>
        /// Check if value is numeric.
        /// </summary>
        /// <param name="value">string</param>
        /// <returns>bool</returns>
        public static bool IsNumeric(string value)
        {
            return double.TryParse(value, out _);
        }

        /// <summary>
        /// Check actual date.
        /// </summary>
        /// <param name="date">string</param>
        /// <param name="minDifference">int</param>
        /// <returns>bool</returns>
        public static bool IsActualDate(string date, int minDifference = -1)
        {
            //TODO: Refactor this method!
            var current = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long unix = (DateTime.Parse(date).ToUniversalTime() - unixStart).Ticks;

            return (long)current.TotalSeconds - (double)unix / TimeSpan.TicksPerSecond > minDifference;
        }

        /// <summary>
        /// Parse given date string.
        /// </summary>
        /// <param name="date">string</param>
        /// <param name="format">string</param>
        /// <returns>string</returns>
        public static string ParseDate(string date, string format = "yyyy-MM-ddTHH\\:mm\\:sszzz")
        {
            if (DateTime.TryParse(date, out DateTime time))
            {
                return time.ToString(format);
            }

            return null;
        }

        /// <summary>
        /// Check if url is a valid one.
        /// </summary>
        /// <param name="url">string</param>
        /// <returns>bool</returns>
        public static bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uri)
                && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// Throw exception.
        /// </summary>
        /// <param name="fields">List</param>
        /// <param name="from">string</param>
        /// <param name="silent">bool</param>
        /// <param name="message">string</param>
        public static void ThrowValidationException(List<string> fields, string from, bool silent = true, string message = null)
        {
            if (!silent)
            {
                throw new EntityValidationFailException(fields, from, message);
            }
        }
    }
}
