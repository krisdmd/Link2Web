namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminlanguages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Languages", "Default", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Languages", "Default");
        }
    }
}
