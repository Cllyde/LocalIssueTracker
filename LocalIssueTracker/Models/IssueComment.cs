using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LocalIssueTracker.Models
{
    public class IssueComment
    {
        public int IssueCommentID { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [DisplayName("Owner")]
        public string OwnerUserName { get; set; }

        public virtual Issue Issue { get; set; }
    }
}