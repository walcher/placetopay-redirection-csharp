using PlacetoPay.Redirection.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlacetoPay.Redirection.Validators
{
    /// <summary>
    /// Class <c>SubscriptionValidator</c>
    /// </summary>
    public class SubscriptionValidator : BaseValidator
    {
        /// <summary>
        /// Validates if subscription entity contains the required information.
        /// </summary>
        /// <param name="entity">object</param>
        /// <param name="fields">list</param>
        /// <param name="silent">boold</param>
        /// <returns>bool</returns>
        public bool IsValid(object entity, out List<string> fields, bool silent)
        {
            List<string> errors = new List<string>();
            Subscription subscription = (Subscription)entity;

            if (subscription.Reference == null || MatchPattern(subscription.Reference, "/[ ]/"))
            {
                errors.Add("reference");
            }

            if (subscription.Description == null || !MatchPattern(subscription.Description, PATTERN_DESCRIPTION))
            {
                errors.Add("description");
            }

            if (errors?.Any() ?? false)
            {
                fields = errors;
                ThrowValidationException(errors, "Subscription", silent);

                return false;
            }

            fields = null;

            return true;
        }
    }
}
