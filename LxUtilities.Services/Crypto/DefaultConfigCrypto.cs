using LxUtilities.Definitions.Crypto;

namespace LxUtilities.Services.Crypto
{
    /// <summary>
    ///     Default config for crypto with an iteration count value of 10,000
    /// </summary>
    public class DefaultConfigCrypto : IConfigCrypto
    {
        /// <summary>
        ///     Default config for crypto with an iteration count value of 10,000
        /// </summary>
        public DefaultConfigCrypto() : this(10000)
        {
        }

        /// <summary>
        ///     Default config with custom interation count
        /// </summary>
        /// <param name="pbkdf2Iterations">Iteration count</param>
        public DefaultConfigCrypto(int pbkdf2Iterations)
        {
            SaltByteSize = 24;
            HashByteSize = 24;
            IterationIndex = 0;
            SaltIndex = 1;
            Pbkdf2Index = 2;
            Pbkdf2Iterations = pbkdf2Iterations;
        }

        /// <summary>
        ///     Salt size in bytes
        /// </summary>
        public int SaltByteSize { get; }

        /// <summary>
        ///     Hash size in bytes
        /// </summary>
        public int HashByteSize { get; }

        /// <summary>
        ///     Index of the interation section
        /// </summary>
        public int IterationIndex { get; }

        /// <summary>
        ///     Index of the salt section
        /// </summary>
        public int SaltIndex { get; }

        /// <summary>
        ///     Index of the PBKDF2 hash
        /// </summary>
        public int Pbkdf2Index { get; }

        /// <summary>
        ///     Iteration number of the hashing processes
        /// </summary>
        public int Pbkdf2Iterations { get; }
    }
}