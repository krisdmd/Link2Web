namespace Link2Web.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddElmah : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AnalyticsVisitors", "AnalyticsChannels_AnalyticsChannelsId", "dbo.AnalyticsChannels");
            DropIndex("dbo.AnalyticsVisitors", new[] { "AnalyticsChannels_AnalyticsChannelsId" });
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.ContactDetails", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ContactDetails", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Projects", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Projects", "Url", c => c.String(nullable: false));
            AlterColumn("dbo.Links", "WebsiteUrl", c => c.String(nullable: false));
            AlterColumn("dbo.Links", "AnchorText", c => c.String(nullable: false));
            AlterColumn("dbo.Links", "DestinationUrl", c => c.String(nullable: false));
            AlterColumn("dbo.LinkContacts", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.LinkContacts", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.LinkStatus", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.WebsiteTypes", "Type", c => c.String(nullable: false));
            DropTable("dbo.AnalyticsChannels");
            DropTable("dbo.AnalyticsVisitors");

            Sql(@"CREATE TABLE [dbo].[ELMAH_Error]
                (
                    [ErrorId]     UNIQUEIDENTIFIER NOT NULL,
                    [Application] NVARCHAR(60)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
                    [Host]        NVARCHAR(50)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
                    [Type]        NVARCHAR(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
                    [Source]      NVARCHAR(60)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
                    [Message]     NVARCHAR(500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
                    [User]        NVARCHAR(50)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
                    [StatusCode]  INT NOT NULL,
                    [TimeUtc]     DATETIME NOT NULL,
                    [Sequence]    INT IDENTITY(1, 1) NOT NULL,
                    [AllXml]      NTEXT COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
                ) ");

            Sql(
                "EXEC('ALTER TABLE [dbo].[ELMAH_Error] WITH NOCHECK ADD CONSTRAINT[PK_ELMAH_Error] PRIMARY KEY([ErrorId])')");

            Sql(
                "EXEC('ALTER TABLE [dbo].[ELMAH_Error] ADD CONSTRAINT[DF_ELMAH_Error_ErrorId] DEFAULT(NEWID()) FOR[ErrorId]')");

            Sql(@"EXEC('CREATE NONCLUSTERED INDEX [IX_ELMAH_Error_App_Time_Seq] ON [dbo].[ELMAH_Error] 
                (
                    [Application]   ASC,
                    [TimeUtc]       DESC,
                    [Sequence]      DESC
                )')");

            Sql(
                @"EXEC('CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml] (@Application NVARCHAR(60), @ErrorId UNIQUEIDENTIFIER) AS
                    SET NOCOUNT ON
                    SELECT [AllXml] FROM [ELMAH_Error] WHERE [ErrorId] = @ErrorId AND [Application] = @Application')");

            Sql(@"EXEC('CREATE PROCEDURE [dbo].[ELMAH_GetErrorsXml]
                (@Application NVARCHAR(60), @PageIndex INT = 0, @PageSize INT = 15, @TotalCount INT OUTPUT)
                AS  
                    SET NOCOUNT ON 
                    DECLARE @FirstTimeUTC DATETIME
                    DECLARE @FirstSequence INT
                    DECLARE @StartRow INT
                    DECLARE @StartRowIndex INT
  
                    SELECT @TotalCount = COUNT(1) FROM [ELMAH_Error] WHERE [Application] = @Application
  
                    SET @StartRowIndex = @PageIndex * @PageSize + 1
  
                    IF @StartRowIndex <= @TotalCount
                    BEGIN 
                        SET ROWCOUNT @StartRowIndex
  
                        SELECT @FirstTimeUTC = [TimeUtc], @FirstSequence = [Sequence] FROM [ELMAH_Error]
                        WHERE [Application] = @Application ORDER BY [TimeUtc] DESC, [Sequence] DESC 
                    END
                    ELSE
                    BEGIN 
                        SET @PageSize = 0 
                    END
  
                    SET ROWCOUNT @PageSize
  
                    SELECT 
                        errorId     = [ErrorId], 
                        application = [Application],
                        host        = [Host], 
                        type        = [Type],
                        source      = [Source],
                        message     = [Message],
                        [user]      = [User],
                        statusCode  = [StatusCode], 
                        time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + ''Z''
                    FROM [ELMAH_Error] error WHERE [Application] = @Application AND [TimeUtc] <= @FirstTimeUTC
                    AND [Sequence] <= @FirstSequence ORDER BY [TimeUtc] DESC, [Sequence] DESC FOR XML AUTO')");

            Sql(
                @"EXEC('CREATE PROCEDURE [dbo].[ELMAH_LogError] (@ErrorId UNIQUEIDENTIFIER, @Application NVARCHAR(60), @Host NVARCHAR(30),
                  @Type NVARCHAR(100), @Source NVARCHAR(60), @Message NVARCHAR(500), @User NVARCHAR(50), @AllXml NTEXT, @StatusCode INT,
                  @TimeUtc DATETIME) AS 
                   
                 SET NOCOUNT ON
         
                 INSERT INTO [ELMAH_Error] ([ErrorId], [Application], [Host], [Type], [Source], [Message], [User], [AllXml], [StatusCode], [TimeUtc])
                 VALUES (@ErrorId, @Application, @Host, @Type, @Source, @Message, @User, @AllXml, @StatusCode, @TimeUtc)')");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AnalyticsVisitors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        Sessions = c.String(),
                        NewUsers = c.String(),
                        Pages = c.String(),
                        AvgSessionDuration = c.String(),
                        Impressions = c.String(),
                        Hits = c.String(),
                        Clicks = c.String(),
                        BounceRate = c.String(),
                        AnalyticsChannels_AnalyticsChannelsId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AnalyticsChannels",
                c => new
                    {
                        AnalyticsChannelsId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AnalyticsChannelsId);
            
            AlterColumn("dbo.WebsiteTypes", "Type", c => c.String());
            AlterColumn("dbo.LinkStatus", "Status", c => c.String());
            AlterColumn("dbo.LinkContacts", "Email", c => c.String());
            AlterColumn("dbo.LinkContacts", "FirstName", c => c.String());
            AlterColumn("dbo.Links", "DestinationUrl", c => c.String());
            AlterColumn("dbo.Links", "AnchorText", c => c.String());
            AlterColumn("dbo.Links", "WebsiteUrl", c => c.String());
            AlterColumn("dbo.Projects", "Url", c => c.String());
            AlterColumn("dbo.Projects", "Email", c => c.String());
            AlterColumn("dbo.Projects", "Name", c => c.String());
            AlterColumn("dbo.Countries", "Code", c => c.String());
            AlterColumn("dbo.Countries", "Name", c => c.String());
            AlterColumn("dbo.ContactDetails", "Email", c => c.String());
            AlterColumn("dbo.ContactDetails", "Name", c => c.String());
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            CreateIndex("dbo.AnalyticsVisitors", "AnalyticsChannels_AnalyticsChannelsId");
            AddForeignKey("dbo.AnalyticsVisitors", "AnalyticsChannels_AnalyticsChannelsId", "dbo.AnalyticsChannels", "AnalyticsChannelsId");

            Sql("EXEC('DROP PROCEDURE [ELMAH_GetErrorXml]')");
            Sql("EXEC('DROP PROCEDURE [ELMAH_GetErrorsXml]')");
            Sql("EXEC('DROP PROCEDURE [ELMAH_LogError]')");
            Sql("Drop table ELMAH_Error");
        }
    }
}
