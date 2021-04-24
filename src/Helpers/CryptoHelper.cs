using System;
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
        /// Generate hash string data.
        /// </summary>
        /// <param name="data">string</param>
        /// <param name="algorithm">string</param>
        /// <param name="raw">bool</param>
        /// <returns>object</returns>
        public static object ComputeHash(string data, string algorithm = "sha1", bool raw = false)
        {
            HashAlgorithm encode;

            if (algorithm == "sha1")
            {
                encode = new SHA1CryptoServiceProvider();
            }
            else
            {
                encode = new SHA256CryptoServiceProvider();
            }

            byte[] dataToByte = Encoding.UTF8.GetBytes(data);
            byte[] hash = encode.ComputeHash(dataToByte);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte i in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", i);
            }

            if (!raw)
            {
                return stringBuilder.ToString();
            }

            return hash;
        }

        /// <summary>
        /// Generate base64 string.
        /// </summary>
        /// <param name="data">object</param>
        /// <returns>string</returns>
        public static string MakeBase64(object data)
        {
            byte[] dataToByte;

            if (data.GetType() == typeof(string))
            {
                dataToByte = Encoding.UTF8.GetBytes((string)data);
            }
            else
            {
                dataToByte = (byte[])data;
            }

            return Convert.ToBase64String(dataToByte);
        }

        /// <summary>
        /// Generate ramdom string.
        /// </summary>
        /// <param name="size">int</param>
        /// <returns>string</returns>
        public static string MakeRandom(int size = 32)
        {
            const string src = "abcdefghijklmnopqrstuvwxyz0123456789";
            var stringBuilder = new StringBuilder();
            Random random = new Random();

            for (var i = 0; i < size; i++)
            {
                var c = src[random.Next(0, src.Length)];
                stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }
    }
}
