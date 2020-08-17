using System;

namespace PlacetoPay.Redirection.Validators
{
    /// <summary>
    /// Class <c>PersonValidator</c>
    /// </summary>
    public class PersonValidator : Country
    {
        public const string PATTERN_NAME = @"/^[a-zñáéíóúäëïöüàèìòùÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙÇçÃã][a-zñáéíóúäëïöüàèìòùÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙÇçÃã\'\.\&\-\d ]{2,60}$/i";
        public const string PATTERN_SURNAME = PATTERN_NAME;
        public const string PATTERN_EMAIL = @"/^([a-zA-Z0-9_\.\-])+[^\.\-\ ]\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})$/";
        public const string PATTERN_MOBILE = PhoneNumber.VALIDATION_PATTERN;
        public const string PATTERN_CITY = @"/^[a-zñáéíóúäëïöüàèìòùÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙÇçÃã\'\. ]{2,50}$/i";
        public const string PATTERN_STATE = PATTERN_NAME;
        public const string PATTERN_STREET = @"/^[a-zñáéíóúäëïöüàèìòùÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙÇçÃã\'\.\,\&\-\#\_\s\d\(\)]{2,250}$/i";
        public const string PATTERN_PHONE = PhoneNumber.VALIDATION_PATTERN;
        public const string PATTERN_POSTALCODE = @"/^[0-9]{4,8}$/";
        public const string PATTERN_COUNTRY = @"/^[A-Z]{2}$/";

        /// <summary>
        /// Get the property name pattern.
        /// </summary>
        /// <param name="field">string</param>
        /// <param name="cleanLimiters">bool</param>
        /// <returns>string</returns>
        public static string GetPattern(string field, bool cleanLimiters = false)
        {
            try
            {
                string name = $"PATTERN_{field.ToUpper()}";
                string pattern = name;

                if (cleanLimiters)
                {
                    pattern.Substring(1, -1);
                }

                return pattern;
            } 
            catch(Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Normalize phone number.
        /// </summary>
        /// <param name="phone">string</param>
        /// <returns>string</returns>
        public static string NormalizePhone(string phone)
        {
            if (phone != null)
            {
                phone = phone.Replace("+57", "");
            }

            return phone;
        }
    }
}
