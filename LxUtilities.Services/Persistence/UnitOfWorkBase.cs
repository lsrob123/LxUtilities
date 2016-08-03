using System;
using System.Data.Entity;
using LxUtilities.Definitions.Caching;
using LxUtilities.Definitions.Persistence;

namespace LxUtilities.Services.Persistence
{
    public abstract class UnitOfWorkBase<TDbContext> : IUnitOfWork
        where TDbContext:DbContext
    {
        protected TDbContext Context;
        protected readonly ICache Cache;

        protected UnitOfWorkBase(Func<TDbContext> dbContextFactory, ICache cache)
        {
            Context = dbContextFactory();
            Cache = cache;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}