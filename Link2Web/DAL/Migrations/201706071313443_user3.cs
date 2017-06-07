namespace Link2Web.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class user3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CountryId_CountryId", "dbo.Countries");
            DropIndex("dbo.AspNetUsers", new[] { "CountryId_CountryId" });
            AddColumn("dbo.AspNetUsers", "TwitterProfileUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "FacebookProfileUrl", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.AspNetUsers", "CountryId_CountryId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CountryId_CountryId");
            AddForeignKey("dbo.AspNetUsers", "CountryId_CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CountryId_CountryId", "dbo.Countries");
            DropIndex("dbo.AspNetUsers", new[] { "CountryId_CountryId" });
            AlterColumn("dbo.AspNetUsers", "CountryId_CountryId", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            DropColumn("dbo.AspNetUsers", "FacebookProfileUrl");
            DropColumn("dbo.AspNetUsers", "TwitterProfileUrl");
            CreateIndex("dbo.AspNetUsers", "CountryId_CountryId");
            AddForeignKey("dbo.AspNetUsers", "CountryId_CountryId", "dbo.Countries", "CountryId");
        }
    }
}
