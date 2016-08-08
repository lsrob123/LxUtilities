namespace LxUtilities.Definitions.Caching
{
    public static class CacheKeyHelper
    {
        public static string GetCacheKey<T>(object suffix, bool enforceLowercaseToSuffix = true)
        {
            if (suffix == null)
                return $"{typeof (T).FullName}";

            var suffixText = $"{suffix}".Trim();
            if (enforceLowercaseToSuffix)
                suffixText = suffixText.ToLower();

            return $"{typeof (T).FullName}_{suffixText}";
        }
    }
}