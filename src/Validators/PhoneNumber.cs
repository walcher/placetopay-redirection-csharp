using System.Text.RegularExpressions;

namespace PlacetoPay.Redirection.Validators
{
    public class PhoneNumber
    {
        public const string VALIDATION_PATTERN = @"([0|\+?[0-9]{1,5})?([0-9 \(\)]{7,})([\(\)\w\d\. ]+)?";

        /// <summary>
        /// Check if number phone is valid.
        /// </summary>
        /// <param name="number">Number to be checked</param>
        /// <returns>True or false, depends on pattern.</returns>
        public static bool IsValidNumber(string number)
        {
            Regex re = new Regex(VALIDATION_PATTERN);

            return !string.IsNullOrEmpty(number) && re.IsMatch(number);
        }
    }
}
