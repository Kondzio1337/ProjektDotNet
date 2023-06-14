using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Projekt.Logic
{
    public class HashHaslo
    {
        public static string HashPassword(string password)
        {
            // Wygenerowanie soli (randomowego ciągu znaków) dla hasła
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Wygenerowanie skrótu hasła
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            string gowno = Convert.ToBase64String(salt);

            // Dołączenie pełnej soli do zahaszowanego hasła
            return $"{hashed}:{gowno}";
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Podział zahaszowanego hasła na skrót i sól
            string[] parts = hashedPassword.Split(':');
            if (parts.Length != 2)
            {
                // Nieprawidłowy format zahaszowanego hasła
                return false;
            }

            string hashed = parts[0];
            string saltString = parts[1];

            if (string.IsNullOrEmpty(hashed) || string.IsNullOrEmpty(saltString))
            {
                // Nieprawidłowy format zahaszowanego hasła (brak skrótu lub soli)
                return false;
            }

            byte[] salt = Convert.FromBase64String(saltString);

            // Weryfikacja hasła
            string computedHashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed == computedHashed;
        }
    }
}
