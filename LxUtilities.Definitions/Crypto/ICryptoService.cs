namespace LxUtilities.Definitions.Crypto
{
    /// <summary>
    ///     For crypto service implementations
    /// </summary>
    public interface ICryptoService
    {
        /// <summary>
        ///     Creates a salted hash of the source string.
        /// </summary>
        /// <param name="source">The source string to hash.</param>
        /// <returns>The hash of the source string.</returns>
        string CreateHash(string source);

        /// <summary>
        ///     Validates a source string given a hash of the correct one.
        /// </summary>
        /// <param name="source">The source string to check.</param>
        /// <param name="correctHash">A hash of the correct source string.</param>
        /// <returns>True if the source string is correct. False otherwise.</returns>
        bool Validate(string source, string correctHash);
    }
}