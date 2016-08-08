using System;
using Identity.Domain;
using Identity.Persistence.EF;
using Identity.Persistence.EF.Context;
using LxUtilities.Definitions.Caching;
using LxUtilities.Definitions.Mapping;
using LxUtilities.Services.Persistence.Ef;

namespace Identity.Persistence
{
    public class IdentityUnitOfWork : UnitOfWorkWithEfAndCache<IdentityDbContext>
    {
        protected readonly IdentityRepository Repository;

        public IdentityUnitOfWork(Func<IdentityDbContext> contextFactory, ICacheWithTransactions cache,
            IMappingService mappingService)
            : base(contextFactory, cache, mappingService)
        {
            Repository = new IdentityRepository(Context, MappingService);
        }

        public User GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Argument username can not be null or empty", nameof(username));

            var cacheKey = CacheKeyHelper.GetCacheKey<User>(username);
            var userKey = Cache.GetCachedItem<Guid>(cacheKey);
            if (userKey != Guid.Empty)
                return GetUser(userKey);

            var user = Repository.SingleOrDefault<User>(x => x.Entity.Username == username);
            Cache.SetCachedItem(cacheKey, user);
            return user;
        }

        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Argument email can not be null or empty", nameof(email));

            var cacheKey = CacheKeyHelper.GetCacheKey<User>(email);
            var userKey = Cache.GetCachedItem<Guid>(cacheKey);
            if (userKey != Guid.Empty)
                return GetUser(userKey);

            var user = Repository.SingleOrDefault<User>(x => x.Entity.Email == email);
            Cache.SetCachedItem(cacheKey, user);
            return user;
        }

        public User GetUser(Guid userKey)
        {
            var cacheKey = CacheKeyHelper.GetCacheKey<User>(userKey);
            var user = Cache.GetCachedItem<User>(cacheKey);
            if (user != null)
                return user;

            user = Repository.SingleOrDefault<User>(x => x.Entity.Key == userKey);
            Cache.SetCachedItem(cacheKey, user);
            return user;
        }

        public void SetUser(User user)
        {
            var savedUser = Repository.AddOrUpdateByKey(user);

            Cache.ExecuteTransaction(x =>
            {
                x.SetCachedItem(CacheKeyHelper.GetCacheKey<User>(savedUser.Email), savedUser.Key);
                x.SetCachedItem(CacheKeyHelper.GetCacheKey<User>(savedUser.Username), savedUser.Key);
                x.SetCachedItem(CacheKeyHelper.GetCacheKey<User>(savedUser.Mobile), savedUser.Key);
                x.SetCachedItem(CacheKeyHelper.GetCacheKey<User>(savedUser.Key), savedUser);
            });
        }
    }
}