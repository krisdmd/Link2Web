namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currencyEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "CurrencyId", "dbo.Currencies");
            DropIndex("dbo.Projects", new[] { "CurrencyId" });
            AlterColumn("dbo.Currencies", "Code", c => c.String());
            AlterColumn("dbo.Currencies", "Name", c => c.String());
            AlterColumn("dbo.Currencies", "Symbol", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Currencies", "Symbol", c => c.String(nullable: false));
            AlterColumn("dbo.Currencies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Currencies", "Code", c => c.String(nullable: false));
            CreateIndex("dbo.Projects", "CurrencyId");
            AddForeignKey("dbo.Projects", "CurrencyId", "dbo.Currencies", "CurrencyId", cascadeDelete: true);
        }
    }
}
