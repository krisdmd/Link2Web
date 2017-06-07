namespace Link2Web.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class user2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EditProfileViewModels", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.EditProfileViewModels", "CountryId_CountryId", "dbo.Countries");
            DropIndex("dbo.EditProfileViewModels", new[] { "Country_CountryId" });
            DropIndex("dbo.EditProfileViewModels", new[] { "CountryId_CountryId" });
            DropTable("dbo.EditProfileViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EditProfileViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 60),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        Zipcode = c.String(),
                        Phone = c.String(),
                        TwitterProfileUrl = c.String(),
                        FacebookProfileUrl = c.String(),
                        ProfilePicture = c.Binary(),
                        Country_CountryId = c.Int(),
                        CountryId_CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.EditProfileViewModels", "CountryId_CountryId");
            CreateIndex("dbo.EditProfileViewModels", "Country_CountryId");
            AddForeignKey("dbo.EditProfileViewModels", "CountryId_CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
            AddForeignKey("dbo.EditProfileViewModels", "Country_CountryId", "dbo.Countries", "CountryId");
        }
    }
}
