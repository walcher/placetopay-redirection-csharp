using PlacetoPay.Redirection.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlacetoPay.Redirection.Validators
{
    /// <summary>
    /// Class <c>TaxDetailValidator</c>
    /// </summary>
    public class TaxDetailValidator : BaseValidator
    {
        public const string TP_IVA = "valueAddedTax";
        public const string TP_IPO = "exciseDuty";

        public static string[] TYPES =
        {
            TP_IVA,
            TP_IPO,
        };

        /// <summary>
        /// Check if kind is valid.
        /// </summary>
        /// <param name="kind">string</param>
        /// <returns>bool</returns>
        public static bool IsValidKind(string kind = null)
        {
            if (!string.IsNullOrEmpty(kind))
            {
                return false;
            }

            return TYPES.Contains(kind);
        }

        /// <summary>
        /// Validates if tax detail entity contains the required information.
        /// </summary>
        /// <param name="entity">object</param>
        /// <param name="fields">list</param>
        /// <param name="silent">boold</param>
        /// <returns>bool</returns>
        public bool IsValid(object entity, out List<string> fields, bool silent)
        {
            List<string> errors = new List<string>();
            TaxDetail taxDetail = (TaxDetail)entity;

            if (taxDetail.Kind == null || !IsValidKind(taxDetail.Kind))
            {
                errors.Add("kind");
            }

            if (taxDetail.Amount <= 0)
            {
                errors.Add("amount");
            }

            if (taxDetail.Base <= 0 || taxDetail.Base < taxDetail.Amount)
            {
                errors.Add("base");
            }

            if (errors?.Any() ?? false)
            {
                fields = errors;
                ThrowValidationException(errors, "TaxDetail", silent);

                return false;
            }

            fields = null;

            return true;
        }
    }
}
