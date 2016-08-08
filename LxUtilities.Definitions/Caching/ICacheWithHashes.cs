using System.Collections.Generic;
using System.Threading.Tasks;

namespace LxUtilities.Definitions.Caching
{
    public interface ICacheWithHashes : ICache
    {
        Task HashSetAsync(string hashKey, IDictionary<string, string> nameValues);
        ICollection<string> HashGet(string hashKey, params string[] names);
    }
}