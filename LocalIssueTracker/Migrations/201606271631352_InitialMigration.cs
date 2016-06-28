namespace LocalIssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IssueComments",
                c => new
                    {
                        IssueCommentID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Issue_IssueID = c.Int(),
                    })
                .PrimaryKey(t => t.IssueCommentID)
                .ForeignKey("dbo.Issues", t => t.Issue_IssueID)
                .Index(t => t.Issue_IssueID);
            
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        IssueID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IssueStatus = c.Int(nullable: false),
                        Project_ProjectID = c.Int(),
                    })
                .PrimaryKey(t => t.IssueID)
                .ForeignKey("dbo.Projects", t => t.Project_ProjectID)
                .Index(t => t.Project_ProjectID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "Project_ProjectID", "dbo.Projects");
            DropForeignKey("dbo.IssueComments", "Issue_IssueID", "dbo.Issues");
            DropIndex("dbo.Issues", new[] { "Project_ProjectID" });
            DropIndex("dbo.IssueComments", new[] { "Issue_IssueID" });
            DropTable("dbo.Projects");
            DropTable("dbo.Issues");
            DropTable("dbo.IssueComments");
        }
    }
}
