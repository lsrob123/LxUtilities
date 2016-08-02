using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using LxUtilities.Definitions.Caching;
using LxUtilities.Definitions.Logging;
using LxUtilities.Definitions.Serialization;
using LxUtilities.Services.Caching.Redis.Config;
using StackExchange.Redis;

namespace LxUtilities.Services.Caching.Redis
{
    public class Cache : IDisposable, ICacheWithHashes
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        protected readonly IDatabase Database;
        protected readonly ReaderWriterLockSlim Lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        protected readonly ILogger Logger;
        protected readonly ISerializer Serializer;

        public Cache(ILogger logger, ISerializer serializer, ICacheConfig configuration = null)
        {
            Logger = logger;


            try
            {
                if (configuration == null)
                {
                    configuration = CacheConfigSectionHandler.GetConfig();
                }

                if (configuration == null)
                {
                    throw new ConfigurationErrorsException(
                        "Unable to locate <redisCacheClient> section into your configuration file.");
                }

                var options = new ConfigurationOptions
                {
                    Ssl = configuration.Ssl,
                    AllowAdmin = configuration.AllowAdmin,
                    Password = configuration.Password,
                    AbortOnConnectFail = false
                };
                foreach (CacheHost redisHost in configuration.RedisHosts)
                {
                    options.EndPoints.Add(redisHost.Host, redisHost.CachePort);
                }

                _connectionMultiplexer = ConnectionMultiplexer.Connect(options);
                Database = _connectionMultiplexer.GetDatabase(configuration.Database);
                Serializer = serializer;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw;
            }
        }

        public Cache(ILogger logger, ISerializer serializer, string connectionString, int database = 0)
        {
            Logger = logger;

            try
            {
                Serializer = serializer;
                _connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
                Database = _connectionMultiplexer.GetDatabase(database);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw;
            }
        }

        public bool Exists(string cacheKey)
        {
            return Database.KeyExists(cacheKey);
        }

        public bool RemoveCachedItem(string cacheKey)
        {
            return Database.KeyDelete(cacheKey);
        }

        public T GetCachedItem<T>(string cacheKey)
        {
            var redisValue = Database.StringGet(cacheKey).ToString();
            return string.IsNullOrWhiteSpace(redisValue)
                ? default(T)
                : Serializer.Deserialize<T>(redisValue);
        }

        public bool SetCachedItem<T>(string cacheKey, T cachedItem, TimeSpan expiration) where T : class
        {
            var cachedString = Serializer.Serialize(cachedItem);
            return Database.StringSet(cacheKey, cachedString, expiration);
        }

        public void HashSet(string hashKey, IDictionary<string, string> nameValues)
        {
            Database.HashSet(hashKey, nameValues.Select(x => new HashEntry(x.Key, x.Value)).ToArray());
        }

        public ICollection<string> HashGet(string hashKey, params string[] names)
        {
            var hashValues = Database.HashGet(hashKey, names.Select(x => (RedisValue) x).ToArray());
            var results = hashValues.Select(x => x.ToString()).ToList();
            return results;
        }

        public void Dispose()
        {
            _connectionMultiplexer.Dispose();
        }

        public void RemoveAllCachedItems(ICollection<string> cacheKeys)
        {
            foreach (var cacheKey in cacheKeys)
            {
                RemoveCachedItem(cacheKey);
            }
        }

        public IDictionary<string, T> GetAllCachedItems<T>(ICollection<string> cacheKeys)
        {
            var redisKeys = cacheKeys.Select(x => (RedisKey) x).ToArray();
            var result = Database.StringGet(redisKeys);
            return redisKeys.ToDictionary(key => (string) key, key =>
            {
                {
                    var index = Array.IndexOf(redisKeys, key);
                    var value = result[index];
                    return value == RedisValue.Null ? default(T) : Serializer.Deserialize<T>(result[index]);
                }
            });
        }

        public IDictionary<string, T> GetAllCachedItems<T>()
        {
            var cacheKeys = GetKeys();
            var redisKeys = cacheKeys.Select(x => (RedisKey) x).ToArray();
            var result = Database.StringGet(redisKeys);
            return redisKeys.ToDictionary(key => (string) key, key =>
            {
                {
                    var index = Array.IndexOf(redisKeys, key);
                    var value = result[index];
                    return value == RedisValue.Null ? default(T) : Serializer.Deserialize<T>(result[index]);
                }
            });
        }

        public bool SetCachedItem<T>(string cacheKey, T cachedItem, DateTimeOffset expiresAt) where T : class
        {
            var expiration = expiresAt.Subtract(DateTimeOffset.Now);
            return SetCachedItem(cacheKey, cachedItem, expiration);
        }

        public bool SetCachedItem<T>(string cacheKey, T cachedItem) where T : class
        {
            var cachedString = Serializer.Serialize(cachedItem);
            return Database.StringSet(cacheKey, cachedString);
        }

        public bool SetAllCachedItems<T>(IList<Tuple<string, T>> cachedItems)
        {
            var redisKeyValueDictionary =
                cachedItems.ToDictionary<Tuple<string, T>, RedisKey, RedisValue>(item => item.Item1,
                    item => Serializer.Serialize(item.Item2));

            return Database.StringSet(redisKeyValueDictionary.ToArray());
        }

        public Dictionary<string, string> GetInfo()
        {
            var redisInfo = Database.ScriptEvaluate("return redis.call('INFO')").ToString();

            return ParseRedisInfo(redisInfo);
        }

        public ICollection<string> GetKeys()
        {
            var keys = (string[]) Database.ScriptEvaluate("return redis.call('KEYS','*')");

            return keys;
        }

        public void FlushDb()
        {
            var endPoints = Database.Multiplexer.GetEndPoints();

            foreach (var endpoint in endPoints)
            {
                Database.Multiplexer.GetServer(endpoint).FlushDatabase(Database.Database);
            }
        }

        public void SaveDb(SaveType saveType)
        {
            var endPoints = Database.Multiplexer.GetEndPoints();

            foreach (var endpoint in endPoints)
            {
                Database.Multiplexer.GetServer(endpoint).Save(saveType);
            }
        }

        private static Dictionary<string, string> ParseRedisInfo(string redisInfo)
        {
            var strArr = redisInfo.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            var dict = new Dictionary<string, string>();
            foreach (var str in strArr)
            {
                if (string.IsNullOrEmpty(str) || str[0] == '#')
                {
                    continue;
                }

                var idx = str.IndexOf(':');
                if (idx <= 0)
                    continue;

                var key = str.Substring(0, idx);
                var infoValue = str.Substring(idx + 1).Trim();
                dict.Add(key, infoValue);
            }
            return dict;
        }
    }
}