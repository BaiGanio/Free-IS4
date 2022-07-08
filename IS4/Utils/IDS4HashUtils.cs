using System;
using System.Security.Cryptography;

namespace IS4
{
    public static class IDS4HashUtils
    {
        public static bool VerifyPassword(string plainPassword, string passwordHash)
        {
            /* Fetch the stored value & Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(passwordHash);

            /* Get the salt */
            byte[] salt = new byte[16];

            Array.Copy(hashBytes, 0, salt, 0, 16);

            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(plainPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            /* Compare the results */
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
