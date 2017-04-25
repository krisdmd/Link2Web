using Link2Web.Controllers;
using Link2Web.DAL;
using Link2Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Link2Web.Areas.Admin.Controllers
{
    public class SettingsController : BaseController
    {
        private Link2WebDbContext db = new Link2WebDbContext();

        // GET: Admin/Settings
        public ActionResult Index()
        {
            var settings = db.Settings.Include(s => s.SettingType);
            return View(settings.ToList());
        }

        // GET: Admin/Settings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = db.Settings.Find(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // GET: Admin/Settings/Create
        public ActionResult Create()
        {
            ViewBag.SettingTypeId = new SelectList(db.SettingTypes, "SettingTypeId", "Type");
            return View();
        }

        // POST: Admin/Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SettingId,SettingTypeId,Name,Value,ValueInt,Visible")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                db.Settings.Add(setting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SettingTypeId = new SelectList(db.SettingTypes, "SettingTypeId", "Type", setting.SettingTypeId);
            return View(setting);
        }

        // GET: Admin/Settings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = db.Settings.Find(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            ViewBag.SettingTypeId = new SelectList(db.SettingTypes, "SettingTypeId", "Type", setting.SettingTypeId);
            return View(setting);
        }

        // POST: Admin/Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SettingId,SettingTypeId,Name,Value,ValueInt,Visible")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(setting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SettingTypeId = new SelectList(db.SettingTypes, "SettingTypeId", "Type", setting.SettingTypeId);
            return View(setting);
        }

        // GET: Admin/Settings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = db.Settings.Find(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // POST: Admin/Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Setting setting = db.Settings.Find(id);
            db.Settings.Remove(setting);
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
