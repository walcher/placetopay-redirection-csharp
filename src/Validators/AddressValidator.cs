using PlacetoPay.Redirection.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlacetoPay.Redirection.Validators
{
    /// <summary>
    /// Class <c>AddressValidator</c>
    /// </summary>
    public class AddressValidator : PersonValidator
    {
        /// <summary>
        /// Validates if address entity contains the required information.
        /// </summary>
        /// <param name="entity">object</param>
        /// <param name="fields">list</param>
        /// <param name="silent">boold</param>
        /// <returns>bool</returns>
        public new bool IsValid(object entity, out List<string> fields, bool silent)
        {
            List<string> errors = new List<string>();
            Address address = (Address)entity;

            if (address.Street == null)
            {
                errors.Add("street");
            }

            if (address.City == null && !MatchPattern(address.City, PATTERN_CITY))
            {
                errors.Add("city");
            }

            if (address.Country == null || !IsValidCountryCode(address.Country))
            {
                errors.Add("country");
            }

            if (address.Phone == null && !PhoneNumber.IsValidNumber(address.Phone))
            {
                errors.Add("phone");
            }

            if (address.PostalCode == null && MatchPattern(address.PostalCode, PATTERN_POSTALCODE))
            {
                errors.Add("postalCode");
            }

            if (errors?.Any() ?? false)
            {
                fields = errors;
                ThrowValidationException(errors, "Address", silent);

                return false;
            }

            fields = null;

            return true;
        }
    }
}
