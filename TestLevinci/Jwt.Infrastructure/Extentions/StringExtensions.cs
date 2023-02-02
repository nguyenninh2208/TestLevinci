using System;
using System.Security.Cryptography;
using System.Text;

namespace Jwt.Infrastructure.Extentions
{
    public static class StringExtensions
    {
        public static string ToMD5(this string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] encodedPassword = new UTF8Encoding().GetBytes(input);

                // need MD5 to calculate the hash
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

                // string representation (similar to UNIX format)
                string encoded = BitConverter.ToString(hash)
                   // without dashes
                   .Replace("-", string.Empty)
                   // make lowercase
                   .ToLower();
                return encoded;
            }
        }
    }
}
