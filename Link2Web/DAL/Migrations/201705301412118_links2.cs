namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class links2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Links", "LinkId", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Links", "LinkId", c => c.Int(nullable: false));
        }
    }
}
