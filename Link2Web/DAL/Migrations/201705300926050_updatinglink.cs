namespace Link2Web.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class updatinglink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "UserId_Id", "dbo.AspNetUsers");
            DropColumn("dbo.Contacts", "UserId");
            AddColumn("dbo.Contacts", "UserId", c => c.String());
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