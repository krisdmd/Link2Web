namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        MailId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ProjectId = c.Int(nullable: false),
                        FromEmail = c.String(),
                        ToEmail = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        IsHtml = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MailId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emails", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Emails", new[] { "ProjectId" });
            DropTable("dbo.Emails");
        }
    }
}
