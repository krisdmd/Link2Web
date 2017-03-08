namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnalyticsVisitors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalyticsChannels",
                c => new
                    {
                        AnalyticsChannelsId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AnalyticsChannelsId);
            
            CreateTable(
                "dbo.AnalyticsVisitors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sessions = c.Double(nullable: false),
                        NewUsers = c.Double(nullable: false),
                        Pages = c.Double(nullable: false),
                        AvgSessionDuration = c.Double(nullable: false),
                        Impressions = c.Int(nullable: false),
                        Hits = c.Int(nullable: false),
                        Clicks = c.Int(nullable: false),
                        BounceRate = c.Double(nullable: false),
                        AnalyticsChannels_AnalyticsChannelsId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnalyticsChannels", t => t.AnalyticsChannels_AnalyticsChannelsId)
                .Index(t => t.AnalyticsChannels_AnalyticsChannelsId);
            
            CreateTable(
                "dbo.ContactDetails",
                c => new
                    {
                        ContactDetailId = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(maxLength: 4000),
                        ScreenName = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 4000),
                        Address = c.String(maxLength: 4000),
                        City = c.String(maxLength: 4000),
                        Zipcode = c.String(maxLength: 4000),
                        Phone = c.String(maxLength: 4000),
                        UserId_Id = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.ContactDetailId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId_Id)
                .Index(t => t.CountryId)
                .Index(t => t.UserId_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                        Code = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 4000),
                        CountryId = c.Int(nullable: false),
                        Url = c.String(maxLength: 4000),
                        PreviewImage = c.String(maxLength: 4000),
                        Note = c.String(maxLength: 4000),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Visible = c.Boolean(nullable: false),
                        UserId_Id = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId_Id)
                .Index(t => t.CountryId)
                .Index(t => t.UserId_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 4000),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 4000),
                        SecurityStamp = c.String(maxLength: 4000),
                        PhoneNumber = c.String(maxLength: 4000),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 4000),
                        ClaimType = c.String(maxLength: 4000),
                        ClaimValue = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 4000),
                        ProviderKey = c.String(nullable: false, maxLength: 4000),
                        UserId = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 4000),
                        RoleId = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 4000),
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
                        Name = c.String(maxLength: 4000),
                        Value = c.String(maxLength: 4000),
                        Visible = c.Boolean(nullable: false),
                        UserId_Id = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.SettingId)
                .ForeignKey("dbo.SettingTypes", t => t.SettingTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId_Id)
                .Index(t => t.SettingTypeId)
                .Index(t => t.UserId_Id);
            
            CreateTable(
                "dbo.SettingTypes",
                c => new
                    {
                        SettingTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 4000),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SettingTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Settings", "UserId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Settings", "SettingTypeId", "dbo.SettingTypes");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ContactDetails", "UserId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "UserId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.ContactDetails", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.AnalyticsVisitors", "AnalyticsChannels_AnalyticsChannelsId", "dbo.AnalyticsChannels");
            DropIndex("dbo.Settings", new[] { "UserId_Id" });
            DropIndex("dbo.Settings", new[] { "SettingTypeId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Projects", new[] { "UserId_Id" });
            DropIndex("dbo.Projects", new[] { "CountryId" });
            DropIndex("dbo.ContactDetails", new[] { "UserId_Id" });
            DropIndex("dbo.ContactDetails", new[] { "CountryId" });
            DropIndex("dbo.AnalyticsVisitors", new[] { "AnalyticsChannels_AnalyticsChannelsId" });
            DropTable("dbo.SettingTypes");
            DropTable("dbo.Settings");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Projects");
            DropTable("dbo.Countries");
            DropTable("dbo.ContactDetails");
            DropTable("dbo.AnalyticsVisitors");
            DropTable("dbo.AnalyticsChannels");
        }
    }
}
