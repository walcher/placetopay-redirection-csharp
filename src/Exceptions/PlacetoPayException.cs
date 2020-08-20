using System;
using System.Diagnostics;

namespace PlacetoPay.Redirection.Exceptions
{
    /// <summary>
    /// Class <c>PlacetoPayException</c>
    /// </summary>
    public class PlacetoPayException : Exception
    {
        /// <summary>
        /// Get formatted exception.
        /// </summary>
        /// <param name="ex">exception</param>
        /// <returns>string</returns>
        public static string ReadException(Exception ex)
        {
            var st = new StackTrace(ex, true);
            var frame = st.GetFrame(0);

            return $"{ex.Message} ON {frame.GetFileName()} LINE {frame.GetFileLineNumber()} [ {ex.TargetSite.ReflectedType.Name} ]";
        }
    }
}
