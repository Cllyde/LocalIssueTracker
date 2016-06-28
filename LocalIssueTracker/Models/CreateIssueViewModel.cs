using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LocalIssueTracker.Models
{
    public class CreateIssueViewModel
    {
        public CreateIssueViewModel(Project p)
        {
            ProjectID = p.ProjectID;
            ProjectName = p.Name;
        }

        public CreateIssueViewModel() { }

        public int ProjectID { get; set; }
        [DisplayName("Project")]
        public string ProjectName { get; set; }
        [DisplayName("Name")]
        public string IssueName { get; set; }
        [DisplayName("Description")]
        public string IssueDescription { get; set; }
    }
}