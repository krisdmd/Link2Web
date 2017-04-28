namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingsettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "ValueInt", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "ValueInt");
        }
    }
}
