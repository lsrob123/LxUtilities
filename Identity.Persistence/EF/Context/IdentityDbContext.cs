using System.Data.Entity;
using Identity.Domain;
using Identity.Persistence.EF.Models;
using LxUtilities.Services.Mapping.AutoMapper;
using LxUtilities.Services.Persistence.EF;

namespace Identity.Persistence.EF.Context
{
    public class IdentityDbContext : DbContext
    {
        static IdentityDbContext()
        {
            MappingService.AddEntityAndRelationalModelMap<User, IdentityUser>();
        }

        public IdentityDbContext() : this("name=Identity")
        {
        }

        public IdentityDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GenericEfTypeConfig<User, IdentityUser>());

            base.OnModelCreating(modelBuilder);
        }
    }
}