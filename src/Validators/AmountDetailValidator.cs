using PlacetoPay.Redirection.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlacetoPay.Redirection.Validators
{
    /// <summary>
    /// Class <c>AmountDetailValidator</c>
    /// </summary>
    public class AmountDetailValidator : BaseValidator
    {
        public const string TP_DISCOUNT = "discount";
        public const string TP_ADDITIONAL = "additional";
        public const string TP_DEVOLUTION_BASE = "vatDevolutionBase";
        public const string TP_SHIPPING = "shipping";
        public const string TP_HANDLING_FEE = "handlingFee";
        public const string TP_INSURANCE = "insurance";
        public const string TP_GIFT_WRAP = "giftWrap";
        public const string TP_SUBTOTAL = "subtotal";
        public const string TP_FEE = "fee";
        public const string TP_TIP = "tip";

        public static string[] TYPES = {
            TP_DISCOUNT,
            TP_ADDITIONAL,
            TP_DEVOLUTION_BASE,
            TP_SHIPPING,
            TP_HANDLING_FEE,
            TP_INSURANCE,
            TP_GIFT_WRAP,
            TP_SUBTOTAL,
            TP_FEE,
            TP_TIP,
        };

        /// <summary>
        /// Validates if amount detail entity contains the required information.
        /// </summary>
        /// <param name="entity">object</param>
        /// <param name="fields">list</param>
        /// <param name="silent">boold</param>
        /// <returns>bool</returns>
        public bool IsValid(object entity, out List<string> fields, bool silent)
        {
            List<string> errors = new List<string>();
            AmountDetail amountDetail = (AmountDetail)entity;

            if (amountDetail.Kind == null || !TYPES.Contains(amountDetail.Kind))
            {
                errors.Add("kind");
            }

            if (amountDetail.Amount == 0)
            {
                errors.Add("amount");
            }

            if (errors?.Any() ?? false)
            {
                fields = errors;
                ThrowValidationException(errors, "AmountDetail", silent);

                return false;
            }

            fields = null;

            return true;
        }
    }
}
