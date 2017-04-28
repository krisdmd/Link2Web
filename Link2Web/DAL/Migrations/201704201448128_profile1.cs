namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profile1 : DbMigration
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
                        Country_CountryId1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_CountryId)
                .ForeignKey("dbo.Countries", t => t.CountryId_CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.Country_CountryId1)
                .Index(t => t.Country_CountryId)
                .Index(t => t.CountryId_CountryId)
                .Index(t => t.Country_CountryId1);
            
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "Zipcode", c => c.String());
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
            AddColumn("dbo.AspNetUsers", "Country_CountryId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "CountryId_CountryId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Country_CountryId1", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Country_CountryId");
            CreateIndex("dbo.AspNetUsers", "CountryId_CountryId");
            CreateIndex("dbo.AspNetUsers", "Country_CountryId1");
            AddForeignKey("dbo.AspNetUsers", "Country_CountryId", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.AspNetUsers", "CountryId_CountryId", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.AspNetUsers", "Country_CountryId1", "dbo.Countries", "CountryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EditProfileViewModels", "Country_CountryId1", "dbo.Countries");
            DropForeignKey("dbo.EditProfileViewModels", "CountryId_CountryId", "dbo.Countries");
            DropForeignKey("dbo.EditProfileViewModels", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.AspNetUsers", "Country_CountryId1", "dbo.Countries");
            DropForeignKey("dbo.AspNetUsers", "CountryId_CountryId", "dbo.Countries");
            DropForeignKey("dbo.AspNetUsers", "Country_CountryId", "dbo.Countries");
            DropIndex("dbo.EditProfileViewModels", new[] { "Country_CountryId1" });
            DropIndex("dbo.EditProfileViewModels", new[] { "CountryId_CountryId" });
            DropIndex("dbo.EditProfileViewModels", new[] { "Country_CountryId" });
            DropIndex("dbo.AspNetUsers", new[] { "Country_CountryId1" });
            DropIndex("dbo.AspNetUsers", new[] { "CountryId_CountryId" });
            DropIndex("dbo.AspNetUsers", new[] { "Country_CountryId" });
            DropColumn("dbo.AspNetUsers", "Country_CountryId1");
            DropColumn("dbo.AspNetUsers", "CountryId_CountryId");
            DropColumn("dbo.AspNetUsers", "Country_CountryId");
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "Zipcode");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Address");
            DropTable("dbo.EditProfileViewModels");
        }
    }
}
