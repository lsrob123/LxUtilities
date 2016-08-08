using System;
using System.Threading.Tasks;

namespace LxUtilities.Definitions.Caching
{
    public interface ICache : IDisposable
    {
        bool Exists(string cacheKey);
        Task<bool> RemoveCachedItemAsync(string cacheKey);
        T GetCachedItem<T>(string cacheKey);
        Task<bool> SetCachedItemAsync<T>(string cacheKey, T cachedItem);
        Task<bool> SetCachedItemAsync<T>(string cacheKey, T cachedItem, TimeSpan expiration);
    }
}