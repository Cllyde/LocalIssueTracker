using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LocalIssueTracker.Models;

namespace LocalIssueTracker.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,Name,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.CreatedDate = DateTime.Now;
                project.ModifiedDate = DateTime.Now;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/CreateIssue/5
        public ActionResult CreateIssue(int? id)
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
        public ActionResult CreateIssue([Bind(Include = "ProjectID,IssueName,IssueDescription")] CreateIssueViewModel vm)
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

            return RedirectToAction("Details", new { id = p.ProjectID });
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,Name,Description,CreatedDate,ModifiedDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.ModifiedDate = DateTime.Now;
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
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
