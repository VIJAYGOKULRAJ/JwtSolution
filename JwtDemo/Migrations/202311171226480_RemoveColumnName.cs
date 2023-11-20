
namespace JwtDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumnName : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LoginModels", "Role");
            DropColumn("dbo.LoginModels", "RoleEnum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoginModels", "RoleEnum", c => c.Int(nullable: false));
            AddColumn("dbo.LoginModels", "Role", c => c.String());
        }
    }
}
