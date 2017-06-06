namespace Link2Web.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class foreignkeylink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Links", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Links", "ContactId_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Links", "LinkType_LinkTypeId1", "dbo.LinkTypes");
            DropForeignKey("dbo.Links", "LinkTypeId_LinkTypeId", "dbo.LinkTypes");
            DropForeignKey("dbo.Links", "Status_LinkStatusId", "dbo.LinkStatus");
            DropForeignKey("dbo.Links", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Links", "LinkType_LinkTypeId", "dbo.LinkTypes");
            DropIndex("dbo.Links", new[] { "ProjectId" });
            DropIndex("dbo.Links", new[] { "Contact_ContactId" });
            DropIndex("dbo.Links", new[] { "ContactId_ContactId" });
            DropIndex("dbo.Links", new[] { "LinkType_LinkTypeId" });
            DropIndex("dbo.Links", new[] { "LinkType_LinkTypeId1" });
            DropIndex("dbo.Links", new[] { "LinkTypeId_LinkTypeId" });
            DropIndex("dbo.Links", new[] { "LinkStatus_LinkStatusId" });
            DropIndex("dbo.Links", new[] { "Status_LinkStatusId" });
            DropColumn("dbo.Links", "StatusId_LinkStatusId");
            RenameColumn(table: "dbo.Links", name: "LinkType_LinkTypeId", newName: "LinkTypeId");
            RenameColumn(table: "dbo.Links", name: "LinkStatus_LinkStatusId", newName: "StatusId_LinkStatusId");
            DropPrimaryKey("dbo.Links");
            AddColumn("dbo.Projects", "Link_LinkId", c => c.Int());
            AddColumn("dbo.Links", "ContactId", c => c.Int(nullable: false));
            AlterColumn("dbo.Links", "LinkTypeId", c => c.Int(nullable: true));
            AddPrimaryKey("dbo.Links", "LinkId");
            CreateIndex("dbo.Projects", "Link_LinkId");
            CreateIndex("dbo.Links", "LinkTypeId");
            AddForeignKey("dbo.Projects", "Link_LinkId", "dbo.Links", "LinkId");
            AddForeignKey("dbo.Links", "LinkTypeId", "dbo.LinkTypes", "LinkTypeId", cascadeDelete: true);
            DropColumn("dbo.Links", "Contact_ContactId");
            DropColumn("dbo.Links", "ContactId_ContactId");
            DropColumn("dbo.Links", "LinkType_LinkTypeId1");
            DropColumn("dbo.Links", "LinkTypeId_LinkTypeId");
            DropColumn("dbo.Links", "Status_LinkStatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Links", "Status_LinkStatusId", c => c.Int());
            AddColumn("dbo.Links", "LinkTypeId_LinkTypeId", c => c.Int());
            AddColumn("dbo.Links", "LinkType_LinkTypeId1", c => c.Int());
            AddColumn("dbo.Links", "ContactId_ContactId", c => c.Int());
            AddColumn("dbo.Links", "Contact_ContactId", c => c.Int());
            DropForeignKey("dbo.Links", "LinkTypeId", "dbo.LinkTypes");
            DropForeignKey("dbo.Projects", "Link_LinkId", "dbo.Links");
            DropIndex("dbo.Links", new[] { "LinkTypeId" });
            DropIndex("dbo.Projects", new[] { "Link_LinkId" });
            DropPrimaryKey("dbo.Links");
            AlterColumn("dbo.Links", "LinkTypeId", c => c.Int());
            DropColumn("dbo.Links", "ContactId");
            DropColumn("dbo.Projects", "Link_LinkId");
            AddPrimaryKey("dbo.Links", "ProjectId");
            RenameColumn(table: "dbo.Links", name: "StatusId_LinkStatusId", newName: "LinkStatus_LinkStatusId");
            RenameColumn(table: "dbo.Links", name: "LinkTypeId", newName: "LinkType_LinkTypeId");
            AddColumn("dbo.Links", "StatusId_LinkStatusId", c => c.Int());
            CreateIndex("dbo.Links", "Status_LinkStatusId");
            CreateIndex("dbo.Links", "LinkStatus_LinkStatusId");
            CreateIndex("dbo.Links", "LinkTypeId_LinkTypeId");
            CreateIndex("dbo.Links", "LinkType_LinkTypeId1");
            CreateIndex("dbo.Links", "LinkType_LinkTypeId");
            CreateIndex("dbo.Links", "ContactId_ContactId");
            CreateIndex("dbo.Links", "Contact_ContactId");
            CreateIndex("dbo.Links", "ProjectId");
            AddForeignKey("dbo.Links", "LinkType_LinkTypeId", "dbo.LinkTypes", "LinkTypeId");
            AddForeignKey("dbo.Links", "ProjectId", "dbo.Projects", "ProjectId");
            AddForeignKey("dbo.Links", "Status_LinkStatusId", "dbo.LinkStatus", "LinkStatusId");
            AddForeignKey("dbo.Links", "LinkTypeId_LinkTypeId", "dbo.LinkTypes", "LinkTypeId");
            AddForeignKey("dbo.Links", "LinkType_LinkTypeId1", "dbo.LinkTypes", "LinkTypeId");
            AddForeignKey("dbo.Links", "ContactId_ContactId", "dbo.Contacts", "ContactId");
            AddForeignKey("dbo.Links", "Contact_ContactId", "dbo.Contacts", "ContactId");
        }
    }
}
