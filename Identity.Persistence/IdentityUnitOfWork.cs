﻿using System;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Identity.Persistence.EF;
using Identity.Persistence.EF.Context;
using LxUtilities.Definitions.Caching;
using LxUtilities.Definitions.Mapping;
using LxUtilities.Services.Persistence;
using LxUtilities.Services.Persistence.EF;

namespace Identity.Persistence
{
    public class IdentityDbContextUnitOfWork : DbContextUnitOfWork<IdentityDbContext>
    {
        protected readonly IdentityDataStore DataStore;

        public IdentityDbContextUnitOfWork(Func<IdentityDbContext> contextFactory, Func<ICacheWithTransactions> cacheFactory,
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
                user = DataStore.SingleOrDefault<User>(x => x.Username == username);
                return user;
            }

            var cacheKey = CacheKeyHelper.GetCacheKey<User>(username);
            var userKey = Cache.GetCachedItem<Guid>(cacheKey);
            if (userKey != Guid.Empty)
                return GetUser(userKey);

            user = DataStore.SingleOrDefault<User>(x => x.Username == username);
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
                user = DataStore.SingleOrDefault<User>(x => x.Email == email);
                return user;
            }

            var cacheKey = CacheKeyHelper.GetCacheKey<User>(email);
            var userKey = Cache.GetCachedItem<Guid>(cacheKey);
            if (userKey != Guid.Empty)
                return GetUser(userKey);

            user = DataStore.SingleOrDefault<User>(x => x.Email == email);
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

            user = DataStore.SingleOrDefault<User>(x => x.Key == userKey);

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

            var cacheKey = CacheKeyHelper.GetCacheKey<User>(savedUser.Key);
            Task.WaitAll(Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(savedUser.Email), savedUser.Key),
                Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(savedUser.Username), savedUser.Key),
                Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(savedUser.Mobile), savedUser.Key),
                Cache.SetCachedItemAsync(cacheKey, savedUser));
        }

        //private static async Task SetUserCachedItems(ICache x, User savedUser)
        //{
        //    await x.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(savedUser.Email), savedUser.Key);
        //    await x.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(savedUser.Username), savedUser.Key);
        //    await x.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(savedUser.Mobile), savedUser.Key);
        //    await x.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(savedUser.Key), savedUser);
        //}
    }
}