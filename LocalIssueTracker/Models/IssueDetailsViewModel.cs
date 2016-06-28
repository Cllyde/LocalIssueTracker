using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [DisplayName("Project Name")]
        public string IssueProjectName { get; set; }

        public int IssueID { get; set; }
        [DisplayName("Name")]
        public string IssueName { get; set; }

        [DisplayName("Description")]
        public string IssueDescription { get; set; }
        [DisplayName("Created Date")]
        public DateTime IssueCreatedDate { get; set; }
        [DisplayName("Modified Date")]
        public DateTime IssueModifiedDate { get; set; }
        [DisplayName("Status")]
        public IssueStatus IssueStatus { get; set; }

        public ICollection<IssueComment> IssueComments { get; set; }

        [DisplayName("Text")]
        [Required, StringLength(350, MinimumLength=10, ErrorMessage="Comment Text has to have at least 10 characters")]
        public string NewCommentText { get; set; }
    }
}