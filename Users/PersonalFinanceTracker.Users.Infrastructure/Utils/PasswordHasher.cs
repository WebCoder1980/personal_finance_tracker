using PersonalFinanceTracker.Users.Application.Ports.Out;
using System.Security.Cryptography;

namespace PersonalFinanceTracker.Users.Infrastructure.Utils
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int ITERATIONS = 100_000;
        private const int SALT_SIZE = 32;

        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, ITERATIONS, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(SALT_SIZE);

            byte[] combined = [.. salt, .. hash];
            return Convert.ToBase64String(combined);
        }

        public bool Verify(string password, string storedHash)
        {
            byte[] data = Convert.FromBase64String(storedHash);
            byte[] salt = data[..SALT_SIZE];
            byte[] storedHashBytes = data[SALT_SIZE..];

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, ITERATIONS, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(SALT_SIZE);

            return CryptographicOperations.FixedTimeEquals(hash, storedHashBytes);
        }
    }
}
