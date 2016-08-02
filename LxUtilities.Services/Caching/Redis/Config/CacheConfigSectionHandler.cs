using System.Configuration;

namespace LxUtilities.Services.Caching.Redis.Config
{
    public class CacheConfigSectionHandler : ConfigurationSection, ICacheConfig
    {
        [ConfigurationProperty("hosts")]
        public CacheHostCollection RedisHosts => this["hosts"] as CacheHostCollection;

        [ConfigurationProperty("allowAdmin")]
        public bool AllowAdmin
        {
            get
            {
                var config = this["allowAdmin"];

                var value = config?.ToString();

                if (string.IsNullOrEmpty(value))
                    return false;

                bool result;
                return bool.TryParse(value, out result) ? result : result;
            }
        }

        [ConfigurationProperty("ssl")]
        public bool Ssl
        {
            get
            {
                var config = this["ssl"];
                var value = config?.ToString();

                if (string.IsNullOrWhiteSpace(value))
                    return false;

                bool result;
                return bool.TryParse(value, out result) ? result : result;
            }
        }

        [ConfigurationProperty("connectTimeout")]
        public int ConnectTimeout
        {
            get
            {
                var config = this["connectTimeout"];
                var value = config?.ToString();

                if (string.IsNullOrWhiteSpace(value))
                    return 5000;

                int result;
                return int.TryParse(value, out result) ? result : 5000;
            }
        }

        [ConfigurationProperty("database")]
        public int Database
        {
            get
            {
                var config = this["database"];
                var value = config?.ToString();

                if (string.IsNullOrWhiteSpace(value))
                    return 0;

                int result;
                return int.TryParse(value, out result) ? result : 0;
            }
        }

        [ConfigurationProperty("password", IsRequired = false)]
        public string Password => this["password"] as string;

        public static CacheConfigSectionHandler GetConfig()
        {
            return ConfigurationManager.GetSection("redisCacheClient") as CacheConfigSectionHandler;
        }
    }
}