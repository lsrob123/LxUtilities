using System;
using Identity.Persistence.EF.Context;
using LxUtilities.Definitions.Caching;
using LxUtilities.Definitions.Mapping;
using LxUtilities.Definitions.Persistence;
using LxUtilities.Services.Persistence;

namespace Identity.Persistence
{
    public class IdentityUnitOfWorkFactory : UnitOfWorkFactoryBase<IdentityDbContextUnitOfWork>
    {
        protected readonly Func<ICacheWithTransactions> CacheFactory;
        protected readonly string ConnectionString;
        protected readonly IMappingService MappingService;

        public IdentityUnitOfWorkFactory(string connectionString, Func<ICacheWithTransactions> cacheFactory, IMappingService mappingService)
        {
            ConnectionString = connectionString;
            CacheFactory = cacheFactory;
            MappingService = mappingService;
        }

        protected override IdentityDbContextUnitOfWork GetUnitOfWork()
        {
            return new IdentityDbContextUnitOfWork(() => new IdentityDbContext(ConnectionString), CacheFactory, MappingService);
        }
    }
}