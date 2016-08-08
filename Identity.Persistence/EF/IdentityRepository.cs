using Identity.Persistence.EF.Context;
using LxUtilities.Definitions.Mapping;
using LxUtilities.Services.Persistence.EF;

namespace Identity.Persistence.EF
{
    public class IdentityRepository : RelationalRepositoryBase<IdentityDbContext>
    {
        public IdentityRepository(IdentityDbContext dbContext, IMappingService mappingService)
            : base(dbContext, mappingService)
        {
        }
    }
}