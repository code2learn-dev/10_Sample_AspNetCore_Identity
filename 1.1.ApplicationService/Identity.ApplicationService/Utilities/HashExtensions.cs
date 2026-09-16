using System.Security.Cryptography;
using System.Text;

namespace Identity.ApplicationService.Utilities
{
    public static class HashExtensions
    {
        public static string ConvertToHash(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            var hashAlgorithm = SHA3_256.Create();
            byte[] hashBytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hashBytes);
        }
    }
}
