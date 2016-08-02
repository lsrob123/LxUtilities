namespace LxUtilities.Contracts.Serialization
{
    /// <summary>
    ///     For implementation of serializer
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="anyObject">Object to be serialized</param>
        /// <returns></returns>
        string Serialize(object anyObject);

        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        /// <param name="serialized">Pre-serialized string</param>
        /// <returns></returns>
        T Deserialize<T>(string serialized);
    }
}