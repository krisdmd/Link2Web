namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnalyticsVisitors", "Date", c => c.String());
            AlterColumn("dbo.AnalyticsVisitors", "Sessions", c => c.String());
            AlterColumn("dbo.AnalyticsVisitors", "NewUsers", c => c.String());
            AlterColumn("dbo.AnalyticsVisitors", "Pages", c => c.String());
            AlterColumn("dbo.AnalyticsVisitors", "AvgSessionDuration", c => c.String());
            AlterColumn("dbo.AnalyticsVisitors", "Impressions", c => c.String());
            AlterColumn("dbo.AnalyticsVisitors", "Hits", c => c.String());
            AlterColumn("dbo.AnalyticsVisitors", "Clicks", c => c.String());
            AlterColumn("dbo.AnalyticsVisitors", "BounceRate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AnalyticsVisitors", "BounceRate", c => c.Double(nullable: false));
            AlterColumn("dbo.AnalyticsVisitors", "Clicks", c => c.Int(nullable: false));
            AlterColumn("dbo.AnalyticsVisitors", "Hits", c => c.Int(nullable: false));
            AlterColumn("dbo.AnalyticsVisitors", "Impressions", c => c.Int(nullable: false));
            AlterColumn("dbo.AnalyticsVisitors", "AvgSessionDuration", c => c.Double(nullable: false));
            AlterColumn("dbo.AnalyticsVisitors", "Pages", c => c.Double(nullable: false));
            AlterColumn("dbo.AnalyticsVisitors", "NewUsers", c => c.Double(nullable: false));
            AlterColumn("dbo.AnalyticsVisitors", "Sessions", c => c.Double(nullable: false));
            DropColumn("dbo.AnalyticsVisitors", "Date");
        }
    }
}
