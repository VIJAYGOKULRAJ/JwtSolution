namespace JwtDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRoleColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoginModels", "Role", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoginModels", "Role");
        }
    }
}
