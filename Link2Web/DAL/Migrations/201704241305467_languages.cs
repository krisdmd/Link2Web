namespace Link2Web.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class languages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "Currency_CurrencyId", "dbo.Currencies");
            DropIndex("dbo.Projects", new[] { "Currency_CurrencyId" });
            DropForeignKey("dbo.Links", "LinkType_LinkTypeId1", "dbo.LinkTypes");
            DropIndex("dbo.Links", new[] { "WebsiteTypeId_WebsiteTypeId" });
            DropIndex("dbo.Links", new[] { "WebsiteType_WebsiteTypeId1" });
            DropIndex("dbo.Links", new[] { "LinkContact_LinkContactId1" });

            RenameColumn(table: "dbo.Projects", name: "Currency_CurrencyId", newName: "CurrencyId");
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LanguageId);
            
            AddColumn("dbo.Projects", "ViewProfileId", c => c.String(nullable: false));
            AddColumn("dbo.Projects", "LanguageId", c => c.Int(nullable: false));
            AlterColumn("dbo.Projects", "CurrencyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "CurrencyId");
            CreateIndex("dbo.Projects", "LanguageId");
            AddForeignKey("dbo.Projects", "LanguageId", "dbo.Languages", "LanguageId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "CurrencyId", "dbo.Currencies", "CurrencyId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Projects", "LanguageId", "dbo.Languages");
            DropIndex("dbo.Projects", new[] { "LanguageId" });
            DropIndex("dbo.Projects", new[] { "CurrencyId" });
            AlterColumn("dbo.Projects", "CurrencyId", c => c.Int());
            DropColumn("dbo.Projects", "LanguageId");
            DropColumn("dbo.Projects", "ViewProfileId");
            DropTable("dbo.Languages");
            RenameColumn(table: "dbo.Projects", name: "CurrencyId", newName: "Currency_CurrencyId");
            CreateIndex("dbo.Projects", "Currency_CurrencyId");
            AddForeignKey("dbo.Projects", "Currency_CurrencyId", "dbo.Currencies", "CurrencyId");
        }
    }
}
