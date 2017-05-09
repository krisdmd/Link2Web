namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class links1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Languages", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Languages", "Code");
        }
    }
}
