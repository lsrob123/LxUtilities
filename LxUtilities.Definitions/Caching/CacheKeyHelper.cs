namespace LxUtilities.Definitions.Caching
{
    public static class CacheKeyHelper
    {
        public static string GetCacheKey<T>(object suffix, bool enforceLowercaseToSuffix = true)
        {
            var cacheKey = $"{typeof(T).FullName}";
            if (suffix != null)
            {
                var suffixText = $"{suffix}".Trim();
                cacheKey = $"{cacheKey}_{suffixText}";
            }

            if (enforceLowercaseToSuffix)
                cacheKey = cacheKey.ToLower();

            return cacheKey;
        }
    }
}