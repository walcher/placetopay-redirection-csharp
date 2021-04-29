using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PlacetoPay.Redirection.Helpers
{
    public class DocumentHelper
    {
        public const string TYPE_CC = "CC";
        public const string TYPE_CE = "CE";
        public const string TYPE_TI = "TI";
        public const string TYPE_RC = "RC";
        public const string TYPE_NIT = "NIT";
        public const string TYPE_RUT = "RUT";
        public const string TYPE_PPN = "PPN";
        public const string TYPE_TAX = "TAX";
        public const string TYPE_LIC = "LIC";
        public const string TYPE_CD = "CD";
        public const string TYPE_SSN = "SSN";
        public const string TYPE_CIP = "CIP";
        public const string TYPE_CPF = "CPF";
        public const string TYPE_CI = "CI";
        public const string TYPE_RUC = "RUC";

        protected static string[] DOCUMENT_TYPES = {
            TYPE_CC,
            TYPE_CE,
            TYPE_TI,
            TYPE_NIT,
            TYPE_RUT,
            TYPE_PPN,
            TYPE_TAX,
            TYPE_LIC,
            TYPE_SSN,
            TYPE_CIP,
            TYPE_CPF,
            TYPE_CI,
            TYPE_RUC,
        };

        public static Dictionary<string, string> VALIDATION_PATTERNS = new Dictionary<string, string>
        {
            { TYPE_CC, @"^[1-9][0-9]{3,9}$" },
            { TYPE_CE, @"^([a-zA-Z]{1,5})?[1-9][0-9]{3,7}$" },
            { TYPE_TI, @"^[1-9][0-9]{4,11}$" },
            { TYPE_NIT, @"^[1-9]\d{6,9}$" },
            { TYPE_RUT, @"^[1-9]\d{6,9}$" },
            { TYPE_PPN, @"^[a-zA-Z0-9_]{4,16}$" },
            { TYPE_TAX, @"^[a-zA-Z0-9_]{4,16}$" },
            { TYPE_LIC, @"^[a-zA-Z0-9_]{4,16}$" },
            { TYPE_SSN, @"^\d{3}\d{2,3}\d{4}$" },
            { TYPE_CIP, @"^(PE|N|E|\d+)?\d{2,6}\d{2,6}$" },
            { TYPE_CPF, @"^\d{10,11}$" },
            { TYPE_CI, @"^\d{10}$" },
            { TYPE_RUC, @"^\d{13}$" },
        };

        /// <summary>
        /// Get document types.
        /// </summary>
        /// <param name="exclude">array</param>
        /// <returns>array</returns>
        public static string[] DocumentTypes(string[] exclude = null)
        {
            string[] types = DOCUMENT_TYPES;

            if (exclude != null && exclude is string[])
            {
                types = types.Except(exclude).ToArray();
            }

            return types;
        }

        /// <summary>
        /// Check if document type is a valid one.
        /// </summary>
        /// <param name="type">string</param>
        /// <returns>bool</returns>
        public static bool IsValidType(string type)
        {
            return DOCUMENT_TYPES.Contains(type);
        }

        /// <summary>
        /// Check if document if a valid one.
        /// </summary>
        /// <param name="type">string</param>
        /// <param name="document">string</param>
        /// <returns>bool</returns>
        public static bool IsValidDocument(string type, string document)
        {
            if (!IsValidType(type))
            {
                return false;
            }

            string pattern = VALIDATION_PATTERNS[type] ?? null;

            if (pattern == null)
            {
                return false;
            }

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return document != null && regex.IsMatch(document);
        }

        /// <summary>
        /// Check if document is bussiness type.
        /// </summary>
        /// <param name="document">string</param>
        /// <returns>bool</returns>
        public static bool BusinessDocument(string document = null)
        {
            string[] businessDocuments = {
                TYPE_NIT,
                TYPE_RUT,
                TYPE_RUC,
            };

            if (document != null)
            {
                return businessDocuments.Contains(document);
            }

            return false;
        }
    }
}
