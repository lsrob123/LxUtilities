using System;

namespace LxUtilities.Contracts.Caching
{
    public interface IGenericCache
    {
        bool Exists(string cacheKey);
        bool RemoveCachedItem(string cacheKey);
        T GetCachedItem<T>(string cacheKey);
        bool SetCachedItem<T>(string cacheKey, T cachedItem, TimeSpan expiration ) where T : class;
    }
}