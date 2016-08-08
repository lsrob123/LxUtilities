using System;
using System.Configuration;
using System.Threading.Tasks;
using LxUtilities.Definitions.Caching;
using LxUtilities.Definitions.Serialization;
using LxUtilities.Services.Caching.Redis.Config;
using StackExchange.Redis;

namespace LxUtilities.Services.Caching.Redis
{
    public class Cache : CacheDatabase, ICacheWithTransactions
    {
        public Cache(ISerializer serializer, ICacheConfig configuration = null) : base(
            () =>
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
                return configuration;
            },
            config =>
            {
                var options = new ConfigurationOptions
                {
                    Ssl = config.Ssl,
                    AllowAdmin = config.AllowAdmin,
                    Password = config.Password,
                    AbortOnConnectFail = false
                };
                foreach (CacheHost redisHost in config.RedisHosts)
                {
                    options.EndPoints.Add(redisHost.Host, redisHost.CachePort);
                }

                var connectionMultiplexer = ConnectionMultiplexer.Connect(options);
                return connectionMultiplexer;
            },
            (config, connectionMultiplexer) =>
            {
                var database = connectionMultiplexer.GetDatabase(config.Database);
                return database;
            }, serializer)
        {
        }

        public Cache(ISerializer serializer, string connectionString, int database = 0) : base(null,
            config => ConnectionMultiplexer.Connect(connectionString),
            (config, connectionMultiplexer) => connectionMultiplexer.GetDatabase(database), serializer)
        {
        }

        public bool ExecuteTransaction(Func<ICacheWithHashes, Task> transactedOperations)
        {
            if (transactedOperations == null)
                return false;

            var transaction = Database.CreateTransaction();
            var transactedDatabase = new CacheDatabase(transaction as IDatabase, Serializer);

            transactedOperations(transactedDatabase);

            var committed = transaction.Execute();
            return committed;
        }

    }
}