namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currencies : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Projects", "Currency_CurrencyId", c => c.Int());
            CreateIndex("dbo.Projects", "Currency_CurrencyId");
            AddForeignKey("dbo.Projects", "Currency_CurrencyId", "dbo.Currencies", "CurrencyId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Currency_CurrencyId", "dbo.Currencies");
            DropIndex("dbo.Projects", new[] { "Currency_CurrencyId" });
            DropColumn("dbo.Projects", "Currency_CurrencyId");
            DropTable("dbo.Currencies");
        }
    }
}
