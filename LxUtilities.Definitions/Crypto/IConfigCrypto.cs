namespace LxUtilities.Definitions.Crypto
{
    /// <summary>
    ///     For crypto config for PBKDF2 algorithm
    /// </summary>
    public interface IConfigCrypto
    {
        /// <summary>
        ///     Salt size in bytes
        /// </summary>
        int SaltByteSize { get; }

        /// <summary>
        ///     Hash size in bytes
        /// </summary>
        int HashByteSize { get; }

        /// <summary>
        ///     Index of the interation section
        /// </summary>
        int IterationIndex { get; }

        /// <summary>
        ///     Index of the salt section
        /// </summary>
        int SaltIndex { get; }

        /// <summary>
        ///     Index of the PBKDF2 hash
        /// </summary>
        int Pbkdf2Index { get; }

        /// <summary>
        ///     Iteration number of the hashing processes
        /// </summary>
        int Pbkdf2Iterations { get; }
    }
}