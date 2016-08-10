using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Identity.Persistence.EF.Models;
using LxUtilities.Services.Mapping.AutoMapper;
using LxUtilities.Services.Persistence.EF;

namespace Identity.Persistence.EF.Context
{
    public class IdentityDbContext : DbContext
    {
        static IdentityDbContext()
        {
            //MappingService.AddEntityAndRelationalModelMap<User, IdentityUser>();

            MappingService.AddEntityAndRelationalModelMap<>();
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
            modelBuilder.Configurations.Add(new GenericEfTypeConfig<User, IdentityUser>());
                

            base.OnModelCreating(modelBuilder);
        }
    }
}