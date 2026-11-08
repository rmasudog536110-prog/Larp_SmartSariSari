using System;
using System.Security.Cryptography;

namespace Larp.infrastructure.Services
{
    /// <summary>
    /// PBKDF2-based password hashing. No external NuGet package required
    /// (uses System.Security.Cryptography, built into .NET).
    /// Store both Hash and Salt per user — never store or compare plain passwords.
    /// </summary>
    public static class PasswordHasher
    {
        private const int SaltSize = 16;      // 128-bit salt
        private const int HashSize = 32;      // 256-bit hash
        private const int Iterations = 100_000;

        public static (byte[] Hash, byte[] Salt) HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Derive(password, salt);
            return (hash, salt);
        }

        public static bool Verify(string password, byte[] storedHash, byte[] storedSalt)
        {
            byte[] attemptedHash = Derive(password, storedSalt);
            return CryptographicOperations.FixedTimeEquals(attemptedHash, storedHash);
        }

        private static byte[] Derive(string password, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password, salt, Iterations, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(HashSize);
        }
    }
}