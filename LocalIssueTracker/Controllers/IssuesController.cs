using LocalIssueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LocalIssueTracker.Controllers
{
    public class IssuesController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: Issues/CreateIssue/5
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
            };
            db.Issues.Add(newIssue);
            db.SaveChanges();

            return RedirectToAction("Details", "Projects", new { id = p.ProjectID });
        }

        // GET: Projects/Details/5
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
            return View(issue);
        }
    }
}