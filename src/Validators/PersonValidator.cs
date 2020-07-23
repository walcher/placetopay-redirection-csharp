using System;
using System.Collections.Generic;
using System.Text;

namespace PlacetoPay.Redirection.Validators
{
    /// <summary>
    /// Class <c>PersonValidator</c>
    /// </summary>
    public class PersonValidator
    {
        /// <summary>
        /// Normalize phone number.
        /// </summary>
        /// <param name="phone">string</param>
        /// <returns>string</returns>
        public static string NormalizePhone(string phone)
        {
            return phone.Replace("+57", "");
        }
    }
}
