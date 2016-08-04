using System.Data.Entity;
using Identity.Domain;
using LxUtilities.Services.Persistence.EF;

namespace Identity.Persistence.EF
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext() : this("name=Identity")
        {
        }

        public IdentityDbContext(string connectionString) : base(connectionString)
        {
        }

        //public DbSet<GenericRelationalModel<User>> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GenericEfModelMap<User, Models.User>());

            base.OnModelCreating(modelBuilder);
        }
    }
}