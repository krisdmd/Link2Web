namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CountryId_CountryId", "dbo.Countries");
            DropForeignKey("dbo.AspNetUsers", "Country_CountryId", "dbo.Countries");
            DropIndex("dbo.AspNetUsers", new[] { "Country_CountryId" });
            DropIndex("dbo.AspNetUsers", new[] { "CountryId_CountryId" });
            RenameColumn(table: "dbo.AspNetUsers", name: "Country_CountryId", newName: "CountryId");
            AlterColumn("dbo.AspNetUsers", "CountryId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CountryId");
            AddForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "CountryId_CountryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "CountryId_CountryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries");
            DropIndex("dbo.AspNetUsers", new[] { "CountryId" });
            AlterColumn("dbo.AspNetUsers", "CountryId", c => c.Int());
            RenameColumn(table: "dbo.AspNetUsers", name: "CountryId", newName: "Country_CountryId");
            CreateIndex("dbo.AspNetUsers", "CountryId_CountryId");
            CreateIndex("dbo.AspNetUsers", "Country_CountryId");
            AddForeignKey("dbo.AspNetUsers", "Country_CountryId", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.AspNetUsers", "CountryId_CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
        }
    }
}
