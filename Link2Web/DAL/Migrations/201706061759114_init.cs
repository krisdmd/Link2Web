namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
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
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
 
            

            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CountryId);
            
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                .ForeignKey("dbo.CrawledLinkStatus", t => t.CrawledLinkStatus_CrawledLinkStatusId)
                .Index(t => t.CrawledLinkStatus_CrawledLinkStatusId);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        CurrencyId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Symbol = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CurrencyId);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        MailId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ProjectId = c.Int(nullable: false),
                        FromEmail = c.String(),
                        ToEmail = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        IsHtml = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MailId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ViewProfileId = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 60),
                        Email = c.String(nullable: false, maxLength: 60),
                        CountryId = c.Int(nullable: false),
                        Url = c.String(nullable: false),
                        PreviewImage = c.String(),
                        Note = c.String(),
                        CurrencyId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Visible = c.Boolean(nullable: false),
                        Link_LinkId = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.Links", t => t.Link_LinkId)
                .Index(t => t.UserId)
                .Index(t => t.CountryId)
                .Index(t => t.LanguageId)
                .Index(t => t.Link_LinkId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Default = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LanguageId);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        LinkId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        LinkTypeId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        WebsiteUrl = c.String(nullable: false),
                        AnchorText = c.String(nullable: false, maxLength: 40),
                        DestinationUrl = c.String(nullable: false),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        BacklinkFound = c.Boolean(nullable: false),
                        StatusId_LinkStatusId = c.Int(),
                    })
                .PrimaryKey(t => t.LinkId)
                .ForeignKey("dbo.LinkStatus", t => t.StatusId_LinkStatusId)
                .Index(t => t.StatusId_LinkStatusId);
            
            CreateTable(
                "dbo.LinkStatus",
                c => new
                    {
                        LinkStatusId = c.Int(nullable: false, identity: true),
                        Status = c.String(nullable: false),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LinkStatusId);
            
            CreateTable(
                "dbo.GoogleUsers",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 500),
                        Username = c.String(),
                        RefreshToken = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.UserId);
            
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        SettingId = c.Int(nullable: false, identity: true),
                        SettingTypeId = c.Int(nullable: false),
                        UserId = c.String(),
                        Name = c.String(),
                        Value = c.String(),
                        ValueInt = c.Int(nullable: false),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SettingId);
            
            CreateTable(
                "dbo.SettingTypes",
                c => new
                    {
                        SettingTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SettingTypeId);
            
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        UserSettingId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Setting = c.String(),
                        Value = c.String(),
                        ValueInt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserSettingId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSettings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Emails", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "Link_LinkId", "dbo.Links");
            DropForeignKey("dbo.Links", "StatusId_LinkStatusId", "dbo.LinkStatus");
            DropForeignKey("dbo.Projects", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.Projects", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Projects", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CrawledLinks", "CrawledLinkStatus_CrawledLinkStatusId", "dbo.CrawledLinkStatus");
            DropForeignKey("dbo.Contacts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CountryId_CountryId", "dbo.Countries");
            DropForeignKey("dbo.AspNetUsers", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserSettings", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Links", new[] { "StatusId_LinkStatusId" });
            DropIndex("dbo.Projects", new[] { "Link_LinkId" });
            DropIndex("dbo.Projects", new[] { "LanguageId" });
            DropIndex("dbo.Projects", new[] { "CountryId" });
            DropIndex("dbo.Projects", new[] { "UserId" });
            DropIndex("dbo.Emails", new[] { "ProjectId" });
            DropIndex("dbo.CrawledLinks", new[] { "CrawledLinkStatus_CrawledLinkStatusId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "CountryId_CountryId" });
            DropIndex("dbo.AspNetUsers", new[] { "Country_CountryId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Contacts", new[] { "UserId" });
            DropTable("dbo.UserSettings");
            DropTable("dbo.SettingTypes");
            DropTable("dbo.Settings");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LinkTypes");
            DropTable("dbo.GoogleUsers");
            DropTable("dbo.LinkStatus");
            DropTable("dbo.Links");
            DropTable("dbo.Languages");
            DropTable("dbo.Projects");
            DropTable("dbo.Emails");
            DropTable("dbo.Currencies");
            DropTable("dbo.CrawledLinks");
            DropTable("dbo.CrawledLinkStatus");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Countries");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Contacts");
        }
    }
}
