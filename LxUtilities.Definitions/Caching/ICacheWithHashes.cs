using System.Collections.Generic;

namespace LxUtilities.Definitions.Caching
{
    public interface ICacheWithHashes : ICache
    {
        void HashSet(string hashKey, IDictionary<string, string> nameValues);
        ICollection<string> HashGet(string hashKey, params string[] names);
    }
}