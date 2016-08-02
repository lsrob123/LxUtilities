using System;
using System.Configuration;

namespace LxUtilities.Services.Caching.Redis.Config
{
    public class CacheHost : ConfigurationElement
    {
        [ConfigurationProperty("host", IsRequired = true)]
        public string Host => this["host"] as string;

        [ConfigurationProperty("cachePort", IsRequired = true)]
        public int CachePort
        {
            get
            {
                var config = this["cachePort"];
                if (config == null)
                    throw new Exception("Redis Cahe port must be number.");

                var value = config.ToString();

                if (string.IsNullOrEmpty(value))
                    throw new Exception("Redis Cahe port must be number.");

                int result;

                if (int.TryParse(value, out result))
                    return result;

                throw new Exception("Redis Cahe port must be number.");
            }
        }
    }
}