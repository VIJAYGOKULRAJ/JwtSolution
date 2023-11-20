namespace JwtDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRefreshTokenTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefreshTokenModels",
                c => new
                    {
                        RefreshToken = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RefreshToken);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RefreshTokenModels");
        }
    }
}
