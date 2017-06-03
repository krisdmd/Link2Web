namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingdb1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Settings", "SettingTypeId", "dbo.SettingTypes");
            DropForeignKey("dbo.Settings", "UserId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "CurrencyId" });
            DropIndex("dbo.Settings", new[] { "SettingTypeId" });
            DropIndex("dbo.Settings", new[] { "UserId_Id" });
            AddColumn("dbo.Settings", "UserId", c => c.String());
            DropColumn("dbo.Settings", "UserId_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "UserId_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Settings", "UserId");
            CreateIndex("dbo.Settings", "UserId_Id");
            CreateIndex("dbo.Settings", "SettingTypeId");
            CreateIndex("dbo.Projects", "CurrencyId");
            AddForeignKey("dbo.Settings", "UserId_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Settings", "SettingTypeId", "dbo.SettingTypes", "SettingTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "CurrencyId", "dbo.Currencies", "CurrencyId", cascadeDelete: true);
        }
    }
}
