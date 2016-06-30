using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalIssueTracker.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Modified Date")]
        public DateTime ModifiedDate { get; set; }
        [DisplayName("Owner")]
        public string OwnerUserName { get; set; }

        public virtual List<Issue> Issues { get; set; }
    }
}