using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using LxUtilities.Definitions.Crypto;

namespace LxUtilities.Services.Crypto
{
    /// <summary>
    ///     Default implementation of ICryptoService with PBKDF2 algorithm
    /// </summary>
    public class CryptoService : ICryptoService
    {
        private readonly IConfigCrypto _config;

        /// <summary>
        ///     Using the default settings with iteration count set to 10,000 from ConfigCryptoDefault
        /// </summary>
        public CryptoService() : this(new DefaultConfigCrypto())
        {
        }

        /// <summary>
        ///     Using the default settings with iteration count set to 10,000 from ConfigCryptoDefault
        /// </summary>
        /// <param name="config"></param>
        public CryptoService(IConfigCrypto config)
        {
            _config = config;
        }

        /// <summary>
        ///     Creates a salted PBKDF2 hash of the source string.
        /// </summary>
        /// <param name="source">The source string to hash.</param>
        /// <returns>The hash of the source string.</returns>
        public string CreateHash(string source)
        {
            // Generate a random salt
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[_config.SaltByteSize];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            var hash = Pbkdf2(source, salt, _config.Pbkdf2Iterations, _config.HashByteSize);
            return _config.Pbkdf2Iterations + ":" +
                   Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash);
        }

        /// <summary>
        ///     Validates a source string given a hash of the correct one.
        /// </summary>
        /// <param name="source">The source string to check.</param>
        /// <param name="correctHash">A hash of the correct source string.</param>
        /// <returns>True if the source string is correct. False otherwise.</returns>
        public bool Validate(string source, string correctHash)
        {
            // Extract the parameters from the hash
            char[] delimiter = {':'};
            var split = correctHash.Split(delimiter);
            var iterations = int.Parse(split[_config.IterationIndex]);
            var salt = Convert.FromBase64String(split[_config.SaltIndex]);
            var hash = Convert.FromBase64String(split[_config.Pbkdf2Index]);

            var testHash = Pbkdf2(source, salt, iterations, hash.Length);
            var isValid = SlowEquals(hash, testHash);
            return isValid;
        }

        /// <summary>
        ///     Compares two byte arrays in length-constant time. This comparison
        ///     method is used so that password hashes cannot be extracted from
        ///     on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="a">The first byte array.</param>
        /// <param name="b">The second byte array.</param>
        /// <returns>True if both byte arrays are equal. False otherwise.</returns>
        private static bool SlowEquals(IReadOnlyList<byte> a, IReadOnlyList<byte> b)
        {
            var diff = (uint) a.Count ^ (uint) b.Count;
            for (var i = 0; i < a.Count && i < b.Count; i++)
                diff |= (uint) (a[i] ^ b[i]);
            return diff == 0;
        }

        /// <summary>
        ///     Computes the PBKDF2-SHA1 hash of a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The PBKDF2 iteration count.</param>
        /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
        /// <returns>A hash of the password.</returns>
        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) {IterationCount = iterations};
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}