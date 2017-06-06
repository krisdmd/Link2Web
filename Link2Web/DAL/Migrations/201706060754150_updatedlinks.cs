namespace Link2Web.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class updatedlinks : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.Contacts", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.EditProfileViewModels", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.EditProfileViewModels", "CountryId_CountryId", "dbo.Countries");
            DropForeignKey("dbo.EditProfileViewModels", "Country_CountryId1", "dbo.Countries");
            DropForeignKey("dbo.Links", "LinkTypeId", "dbo.LinkTypes");
            DropIndex("dbo.Contacts", new[] { "CountryId" });
            DropIndex("dbo.AspNetUsers", new[] { "Country_CountryId1" });
            DropIndex("dbo.EditProfileViewModels", new[] { "Country_CountryId" });
            DropIndex("dbo.EditProfileViewModels", new[] { "CountryId_CountryId" });
            DropIndex("dbo.EditProfileViewModels", new[] { "Country_CountryId1" });
            DropIndex("dbo.Links", new[] { "LinkTypeId" });
            DropColumn("dbo.Links", "WebsiteType_WebsiteTypeId1");
            DropColumn("dbo.Links", "LinkContact_LinkContactId1");     
            DropTable("dbo.EditProfileViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EditProfileViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        ConfirmPassword = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        Zipcode = c.String(),
                        Phone = c.String(),
                        ProfilePicture = c.Binary(),
                        Country_CountryId = c.Int(),
                        CountryId_CountryId = c.Int(nullable: false),
                        Country_CountryId1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            RenameColumn(table: "dbo.AspNetUsers", name: "Country_CountryId", newName: "Country_CountryId1");
            AddColumn("dbo.AspNetUsers", "Country_CountryId", c => c.Int());
            CreateIndex("dbo.Links", "LinkTypeId");
            CreateIndex("dbo.EditProfileViewModels", "Country_CountryId1");
            CreateIndex("dbo.EditProfileViewModels", "CountryId_CountryId");
            CreateIndex("dbo.EditProfileViewModels", "Country_CountryId");
            CreateIndex("dbo.AspNetUsers", "Country_CountryId1");
            CreateIndex("dbo.Contacts", "CountryId");
            AddForeignKey("dbo.Links", "LinkTypeId", "dbo.LinkTypes", "LinkTypeId", cascadeDelete: true);
            AddForeignKey("dbo.EditProfileViewModels", "Country_CountryId1", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.EditProfileViewModels", "CountryId_CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
            AddForeignKey("dbo.EditProfileViewModels", "Country_CountryId", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.Contacts", "CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "Country_CountryId", "dbo.Countries", "CountryId");
        }
    }
}
