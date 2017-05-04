namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class globalsettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CrawledLinkStatus",
                c => new
                    {
                        CrawledLinkStatusId = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CrawledLinkStatusId);
            
            CreateTable(
                "dbo.CrawledLinks",
                c => new
                    {
                        CrawledLinkId = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Status = c.String(nullable: false),
                        Total = c.Int(nullable: false),
                        CrawledLinkStatus_CrawledLinkStatusId = c.Int(),
                    })
                .PrimaryKey(t => t.CrawledLinkId)
                .Index(t => t.CrawledLinkStatus_CrawledLinkStatusId);
            
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        UserSettingId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Value = c.String(),
                        ValueInt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserSettingId)
                .Index(t => t.UserId);
            
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Projects", "Email", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Links", "AnchorText", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.SettingTypes", "Type", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserSettings", new[] { "UserId" });
            DropIndex("dbo.CrawledLinks", new[] { "CrawledLinkStatus_CrawledLinkStatusId" });
            AlterColumn("dbo.SettingTypes", "Type", c => c.String());
            AlterColumn("dbo.Links", "AnchorText", c => c.String(nullable: false));
            AlterColumn("dbo.Projects", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false));
            DropTable("dbo.UserSettings");
            DropTable("dbo.CrawledLinks");
            DropTable("dbo.CrawledLinkStatus");
        }
    }
}
