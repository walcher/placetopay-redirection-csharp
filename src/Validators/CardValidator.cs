using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PlacetoPay.Redirection.Validators
{
    public class CardValidator
    {
        public const string VISA = "visa";
        public const string VISA_ELECTRON = "visa_electron";
        public const string AMEX = "amex";
        public const string MASTERCARD = "master";
        public const string CODENSA = "codensa";
        public const string DINERS = "diners";
        public const string JBC = "jbc";
        public const string DISCOVER = "discover";

        public static Dictionary<string, string> PATTERNS = new Dictionary<string, string>
        {
            { VISA_ELECTRON, @"^(4026|417500|4508|4844|491(3|7))" },
            { VISA, @"^4([0-9]{12}|[0-9]{15})$" },
            { CODENSA, @"^590712[0-9]{10}$" },
            { MASTERCARD, @"^5[1-5][0-9]{14}$" },
            { JBC, @"^35(2[89]|[3-8][0-9])" },
            { AMEX, @"^3[47][0-9]{13}$" },
            { DINERS, @"^3(0[0-5]|[68][0-9])[0-9]{11,13}$" },
            { DISCOVER, @"^(6011|622(12[6-9]|1[3-9][0-9]|[2-8][0-9]{2}|9[0-1][0-9]|92[0-5]|64[4-9])|65)" },
         };

        public static string[] FRANCHISES = {
            VISA,
            VISA_ELECTRON,
            AMEX,
            MASTERCARD,
            CODENSA,
            DINERS,
            JBC,
            DISCOVER,
        };

        /// <summary>
        /// Get Franchise by card number.
        /// </summary>
        /// <param name="number">string</param>
        /// <returns>string</returns>
        public static string CardNumberFranchise(string number)
        {
            foreach (var franchise in PATTERNS)
            {
                Regex regex = new Regex(franchise.Value);

                if (!string.IsNullOrEmpty(number) && regex.IsMatch(number))
                {
                    return franchise.Key;
                }
            }

            return null;
        }
    }
}
