namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user : DbMigration
    {
        public override void Up()
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_CountryId)
                .ForeignKey("dbo.Countries", t => t.CountryId_CountryId, cascadeDelete: true)
                .Index(t => t.Country_CountryId)
                .Index(t => t.CountryId_CountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EditProfileViewModels", "CountryId_CountryId", "dbo.Countries");
            DropForeignKey("dbo.EditProfileViewModels", "Country_CountryId", "dbo.Countries");
            DropIndex("dbo.EditProfileViewModels", new[] { "CountryId_CountryId" });
            DropIndex("dbo.EditProfileViewModels", new[] { "Country_CountryId" });
            DropTable("dbo.EditProfileViewModels");
        }
    }
}
