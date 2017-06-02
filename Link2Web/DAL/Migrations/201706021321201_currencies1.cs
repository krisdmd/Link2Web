namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currencies1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Currencies", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Currencies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Currencies", "Symbol", c => c.String(nullable: false));
            CreateIndex("dbo.Projects", "CurrencyId");
            AddForeignKey("dbo.Projects", "CurrencyId", "dbo.Currencies", "CurrencyId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "CurrencyId", "dbo.Currencies");
            DropIndex("dbo.Projects", new[] { "CurrencyId" });
            AlterColumn("dbo.Currencies", "Symbol", c => c.String());
            AlterColumn("dbo.Currencies", "Name", c => c.String());
            AlterColumn("dbo.Currencies", "Code", c => c.String());
        }
    }
}
