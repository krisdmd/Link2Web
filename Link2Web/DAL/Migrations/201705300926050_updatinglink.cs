namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatinglink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "UserId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Contacts", new[] { "UserId_Id" });
            AddColumn("dbo.Contacts", "UserId", c => c.String());
            DropColumn("dbo.Contacts", "UserId_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "UserId_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Contacts", "UserId");
            CreateIndex("dbo.Contacts", "UserId_Id");
            AddForeignKey("dbo.Contacts", "UserId_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
