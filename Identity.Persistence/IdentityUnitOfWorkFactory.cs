using Identity.Persistence.EF.Context;
using LxUtilities.Definitions.Caching;
using LxUtilities.Definitions.Mapping;
using LxUtilities.Definitions.Persistence;
using LxUtilities.Services.Persistence;

namespace Identity.Persistence
{
    public class IdentityUnitOfWorkFactory : UnitOfWorkFactoryBase<IdentityUnitOfWork>
    {
        protected readonly ICacheWithTransactions Cache;
        protected readonly string ConnectionString;
        protected readonly IMappingService MappingService;

        public IdentityUnitOfWorkFactory(string connectionString, ICacheWithTransactions cache, IMappingService mappingService)
        {
            ConnectionString = connectionString;
            Cache = cache;
            MappingService = mappingService;
        }

        protected override IdentityUnitOfWork GetUnitOfWork()
        {
            return new IdentityUnitOfWork(() => new IdentityDbContext(ConnectionString), Cache, MappingService);
        }
    }
}