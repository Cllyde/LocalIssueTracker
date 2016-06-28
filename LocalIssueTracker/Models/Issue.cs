using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalIssueTracker.Models
{
    public class Issue
    {
        public int IssueID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Modified Date")]
        public DateTime ModifiedDate { get; set; }
        [DisplayName("Status")]
        public IssueStatus IssueStatus { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<IssueComment> IssueComments { get; set; }
    }
}