using System.Data.Entity.Migrations;

namespace Identity.Persistence.Migrations
{
    public partial class AddedAccountStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IdentityUsers", "Entity_AccountStatus_Value", c => c.String());
            AlterColumn("dbo.IdentityUsers", "Entity_Username", c => c.String(false, 50));
            AlterColumn("dbo.IdentityUsers", "Entity_Email", c => c.String(false, 50));
        }

        public override void Down()
        {
            AlterColumn("dbo.IdentityUsers", "Entity_Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.IdentityUsers", "Entity_Username", c => c.String(maxLength: 50));
            DropColumn("dbo.IdentityUsers", "Entity_AccountStatus_Value");
        }
    }
}