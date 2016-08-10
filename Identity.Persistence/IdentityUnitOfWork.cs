using System;
using System.Threading.Tasks;
using Identity.Domain;
using Identity.Domain.Entities;
using Identity.Persistence.EF;
using Identity.Persistence.EF.Context;
using LxUtilities.Definitions.Caching;
using LxUtilities.Definitions.Mapping;
using LxUtilities.Services.Persistence.Ef;

namespace Identity.Persistence
{
    public class IdentityUnitOfWork : UnitOfWorkWithEfAndCache<IdentityDbContext>
    {
        protected readonly IdentityDataStore DataStore;

        public IdentityUnitOfWork(Func<IdentityDbContext> contextFactory, Func<ICacheWithTransactions> cacheFactory,
            IMappingService mappingService)
            : base(contextFactory, cacheFactory, mappingService)
        {
            DataStore = new IdentityDataStore(Context, MappingService);
        }

        public User GetUserByUsername(string username, bool bypassCache = false)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Argument username can not be null or empty", nameof(username));

            User user;
            if (bypassCache)
            {
                user = DataStore.SingleOrDefault<User>(x => x.Entity.Username == username);
                return user;
            }

            var cacheKey = CacheKeyHelper.GetCacheKey<User>(username);
            var userKey = Cache.GetCachedItem<Guid>(cacheKey);
            if (userKey != Guid.Empty)
                return GetUser(userKey);

            user = DataStore.SingleOrDefault<User>(x => x.Entity.Username == username);
            Cache.SetCachedItemAsync(cacheKey, user);
            return user;
        }

        public User GetUserByEmail(string email, bool bypassCache = false)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Argument email can not be null or empty", nameof(email));

            User user;
            if (bypassCache)
            {
                user = DataStore.SingleOrDefault<User>(x => x.Entity.Email == email);
                return user;
            }

            var cacheKey = CacheKeyHelper.GetCacheKey<User>(email);
            var userKey = Cache.GetCachedItem<Guid>(cacheKey);
            if (userKey != Guid.Empty)
                return GetUser(userKey);

            user = DataStore.SingleOrDefault<User>(x => x.Entity.Email == email);
            Cache.SetCachedItemAsync(cacheKey, user);
            return user;
        }

        public User GetUser(Guid userKey, bool bypassCache = false)
        {
            User user;
            var cacheKey = CacheKeyHelper.GetCacheKey<User>(userKey);
            if (!bypassCache)
            {
                user = Cache.GetCachedItem<User>(cacheKey);
                if (user != null)
                    return user;
            }

            user = DataStore.SingleOrDefault<User>(x => x.Entity.Key == userKey);

            if (!bypassCache)
            {
                Cache.SetCachedItemAsync(cacheKey, user);
            }

            return user;
        }

        public void SetUser(User user, bool bypassCache = false)
        {
            var savedUser = DataStore.AddOrUpdateByKey(user);

            if (bypassCache)
                return;

            //Cache.ExecuteTransaction(async x => await SetUserCachedItems(x, savedUser));

            Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(savedUser.Email), savedUser.Key).Wait();
            Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(savedUser.Username), savedUser.Key).Wait();
            Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(savedUser.Mobile), savedUser.Key).Wait();

            var cacheKey = CacheKeyHelper.GetCacheKey<User>(savedUser.Key);
            Cache.SetCachedItemAsync(cacheKey, savedUser).Wait();
            var u = Cache.GetCachedItem<User>(cacheKey);
        }

        private static async Task SetUserCachedItems(ICache x, User savedUser)
        {
            await x.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(savedUser.Email), savedUser.Key);
            await x.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(savedUser.Username), savedUser.Key);
            await x.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(savedUser.Mobile), savedUser.Key);
            await x.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(savedUser.Key), savedUser);
        }
    }
}