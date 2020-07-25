using System.Security.Cryptography;
using System.Text;

namespace PlacetoPay.Redirection.Helpers
{
    /// <summary>
    /// Class <c>CryptoHelper</c>
    /// </summary>
    public class CryptoHelper
    {
        /// <summary>
        /// Generate sh1 string from data.
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>string</returns>
        public static string MakeSHA1(string data)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] dataToByte = Encoding.Default.GetBytes(data);
            byte[] hash = sha1.ComputeHash(dataToByte);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte i in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", i);
            }

            return stringBuilder.ToString();
        }
    }
}
