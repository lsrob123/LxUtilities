namespace LxUtilities.Definitions.Serialization
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
        /// <param name="useFullContractResolver"></param>
        /// <returns></returns>
        string Serialize(object anyObject, bool useFullContractResolver = false);

        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        /// <param name="serialized">Pre-serialized string</param>
        /// <param name="useFullContractResolver"></param>
        /// <returns></returns>
        T Deserialize<T>(string serialized, bool useFullContractResolver = true);
    }
}