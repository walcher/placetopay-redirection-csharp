using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlacetoPay.Redirection.Validators
{
    public class PersonValidator : Country
    {
        public const string PATTERN_NAME = @"(?i)^[a-zñáéíóúäëïöüàèìòùÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙÇçÃã][a-zñáéíóúäëïöüàèìòùÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙÇçÃã\'\.\&\-\d ]{1,60}$(?-i)";
        public const string PATTERN_SURNAME = PATTERN_NAME;
        public const string PATTERN_EMAIL = @"^([a-zA-Z0-9_\.\-])+[^\.\-\ ]\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})$";
        public const string PATTERN_MOBILE = PhoneNumber.VALIDATION_PATTERN;
        public const string PATTERN_CITY = @"(?i)^[a-zñáéíóúäëïöüàèìòùÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙÇçÃã\'\. ]{2,50}$(?-i)";
        public const string PATTERN_STATE = PATTERN_NAME;
        public const string PATTERN_STREET = @"(?i)^[a-zñáéíóúäëïöüàèìòùÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙÇçÃã\'\.\,\&\-\#\\_\s\d\(\)]{2,250}$(?-i)";
        public const string PATTERN_PHONE = PhoneNumber.VALIDATION_PATTERN;
        public const string PATTERN_POSTALCODE = @"^[0-9]{4,8}$";
        public const string PATTERN_COUNTRY = @"^[A-Z]{2}$";

        /// <summary>
        /// Get the property name pattern.
        /// </summary>
        /// <param name="field">String of the field to be searched</param>
        /// <param name="cleanLimiters">Check if remove the pattern limiter</param>
        /// <returns>Return regex.</returns>
        public static string GetPattern(string field, bool cleanLimiters = false)
        {
            try
            {
                string name = $"PATTERN_{field.ToUpper()}";
                string pattern = (string)typeof(PersonValidator).GetField(name).GetValue(null);

                if (cleanLimiters)
                {
                    return pattern.Substring(0, pattern.Length - 5).Substring(4);
                }

                return pattern;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Validates if person entity contains the required information.
        /// </summary>
        /// <param name="entity">object</param>
        /// <param name="fields">list</param>
        /// <param name="silent">boold</param>
        /// <returns>bool</returns>
        public bool IsValid(object entity, out List<string> fields, bool silent)
        {
            List<string> errors = new List<string>();
            Person person = (Person)entity;

            if (person.Name == null || !MatchPattern(person.Name, PATTERN_NAME))
            {
                errors.Add("name");
            }

            if (person.Surname != null && !MatchPattern(person.Surname, PATTERN_SURNAME) && !person.IsBusiness())
            {
                errors.Add("surname");
            }

            if (person.Email != null && !MatchPattern(person.Email, PATTERN_EMAIL))
            {
                errors.Add("email");
            }

            if (person.Document != null)
            {
                if (person.DocumentType == null)
                {
                    errors.Add("documentType");
                    errors.Add("document");
                }

                if (!DocumentHelper.IsValidDocument(person.DocumentType, person.Document))
                {
                    errors.Add("documentType");
                    errors.Add("document");
                }
            }

            if (person.Mobile != null && !PhoneNumber.IsValidNumber(person.Mobile))
            {
                errors.Add("mobile");
            }

            if (errors?.Any() ?? false)
            {
                fields = errors;
                ThrowValidationException(errors, "Person", silent);

                return false;
            }

            fields = null;

            return true;
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
