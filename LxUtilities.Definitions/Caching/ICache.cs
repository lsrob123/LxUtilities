using System;

namespace LxUtilities.Definitions.Caching
{
    public interface ICache
    {
        bool Exists(string cacheKey);
        bool RemoveCachedItem(string cacheKey);
        T GetCachedItem<T>(string cacheKey);
        bool SetCachedItem<T>(string cacheKey, T cachedItem, TimeSpan expiration) where T : class;
    }
}