using Link2Web.DAL;
using Link2Web.Helpers;
using Link2Web.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Link2Web.Controllers
{
    public class ProjectController : BaseController
    {
        private Link2WebDbContext db = new Link2WebDbContext();
        private DbAddOrUpdate dbAddorUpdate = new DbAddOrUpdate();

        public ProjectController()
        {
            GlobalSettings.HideCreateProjectDialog = true;

        }

        // GET: Project
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var projects = db.Projects.Include(p => p.Country).Where(p => p.UserId.Equals(userId));
            return View(projects.ToList());
        }

        // GET: Project/Details/5
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

        // GET: Project/Create
        public ActionResult Create()
        {
            GlobalSettings.HideCreateProjectDialog = true;
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "Name");
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "Name");

            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,Name,Email,CountryId,CurrencyId,LanguageId,Url,Note,ViewProfileId")] Project project)
        {
            GlobalSettings.HideCreateProjectDialog = true;
            if (ModelState.IsValid)
            {
                project.UserId = User.Identity.GetUserId();
                db.Projects.Add(project);
                db.SaveChanges();

                var userSettings = new UserSetting
                {
                    UserId = User.Identity.GetUserId(),
                    Setting = "CurrentProject",
                    ValueInt = project.ProjectId
                };

                System.Web.HttpContext.Current.Session["ViewProjectId"] = project.ViewProfileId;

                dbAddorUpdate.AddOrUpdateUserSetting(db, userSettings);

                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name", project.CountryId);
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "Name", project.LanguageId);
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "Name", project.CurrencyId);

            return View(project);
        }

        // GET: Project/Edit/5
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
;
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name", project.CountryId);
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "Name", project.LanguageId);
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "Name", project.CurrencyId);
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,Name,Email,CountryId,CurrencyId,LanguageId,Url,Note,ViewProfileId")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.UserId = User.Identity.GetUserId();
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();

                var userSettings = new UserSetting
                {
                    UserId = User.Identity.GetUserId(),
                    Setting = "CurrentProject",
                    ValueInt = project.ProjectId
                };

                System.Web.HttpContext.Current.Session["ViewProjectId"] = project.ViewProfileId;
                dbAddorUpdate.AddOrUpdateUserSetting(db, userSettings);

                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name", project.CountryId);
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "Name", project.LanguageId);
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "Name", project.CurrencyId);

            return View(project);
        }

        // GET: Project/Delete/5
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

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult ProjectExists()
        {
            var userId = User.Identity.GetUserId();
            var projectCount = db.Projects.Count(p => Equals(p.UserId, userId)) > 0;
            return Json(projectCount, JsonRequestBehavior.AllowGet);
        }

        // GET: Project/Select/5
        public ActionResult Select(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            var userSettings = new UserSetting
            {
                UserId = User.Identity.GetUserId(),
                Setting = "CurrentProject",
                ValueInt = project.ProjectId
            };

            System.Web.HttpContext.Current.Session["ViewProjectId"] = project.ViewProfileId;


            dbAddorUpdate.AddOrUpdateUserSetting(db, userSettings);

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
