namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnalyticsToken : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalyticsTokens",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AnalyticsTokens");
        }
    }
}
