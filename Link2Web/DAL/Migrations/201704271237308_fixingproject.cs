namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingproject : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Projects", name: "UserId_Id", newName: "UserId");
            RenameIndex(table: "dbo.Projects", name: "IX_UserId_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Projects", name: "IX_UserId", newName: "IX_UserId_Id");
            RenameColumn(table: "dbo.Projects", name: "UserId", newName: "UserId_Id");
        }
    }
}
