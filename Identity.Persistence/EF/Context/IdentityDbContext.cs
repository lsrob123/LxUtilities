using System.Data.Entity;
using Identity.Domain.Entities;
using Identity.Persistence.EF.Models;
using LxUtilities.Services.Bootstrapping;
using LxUtilities.Services.Mapping.AutoMapper;
using LxUtilities.Services.Persistence.EF;

namespace Identity.Persistence.EF.Context
{
    public class IdentityDbContext : DbContext
    {
        [BootstrapAction]
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
            //TODO: add more indexes
            var usersConfig = new GenericEfTypeConfig<User, IdentityUser>();
            modelBuilder.Configurations.Add(usersConfig);

            base.OnModelCreating(modelBuilder);
        }
    }
}