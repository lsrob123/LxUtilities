using System;
using System.Data.Entity;
using LxUtilities.Definitions.Caching;
using LxUtilities.Definitions.Mapping;

namespace LxUtilities.Services.Persistence.Ef
{
    public class UnitOfWorkWithEfAndCache<TDbContext> : UnitOfWorkBase
        where TDbContext : DbContext
    {
        protected readonly ICacheWithTransactions Cache;
        protected readonly TDbContext Context;
        protected readonly IMappingService MappingService;

        public UnitOfWorkWithEfAndCache(Func<TDbContext> contextFactory, ICacheWithTransactions cache,
            IMappingService mappingService)
        {
            Cache = cache;
            MappingService = mappingService;
            Context = contextFactory();
        }

        protected override void DisposingAction()
        {
            Context.Dispose();
        }

        public override void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}