using System.Collections.Generic;

namespace PlacetoPay.Redirection.Exceptions
{
    /// <summary>
    /// Class <c>EntityValidationFailException</c>
    /// </summary>
    public class EntityValidationFailException : ValidationFailException
    {
        protected List<string> fields;
        protected string from;

        /// <summary>
        /// EntityValidationFailException constructor.
        /// </summary>
        /// <param name="fields">List</param>
        /// <param name="from">string</param>
        /// <param name="message">string</param>
        public EntityValidationFailException(List<string> fields, string from = null, string message = null)
            : base(ModifyMessage(fields, from, message))
        {
            this.fields = fields;
            this.from = from;
        }

        /// <summary>
        /// Fields property.
        /// </summary>
        public List<string> Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        /// <summary>
        /// From property.
        /// </summary>
        public string From
        {
            get { return from; }
            set { from = value; }
        }

        /// <summary>
        /// Get current message.
        /// </summary>
        /// <param name="fields">List</param>
        /// <param name="from">string</param>
        /// <param name="message">string</param>
        /// <returns>string</returns>
        public static string ModifyMessage(List<string> fields, string from = null, string message = null)
        {
            if (message == null)
            {
                message = $"Validation fail on entity {from} values ({string.Join(", ", fields.ToArray())})";
            }

            return message;
        }
    }
}
