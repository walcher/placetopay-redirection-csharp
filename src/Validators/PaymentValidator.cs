using PlacetoPay.Redirection.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlacetoPay.Redirection.Validators
{
    /// <summary>
    /// Class <c>PaymentValidator</c>
    /// </summary>
    public class PaymentValidator : BaseValidator
    {
        /// <summary>
        /// Validates if payment entity contains the required information.
        /// </summary>
        /// <param name="entity">object</param>
        /// <param name="fields">list</param>
        /// <param name="silent">boold</param>
        /// <returns>bool</returns>
        public bool IsValid(object entity, out List<string> fields, bool silent)
        {
            List<string> errors = new List<string>();
            Payment payment = (Payment)entity;

            if (payment.Reference == null || MatchPattern(payment.Reference, @"/[ ]/"))
            {
                errors.Add("reference");
            }

            if (payment.Description != null && !MatchPattern(payment.Description, PATTERN_DESCRIPTION))
            {
                errors.Add("description");
            }

            if (payment.Amount == null)
            {
                errors.Add("amount");
            }

            if (errors?.Any() ?? false)
            {
                fields = errors;
                ThrowValidationException(errors, "Payment", silent);

                return false;
            }

            fields = null;

            return true;
        }
    }
}
