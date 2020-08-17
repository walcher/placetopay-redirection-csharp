namespace PlacetoPay.Redirection.Exceptions
{
    /// <summary>
    /// Class <c>ValidationFailException</c>
    /// </summary>
    public class ValidationFailException : BaseException
    {
        /// <summary>
        /// ValidationFailException constructor.
        /// </summary>
        /// <param name="message">string</param>
        public ValidationFailException(string message) : base(message) { }
    }
}
