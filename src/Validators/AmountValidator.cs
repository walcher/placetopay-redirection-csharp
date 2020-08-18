using PlacetoPay.Redirection.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlacetoPay.Redirection.Validators
{
    /// <summary>
    /// Class <c>AmountValidator</c>
    /// </summary>
    public class AmountValidator : BaseValidator
    {
        /// <summary>
        /// Validates if amount entity contains the required information.
        /// </summary>
        /// <param name="entity">object</param>
        /// <param name="fields">list</param>
        /// <param name="silent">boold</param>
        /// <returns>bool</returns>
        public bool IsValid(object entity, out List<string> fields, bool silent)
        {
            List<string> errors = new List<string>();
            Amount amount = (Amount)entity;

            if (amount.Taxes?.Any() ?? false)
            {
                errors.Add("taxes");
            }

            if (amount.Details?.Any() ?? false)
            {
                errors.Add("details");
            }

            if (errors?.Any() ?? false)
            {
                fields = errors;
                ThrowValidationException(errors, "Amount", silent);

                return false;
            }

            fields = null;

            return true;
        }
    }
}
