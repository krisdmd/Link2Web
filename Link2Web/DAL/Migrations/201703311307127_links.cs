namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class links : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        LinkId = c.Int(nullable: false),
                        WebsiteUrl = c.String(),
                        AnchorText = c.String(),
                        DestinationUrl = c.String(),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LinkContact_LinkContactId = c.Int(),
                        LinkContact_LinkContactId1 = c.Int(),
                        LinkContactId_LinkContactId = c.Int(),
                        LinkType_LinkTypeId = c.Int(),
                        LinkType_LinkTypeId1 = c.Int(),
                        LinkTypeId_LinkTypeId = c.Int(),
                        LinkStatus_LinkStatusId = c.Int(),
                        Status_LinkStatusId = c.Int(),
                        StatusId_LinkStatusId = c.Int(),
                        WebsiteType_WebsiteTypeId = c.Int(),
                        WebsiteType_WebsiteTypeId1 = c.Int(),
                        WebsiteTypeId_WebsiteTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.LinkContacts", t => t.LinkContact_LinkContactId)
                .ForeignKey("dbo.LinkContacts", t => t.LinkContact_LinkContactId1)
                .ForeignKey("dbo.LinkContacts", t => t.LinkContactId_LinkContactId)
                .ForeignKey("dbo.LinkTypes", t => t.LinkType_LinkTypeId)
                .ForeignKey("dbo.LinkTypes", t => t.LinkType_LinkTypeId1)
                .ForeignKey("dbo.LinkTypes", t => t.LinkTypeId_LinkTypeId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.LinkStatus", t => t.LinkStatus_LinkStatusId)
                .ForeignKey("dbo.LinkStatus", t => t.Status_LinkStatusId)
                .ForeignKey("dbo.LinkStatus", t => t.StatusId_LinkStatusId)
                .ForeignKey("dbo.WebsiteTypes", t => t.WebsiteType_WebsiteTypeId)
                .ForeignKey("dbo.WebsiteTypes", t => t.WebsiteType_WebsiteTypeId1)
                .ForeignKey("dbo.WebsiteTypes", t => t.WebsiteTypeId_WebsiteTypeId)
                .Index(t => t.ProjectId)
                .Index(t => t.LinkContact_LinkContactId)
                .Index(t => t.LinkContact_LinkContactId1)
                .Index(t => t.LinkContactId_LinkContactId)
                .Index(t => t.LinkType_LinkTypeId)
                .Index(t => t.LinkType_LinkTypeId1)
                .Index(t => t.LinkTypeId_LinkTypeId)
                .Index(t => t.LinkStatus_LinkStatusId)
                .Index(t => t.Status_LinkStatusId)
                .Index(t => t.StatusId_LinkStatusId)
                .Index(t => t.WebsiteType_WebsiteTypeId)
                .Index(t => t.WebsiteType_WebsiteTypeId1)
                .Index(t => t.WebsiteTypeId_WebsiteTypeId);
            
            CreateTable(
                "dbo.LinkContacts",
                c => new
                    {
                        LinkContactId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        TwitterProfileUrl = c.String(),
                        FacebookProfileUrl = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LinkContactId);
            
            CreateTable(
                "dbo.LinkTypes",
                c => new
                    {
                        LinkTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LinkTypeId);
            
            CreateTable(
                "dbo.LinkStatus",
                c => new
                    {
                        LinkStatusId = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LinkStatusId);
            
            CreateTable(
                "dbo.WebsiteTypes",
                c => new
                    {
                        WebsiteTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.WebsiteTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Links", "WebsiteTypeId_WebsiteTypeId", "dbo.WebsiteTypes");
            DropForeignKey("dbo.Links", "WebsiteType_WebsiteTypeId1", "dbo.WebsiteTypes");
            DropForeignKey("dbo.Links", "WebsiteType_WebsiteTypeId", "dbo.WebsiteTypes");
            DropForeignKey("dbo.Links", "StatusId_LinkStatusId", "dbo.LinkStatus");
            DropForeignKey("dbo.Links", "Status_LinkStatusId", "dbo.LinkStatus");
            DropForeignKey("dbo.Links", "LinkStatus_LinkStatusId", "dbo.LinkStatus");
            DropForeignKey("dbo.Links", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Links", "LinkTypeId_LinkTypeId", "dbo.LinkTypes");
            DropForeignKey("dbo.Links", "LinkType_LinkTypeId1", "dbo.LinkTypes");
            DropForeignKey("dbo.Links", "LinkType_LinkTypeId", "dbo.LinkTypes");
            DropForeignKey("dbo.Links", "LinkContactId_LinkContactId", "dbo.LinkContacts");
            DropForeignKey("dbo.Links", "LinkContact_LinkContactId1", "dbo.LinkContacts");
            DropForeignKey("dbo.Links", "LinkContact_LinkContactId", "dbo.LinkContacts");
            DropIndex("dbo.Links", new[] { "WebsiteTypeId_WebsiteTypeId" });
            DropIndex("dbo.Links", new[] { "WebsiteType_WebsiteTypeId1" });
            DropIndex("dbo.Links", new[] { "WebsiteType_WebsiteTypeId" });
            DropIndex("dbo.Links", new[] { "StatusId_LinkStatusId" });
            DropIndex("dbo.Links", new[] { "Status_LinkStatusId" });
            DropIndex("dbo.Links", new[] { "LinkStatus_LinkStatusId" });
            DropIndex("dbo.Links", new[] { "LinkTypeId_LinkTypeId" });
            DropIndex("dbo.Links", new[] { "LinkType_LinkTypeId1" });
            DropIndex("dbo.Links", new[] { "LinkType_LinkTypeId" });
            DropIndex("dbo.Links", new[] { "LinkContactId_LinkContactId" });
            DropIndex("dbo.Links", new[] { "LinkContact_LinkContactId1" });
            DropIndex("dbo.Links", new[] { "LinkContact_LinkContactId" });
            DropIndex("dbo.Links", new[] { "ProjectId" });
            DropTable("dbo.WebsiteTypes");
            DropTable("dbo.LinkStatus");
            DropTable("dbo.LinkTypes");
            DropTable("dbo.LinkContacts");
            DropTable("dbo.Links");
        }
    }
}
