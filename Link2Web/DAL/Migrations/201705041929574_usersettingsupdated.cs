namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usersettingsupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSettings", "Setting", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSettings", "Setting");
        }
    }
}
