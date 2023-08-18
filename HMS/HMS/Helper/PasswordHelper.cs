using System.Security.Cryptography;
using System.Text;

namespace HMS.Helper
{
    public static  class PasswordHelper
    {
        const int keySize = 64;
        const int iterations = 350000;
        //HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        public static string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                 HashAlgorithmName.SHA512,
                keySize);
            return Convert.ToHexString(hash);
        }
        public static bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA512, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
