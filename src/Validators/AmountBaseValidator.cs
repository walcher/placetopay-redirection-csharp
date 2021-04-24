using PlacetoPay.Redirection.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlacetoPay.Redirection.Validators
{
    /// <summary>
    /// Class <c>AmountBaseValidator</c>
    /// </summary>
    public class AmountBaseValidator : BaseValidator
    {
        /// <summary>
        /// Validates if amount base entity contains the required information.
        /// </summary>
        /// <param name="entity">object</param>
        /// <param name="fields">list</param>
        /// <param name="silent">boold</param>
        /// <returns>bool</returns>
        public bool IsValid(object entity, out List<string> fields, bool silent)
        {
            List<string> errors = new List<string>();
            AmountBase amountBase = (AmountBase)entity;

            if (amountBase.Currency == null || !Currency.IsValidCurrency(amountBase.Currency))
            {
                errors.Add("currency");
            }

            if (amountBase.Total == 0)
            {
                errors.Add("total");
            }

            if (errors?.Any() ?? false)
            {
                fields = errors;
                ThrowValidationException(errors, "AmountBase", silent);

                return false;
            }

            fields = null;

            return true;
        }
    }
}
