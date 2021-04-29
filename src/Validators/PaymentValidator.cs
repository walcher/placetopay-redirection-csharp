using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlacetoPay.Redirection.Validators
{
    public class PaymentValidator : BaseValidator
    {
        /// <summary>
        /// Validates if payment entity contains the required information.
        /// </summary>
        /// <param name="entity">Payment instance</param>
        /// <param name="fields">List of errors</param>
        /// <param name="silent">Error exception coul be silent</param>
        /// <returns>True or false, depends on error list.</returns>
        public bool IsValid(object entity, out List<string> fields, bool silent)
        {
            List<string> errors = new List<string>();
            Payment payment = (Payment)entity;

            if (payment.Reference == null || !MatchPattern(payment.Reference, PATTERN_REFERENCE))
            {
                errors.Add("reference");
            }

            if (payment.Description != null && (payment.Description.GetType() == typeof(JArray) || !MatchPattern(payment.Description, PATTERN_DESCRIPTION)))
            {
                errors.Add("description");
            }

            if (payment.Amount == null || payment.Amount.Total <= 0)
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
