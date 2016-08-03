using System;
using LxUtilities.Definitions.Persistence;
using LxUtilities.Definitions.Caching;
using System.Data.Entity;

namespace LxUtilities.Services.Persistence.Ef
{
    public abstract class UnitOfWorkBase<TDbContext> : IUnitOfWork
        where TDbContext: DbContext, new()
    {
        protected TDbContext DbContext;
        protected readonly ICache Cache;

        public UnitOfWorkBase(string connectionString, ICache cache)
        {
            DbContext = new TDbContext(connectionString);
            ConnectionString = connectionString;
            Cache = cache;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}