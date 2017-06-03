using Link2Web.Areas.Admin.Models;
using Link2Web.Controllers;
using Link2Web.DAL;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Link2Web.Areas.Admin.Controllers
{
    public class SettingTypesController : BaseController
    {
        private Link2WebDbContext db = new Link2WebDbContext();

        // GET: Admin/SettingTypes
        public ActionResult Index()
        {
            return View(db.SettingTypes.ToList());
        }

        // GET: Admin/SettingTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingType settingType = db.SettingTypes.Find(id);
            if (settingType == null)
            {
                return HttpNotFound();
            }
            return View(settingType);
        }

        // GET: Admin/SettingTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SettingTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SettingTypeId,Type,Visible")] SettingType settingType)
        {
            if (ModelState.IsValid)
            {
                db.SettingTypes.Add(settingType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(settingType);
        }

        // GET: Admin/SettingTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingType settingType = db.SettingTypes.Find(id);
            if (settingType == null)
            {
                return HttpNotFound();
            }
            return View(settingType);
        }

        // POST: Admin/SettingTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SettingTypeId,Type,Visible")] SettingType settingType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(settingType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(settingType);
        }

        // GET: Admin/SettingTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingType settingType = db.SettingTypes.Find(id);
            if (settingType == null)
            {
                return HttpNotFound();
            }
            return View(settingType);
        }

        // POST: Admin/SettingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SettingType settingType = db.SettingTypes.Find(id);
            db.SettingTypes.Remove(settingType);
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
