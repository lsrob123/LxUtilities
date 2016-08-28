using System;
using System.Data.Entity;
using LxUtilities.Definitions.Caching;
using LxUtilities.Definitions.Mapping;

namespace LxUtilities.Services.Persistence.EF
{
    public class DbContextUnitOfWork<TDbContext> : UnitOfWorkBase
        where TDbContext : DbContext
    {
        protected readonly ICacheWithTransactions Cache;
        protected readonly TDbContext Context;
        protected readonly IMappingService MappingService;

        public DbContextUnitOfWork(Func<TDbContext> contextFactory, Func<ICacheWithTransactions> cacheFactory,
            IMappingService mappingService)
        {
            Cache = cacheFactory();
            Context = contextFactory();
            MappingService = mappingService;
        }

        protected override void DisposingAction()
        {
            Context.Dispose();
            Cache.Dispose();
        }

        public override void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}