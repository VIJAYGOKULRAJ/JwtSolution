namespace JwtDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAllTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SavedSearches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        UserId = c.Guid(nullable: false),
                        SearchQueryJson = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastModifiedBy = c.String(),
                        SavedSearchViewId = c.Int(nullable: false),
                        Type = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.SavedSearchViews", t => t.SavedSearchViewId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SavedSearchViewId);
            
            CreateTable(
                "dbo.SavedSearchViews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FieldNameJson = c.String(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedBy = c.String(),
                        Name = c.String(),
                        UserId = c.Guid(nullable: false),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SavedSearchShares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SavedSearchId = c.Int(nullable: false),
                        SharedUserId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        LastModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SavedSearches", t => t.SavedSearchId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.SharedUserId, cascadeDelete: false)
                .Index(t => t.SavedSearchId)
                .Index(t => t.SharedUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavedSearchShares", "SharedUserId", "dbo.Users");
            DropForeignKey("dbo.SavedSearchShares", "SavedSearchId", "dbo.SavedSearches");
            DropForeignKey("dbo.SavedSearches", "SavedSearchViewId", "dbo.SavedSearchViews");
            DropForeignKey("dbo.SavedSearchViews", "UserId", "dbo.Users");
            DropForeignKey("dbo.SavedSearches", "UserId", "dbo.Users");
            DropIndex("dbo.SavedSearchShares", new[] { "SharedUserId" });
            DropIndex("dbo.SavedSearchShares", new[] { "SavedSearchId" });
            DropIndex("dbo.SavedSearchViews", new[] { "UserId" });
            DropIndex("dbo.SavedSearches", new[] { "SavedSearchViewId" });
            DropIndex("dbo.SavedSearches", new[] { "UserId" });
            DropTable("dbo.SavedSearchShares");
            DropTable("dbo.SavedSearchViews");
            DropTable("dbo.SavedSearches");
        }
    }
}
