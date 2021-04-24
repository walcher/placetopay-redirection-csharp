using System;

namespace PlacetoPay.Redirection.Exceptions
{
    /// <summary>
    /// Class <c>BaseException</c>
    /// </summary>
    public class BaseException : Exception
    {
        /// <summary>
        /// BaseException constructor.
        /// </summary>
        /// <param name="message">string</param>
        public BaseException(string message) : base(message) { }
    }
}
