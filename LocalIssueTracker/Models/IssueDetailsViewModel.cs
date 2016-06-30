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
            IssueComments = i.IssueComments;
            IssueCreatedDate = i.CreatedDate;
            IssueDescription = i.Description;
            IssueID = i.IssueID;
            IssueModifiedDate = i.ModifiedDate;
            IssueName = i.Name;
            IssueOwnerUserName = i.OwnerUserName;
            IssueProjectID = i.Project.ProjectID;
            IssueProjectName = i.Project.Name;
            IssueStatus = i.IssueStatus;
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
        [DisplayName("Owner")]
        public string IssueOwnerUserName { get; set; }
        [DisplayName("Status")]
        public IssueStatus IssueStatus { get; set; }

        public ICollection<IssueComment> IssueComments { get; set; }

        [DisplayName("Text")]
        [Required]
        [StringLength(350, MinimumLength=10, ErrorMessage="Comment Text has to have at least 10 characters")]
        public string NewCommentText { get; set; }
    }
}