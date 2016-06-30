namespace LocalIssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OwnerUserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IssueComments", "OwnerUserName", c => c.String());
            AddColumn("dbo.Issues", "OwnerUserName", c => c.String());
            AddColumn("dbo.Projects", "OwnerUserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "OwnerUserName");
            DropColumn("dbo.Issues", "OwnerUserName");
            DropColumn("dbo.IssueComments", "OwnerUserName");
        }
    }
}
