using PlacetoPay.Redirection.Entities;
using PlacetoPay.Redirection.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlacetoPay.Redirection.Validators
{
    /// <summary>
    /// Class <c>PersonValidator</c>
    /// </summary>
    public class PersonValidator : Country
    {
        //public const string PATTERN_NAME = @"^[\p{L} \.\-]+$";
        public const string PATTERN_NAME = @"^[a-zñáéíóúäëïöüàèìòùÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙÇçÃã][a-zñáéíóúäëïöüàèìòùÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙÇçÃã\'\.\&\-\d ]{2,60}";
        public const string PATTERN_SURNAME = PATTERN_NAME;
        public const string PATTERN_EMAIL = @"^([a-zA-Z0-9_\.\-])+[^\.\-\ ]\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})$";
        public const string PATTERN_MOBILE = PhoneNumber.VALIDATION_PATTERN;
        public const string PATTERN_CITY = @"^[a-zñáéíóúäëïöüàèìòùÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙÇçÃã\'\. ]{2,50}";
        public const string PATTERN_STATE = PATTERN_NAME;
        public const string PATTERN_STREET = @"^[a-zñáéíóúäëïöüàèìòùÑÁÉÍÓÚÄËÏÖÜÀÈÌÒÙÇçÃã\'\.\,\&\-\#\s\d\(\)\(_)]{2,250}";
        public const string PATTERN_PHONE = PhoneNumber.VALIDATION_PATTERN;
        public const string PATTERN_POSTALCODE = @"^[0-9]{4,8}$";
        public const string PATTERN_COUNTRY = @"^[A-Z]{2}$";

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
                string pattern = (string)typeof(PersonValidator).GetField(name).GetValue(null);

                if (cleanLimiters)
                {
                    pattern.Substring(1, -1);
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
