using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LocalIssueTracker.Models
{
    public class IssueDetailsViewModel
    {
        public IssueDetailsViewModel() { }

        public IssueDetailsViewModel(Issue i)
        {
            IssueProjectID = i.Project.ProjectID;
            IssueProjectName = i.Project.Name;
            IssueID = i.IssueID;
            IssueName = i.Name;
            IssueDescription = i.Description;
            IssueCreatedDate = i.CreatedDate;
            IssueModifiedDate = i.ModifiedDate;
            IssueStatus = i.IssueStatus;
            IssueComments = i.IssueComments;

            NewCommentText = String.Empty;
        }

        public int IssueProjectID { get; set; }
        public string IssueProjectName { get; set; }

        public int IssueID { get; set; }
        public string IssueName { get; set; }

        public string IssueDescription { get; set; }
        public DateTime IssueCreatedDate { get; set; }
        public DateTime IssueModifiedDate { get; set; }
        public IssueStatus IssueStatus { get; set; }

        public ICollection<IssueComment> IssueComments { get; set; }

        [DisplayName("Text")]
        public string NewCommentText { get; set; }
    }
}