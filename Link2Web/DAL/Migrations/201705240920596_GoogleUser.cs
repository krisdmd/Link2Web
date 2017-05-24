namespace Link2Web.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class GoogleUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GoogleUsers",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 500),
                        Username = c.String(nullable: false, maxLength:500),
                        RefreshToken = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.UserId);
            
            DropTable("dbo.AnalyticsTokens");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AnalyticsTokens",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Key);
            
            DropTable("dbo.GoogleUsers");
        }
    }
}
