using LxUtilities.Definitions.Crypto;

namespace LxUtilities.Services.Crypto
{
    /// <summary>
    ///     Factory for spinning up new instance of CryptoService
    /// </summary>
    public static class DefaultCryptoFactory
    {
        /// <summary>
        ///     Creates new CryptoService instance with a DefaultConfigCrypto
        /// </summary>
        /// <returns></returns>
        public static CryptoService Create()
        {
            return Create(new DefaultConfigCrypto());
        }

        /// <summary>
        ///     Creates new CryptoService instance with custom config which implements IConfigCrypto
        /// </summary>
        /// <param name="config">Config instance of a type which implements IConfigCrypto</param>
        /// <returns></returns>
        public static CryptoService Create(IConfigCrypto config)
        {
            return new CryptoService(config);
        }
    }
}