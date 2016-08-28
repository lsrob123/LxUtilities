using System.Data.Entity;
using Identity.Domain.Entities;
using LxUtilities.Definitions.Bootstrapping;
using LxUtilities.Services.Persistence.EF;

namespace Identity.Persistence.EF.Context
{
    public class IdentityDbContext : DbContext
    {
        [BootstrapAction]
        static IdentityDbContext()
        {
        }

        public IdentityDbContext() : this("name=Identity")
        {
        }

        public IdentityDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EntityTypeConfig<User>());

            base.OnModelCreating(modelBuilder);
        }
    }
}