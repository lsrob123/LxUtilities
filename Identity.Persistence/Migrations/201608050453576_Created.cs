using System.Data.Entity.Migrations;

namespace Identity.Persistence.Migrations
{
    public partial class Created : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdentityUsers",
                c => new
                {
                    Id = c.Long(false, true),
                    Entity_Username = c.String(maxLength: 50),
                    Entity_HashedPassword = c.String(maxLength: 100),
                    Entity_Email = c.String(maxLength: 50),
                    Entity_Mobile = c.String(maxLength: 50),
                    Entity_Key = c.Guid(false)
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Entity_Key, unique: true);
        }

        public override void Down()
        {
            DropIndex("dbo.IdentityUsers", new[] {"Entity_Key"});
            DropTable("dbo.IdentityUsers");
        }
    }
}