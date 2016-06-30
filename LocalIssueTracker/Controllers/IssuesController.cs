using LocalIssueTracker.Attributes;
using LocalIssueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LocalIssueTracker.Controllers
{
    [Authorize]
    public class IssuesController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: Issues/Create/5
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var p = db.Projects.Find(id);
            if (p == null)
            {
                throw new KeyNotFoundException("The project ID provided does not relate to an existing project.");
            }

            var vm = new CreateIssueViewModel(p);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,IssueName,IssueDescription")] CreateIssueViewModel vm)
        {
            if (vm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var p = db.Projects.Find(vm.ProjectID);
            if (p == null)
            {
                throw new KeyNotFoundException("The project ID provided does not relate to an existing project.");
            }

            var newIssue = new Issue()
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Name = vm.IssueName,
                Description = vm.IssueDescription,
                Project = p,
                OwnerUserName = User.Identity.Name,
            };
            db.Issues.Add(newIssue);
            db.SaveChanges();

            return RedirectToAction("Details", "Projects", new { id = p.ProjectID });
        }

        // GET: Issues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            IssueDetailsViewModel vm = new IssueDetailsViewModel(issue);
            return View(vm);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Comment")]
        public ActionResult Comment([Bind(Include = "IssueID,NewCommentText")]IssueDetailsViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Details", vm);
            }
            if (vm.IssueID == 0)
            {
                throw new ArgumentException("The IssueID cannot be 0.");
            }

            Issue i = db.Issues.Find(vm.IssueID);

            if (i == null)
            {
                throw new KeyNotFoundException(String.Format("The issue for ID {0} was not found.", vm.IssueID));
            }

            AddIssueComment(vm.NewCommentText, i);

            return RedirectToAction("Details", "Issues", new { id = vm.IssueID });
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "CommentAndClose")]
        public ActionResult CommentAndClose([Bind(Include = "IssueID,NewCommentText")]IssueDetailsViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Details", vm);
            }
            if (vm.IssueID == 0)
            {
                throw new ArgumentException("The IssueID cannot be 0.");
            }

            Issue i = db.Issues.Find(vm.IssueID);

            if (i == null)
            {
                throw new KeyNotFoundException(String.Format("The issue for ID {0} was not found.", vm.IssueID));
            }

            AddIssueComment(vm.NewCommentText, i);
            CloseIssue(i);

            return RedirectToAction("Details", "Issues", new { id = vm.IssueID });
        }

        private bool AddIssueComment(string commentText, Issue issue)
        {
            bool succeeded = false;
            try
            {
                IssueComment ic = new IssueComment();
                ic.CreatedDate = DateTime.Now;
                ic.ModifiedDate = DateTime.Now;
                ic.Text = commentText;
                ic.Issue = issue;
                ic.OwnerUserName = User.Identity.Name;

                db.IssueComments.Add(ic);
                db.SaveChanges();
                succeeded = true;
            }
            catch (Exception ex)
            {
                succeeded = false;
            }

            return succeeded;
        }

        private bool CloseIssue(Issue issue)
        {
            bool succeeded = false;
            try
            {
                issue.IssueStatus = IssueStatus.Closed;
                issue.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                succeeded = true;
            }
            catch (Exception ex)
            {
                succeeded = false;
            }

            return succeeded;
        }

        private bool OpenIssue(Issue issue)
        {
            bool succeeded = false;
            try
            {
                issue.IssueStatus = IssueStatus.Open;
                issue.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                succeeded = true;
            }
            catch (Exception ex)
            {
                succeeded = false;
            }

            return succeeded;
        }

        public ActionResult DeleteIssueComment(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            IssueComment ic = db.IssueComments.Find(id);

            if (ic == null)
            {
                throw new KeyNotFoundException(String.Format("IssueComment for ID {0} not found.", id));
            }

            int issueId = ic.Issue.IssueID;
            db.IssueComments.Remove(ic);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = issueId });
        }

        // GET: Issues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            Issue issue = db.Issues.Find(id);
            return View(issue);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Issue issue = db.Issues.Find(id);
            int projectId = issue.Project.ProjectID;
            var comments = issue.IssueComments.ToList();

            // Delete the comments for the issue because we can't have
            // comments pointing to an invalid issue (referential integrity).
            for (int i = comments.Count - 1; i >= 0; i--)
            {
                db.IssueComments.Remove(comments[i]);
            }

            db.Issues.Remove(issue);
            db.SaveChanges();
            return RedirectToAction("Details", "Projects", new { id = projectId });
        }

        // GET: Issues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            Issue issue = db.Issues.Find(id);
            return View(issue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IssueID,Name,Description,IssueStatus")] Issue issue)
        {
            if (issue.IssueID == 0)
            {
                throw new ArgumentException("id");
            }

            Issue i = db.Issues.Find(issue.IssueID);
            i.Name = issue.Name;
            i.Description = issue.Description;
            i.ModifiedDate = DateTime.Now;
            i.IssueStatus = issue.IssueStatus;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = i.IssueID });
        }

        public ActionResult SwitchStatus(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            Issue issue = db.Issues.Find(id);

            if (issue.IssueStatus == IssueStatus.Open)
            {
                CloseIssue(issue);
            }
            else
            {
                OpenIssue(issue);
            }

            return RedirectToAction("Details", new { id = issue.IssueID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}