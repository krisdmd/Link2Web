namespace Link2Web.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class updatingdb2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "Links_ProjectId", "dbo.Links");
            DropIndex("dbo.Contacts", new[] { "Links_ProjectId" });
            RenameColumn(table: "dbo.AspNetUsers", name: "Country_CountryId1", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.AspNetUsers", name: "Country_CountryId", newName: "Country_CountryId1");
            RenameColumn(table: "dbo.AspNetUsers", name: "__mig_tmp__0", newName: "Country_CountryId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Country_CountryId1", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Country_CountryId", newName: "IX_Country_CountryId1");
            RenameIndex(table: "dbo.AspNetUsers", name: "__mig_tmp__0", newName: "IX_Country_CountryId");
            AddColumn("dbo.Links", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.Contacts", "Links_ProjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "Links_ProjectId", c => c.Int());
            DropForeignKey("dbo.Contacts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Links", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Links", new[] { "UserId" });
            DropIndex("dbo.Contacts", new[] { "UserId" });
            AlterColumn("dbo.Links", "UserId", c => c.String());
            AlterColumn("dbo.Contacts", "UserId", c => c.String());
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Country_CountryId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Country_CountryId1", newName: "IX_Country_CountryId");
            RenameIndex(table: "dbo.AspNetUsers", name: "__mig_tmp__0", newName: "IX_Country_CountryId1");
            RenameColumn(table: "dbo.AspNetUsers", name: "Country_CountryId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.AspNetUsers", name: "Country_CountryId1", newName: "Country_CountryId");
            RenameColumn(table: "dbo.AspNetUsers", name: "__mig_tmp__0", newName: "Country_CountryId1");
            CreateIndex("dbo.Contacts", "Links_ProjectId");
            AddForeignKey("dbo.Contacts", "Links_ProjectId", "dbo.Links", "ProjectId");
        }
    }
}
