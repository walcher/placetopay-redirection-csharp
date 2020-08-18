using System.Text.RegularExpressions;

namespace PlacetoPay.Redirection.Validators
{
    /// <summary>
    /// Class <c>PhoneNumber</c>
    /// </summary>
    public class PhoneNumber
    {
        public const string VALIDATION_PATTERN = @"/([0|\+?[0-9]{1,5})?([0-9 \(\)]{7,})([\(\)\w\d\. ]+)?/";

        /// <summary>
        /// Check if number phone is valid.
        /// </summary>
        /// <param name="number">string</param>
        /// <returns>bool</returns>
        public static bool IsValidNumber(string number)
        {
            Regex re = new Regex(VALIDATION_PATTERN);

            return !string.IsNullOrEmpty(number) && re.IsMatch(number);
        }
    }
}
