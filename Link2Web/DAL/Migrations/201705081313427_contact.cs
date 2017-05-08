namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contact : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContactDetails", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Links", "LinkContact_LinkContactId", "dbo.LinkContacts");
            DropForeignKey("dbo.Links", "LinkContact_LinkContactId1", "dbo.LinkContacts");
            DropForeignKey("dbo.Links", "LinkContactId_LinkContactId", "dbo.LinkContacts");
            DropForeignKey("dbo.Links", "WebsiteType_WebsiteTypeId", "dbo.WebsiteTypes");
            DropForeignKey("dbo.Links", "WebsiteType_WebsiteTypeId1", "dbo.WebsiteTypes");
            DropForeignKey("dbo.Links", "WebsiteTypeId_WebsiteTypeId", "dbo.WebsiteTypes");
            DropForeignKey("dbo.ContactDetails", "UserId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ContactDetails", new[] { "CountryId" });
            DropIndex("dbo.ContactDetails", new[] { "UserId_Id" });
            DropIndex("dbo.Links", new[] { "LinkContact_LinkContactId" });
            DropIndex("dbo.Links", new[] { "LinkContact_LinkContactId1" });
            DropIndex("dbo.Links", new[] { "LinkContactId_LinkContactId" });
            DropIndex("dbo.Links", new[] { "WebsiteType_WebsiteTypeId" });
            DropIndex("dbo.Links", new[] { "WebsiteType_WebsiteTypeId1" });
            DropIndex("dbo.Links", new[] { "WebsiteTypeId_WebsiteTypeId" });
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        ScreenName = c.String(),
                        Email = c.String(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        Zipcode = c.String(),
                        Phone = c.String(),
                        TwitterProfileUrl = c.String(),
                        FacebookProfileUrl = c.String(),
                        Active = c.Boolean(nullable: false),
                        Links_ProjectId = c.Int(),
                        UserId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Links", t => t.Links_ProjectId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId_Id)
                .Index(t => t.CountryId)
                .Index(t => t.Links_ProjectId)
                .Index(t => t.UserId_Id);
            
            AddColumn("dbo.Links", "BacklinkFound", c => c.Boolean(nullable: false));
            AddColumn("dbo.Links", "Contact_ContactId", c => c.Int());
            AddColumn("dbo.Links", "ContactId_ContactId", c => c.Int());
            CreateIndex("dbo.Links", "Contact_ContactId");
            CreateIndex("dbo.Links", "ContactId_ContactId");
            AddForeignKey("dbo.Links", "Contact_ContactId", "dbo.Contacts", "ContactId");
            AddForeignKey("dbo.Links", "ContactId_ContactId", "dbo.Contacts", "ContactId");
            DropColumn("dbo.Links", "LinkContact_LinkContactId");
            DropColumn("dbo.Links", "LinkContact_LinkContactId1");
            DropColumn("dbo.Links", "LinkContactId_LinkContactId");
            DropColumn("dbo.Links", "WebsiteType_WebsiteTypeId");
            DropColumn("dbo.Links", "WebsiteType_WebsiteTypeId1");
            DropColumn("dbo.Links", "WebsiteTypeId_WebsiteTypeId");
            DropTable("dbo.ContactDetails");
            DropTable("dbo.LinkContacts");
            DropTable("dbo.WebsiteTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WebsiteTypes",
                c => new
                    {
                        WebsiteTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.WebsiteTypeId);
            
            CreateTable(
                "dbo.LinkContacts",
                c => new
                    {
                        LinkContactId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        Email = c.String(nullable: false),
                        TwitterProfileUrl = c.String(),
                        FacebookProfileUrl = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LinkContactId);
            
            CreateTable(
                "dbo.ContactDetails",
                c => new
                    {
                        ContactDetailId = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        ScreenName = c.String(),
                        Email = c.String(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        Zipcode = c.String(),
                        Phone = c.String(),
                        UserId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ContactDetailId);
            
            AddColumn("dbo.Links", "WebsiteTypeId_WebsiteTypeId", c => c.Int());
            AddColumn("dbo.Links", "WebsiteType_WebsiteTypeId1", c => c.Int());
            AddColumn("dbo.Links", "WebsiteType_WebsiteTypeId", c => c.Int());
            AddColumn("dbo.Links", "LinkContactId_LinkContactId", c => c.Int());
            AddColumn("dbo.Links", "LinkContact_LinkContactId1", c => c.Int());
            AddColumn("dbo.Links", "LinkContact_LinkContactId", c => c.Int());
            DropForeignKey("dbo.Contacts", "UserId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contacts", "Links_ProjectId", "dbo.Links");
            DropForeignKey("dbo.Links", "ContactId_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Links", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Contacts", "CountryId", "dbo.Countries");
            DropIndex("dbo.Links", new[] { "ContactId_ContactId" });
            DropIndex("dbo.Links", new[] { "Contact_ContactId" });
            DropIndex("dbo.Contacts", new[] { "UserId_Id" });
            DropIndex("dbo.Contacts", new[] { "Links_ProjectId" });
            DropIndex("dbo.Contacts", new[] { "CountryId" });
            DropColumn("dbo.Links", "ContactId_ContactId");
            DropColumn("dbo.Links", "Contact_ContactId");
            DropColumn("dbo.Links", "BacklinkFound");
            DropTable("dbo.Contacts");
            CreateIndex("dbo.Links", "WebsiteTypeId_WebsiteTypeId");
            CreateIndex("dbo.Links", "WebsiteType_WebsiteTypeId1");
            CreateIndex("dbo.Links", "WebsiteType_WebsiteTypeId");
            CreateIndex("dbo.Links", "LinkContactId_LinkContactId");
            CreateIndex("dbo.Links", "LinkContact_LinkContactId1");
            CreateIndex("dbo.Links", "LinkContact_LinkContactId");
            CreateIndex("dbo.ContactDetails", "UserId_Id");
            CreateIndex("dbo.ContactDetails", "CountryId");
            AddForeignKey("dbo.ContactDetails", "UserId_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Links", "WebsiteTypeId_WebsiteTypeId", "dbo.WebsiteTypes", "WebsiteTypeId");
            AddForeignKey("dbo.Links", "WebsiteType_WebsiteTypeId1", "dbo.WebsiteTypes", "WebsiteTypeId");
            AddForeignKey("dbo.Links", "WebsiteType_WebsiteTypeId", "dbo.WebsiteTypes", "WebsiteTypeId");
            AddForeignKey("dbo.Links", "LinkContactId_LinkContactId", "dbo.LinkContacts", "LinkContactId");
            AddForeignKey("dbo.Links", "LinkContact_LinkContactId1", "dbo.LinkContacts", "LinkContactId");
            AddForeignKey("dbo.Links", "LinkContact_LinkContactId", "dbo.LinkContacts", "LinkContactId");
            AddForeignKey("dbo.ContactDetails", "CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
        }
    }
}
