using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LocalIssueTracker.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext() : base("DefaultConnection") { }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<IssueComment> IssueComments { get; set; }
    }
}