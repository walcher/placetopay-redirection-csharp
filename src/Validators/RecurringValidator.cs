using PlacetoPay.Redirection.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlacetoPay.Redirection.Validators
{
    /// <summary>
    /// Class <c>RecurringValidator</c>
    /// </summary>
    public class RecurringValidator : BaseValidator
    {
        public const string PERIOD_DAY = "D";
        public const string PERIOD_MONTH = "M";
        public const string PERIOD_YEAR = "Y";

        public static string[] PERIODS =
        {
            PERIOD_DAY,
            PERIOD_MONTH,
            PERIOD_YEAR,
        };

        /// <summary>
        /// Validates if recurring entity contains the required information.
        /// </summary>
        /// <param name="entity">object</param>
        /// <param name="fields">list</param>
        /// <param name="silent">boold</param>
        /// <returns>bool</returns>
        public bool IsValid(object entity, out List<string> fields, bool silent)
        {
            List<string> errors = new List<string>();
            Recurring recurring = (Recurring)entity;

            if (!PERIODS.Contains(recurring.Periodicity))
            {
                errors.Add("periodicity");
            }

            if (recurring.Interval == 0)
            {
                errors.Add("interval");
            }

            if (recurring.NextPayment == null || !IsActualDate(recurring.NextPayment))
            {
                errors.Add("nextPayment");
            }

            if (recurring.MaxPeriods == 0)
            {
                errors.Add("maxPeriods");
            }

            if (recurring.DueDate != null && !IsActualDate(recurring.DueDate))
            {
                errors.Add("dueDate");
            }

            if (errors?.Any() ?? false)
            {
                fields = errors;
                ThrowValidationException(errors, "Recurring", silent);

                return false;
            }

            fields = null;

            return true;
        }
    }
}
