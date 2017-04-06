using Link2Web.DAL;
using Link2Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Link2Web.Areas.Admin.Controllers
{
    public class WebsiteTypesController : BaseController
    {
        private Link2WebDbContext db = new Link2WebDbContext();

        // GET: Admin/WebsiteTypes
        public ActionResult Index()
        {
            return View(db.WebsiteTypes.ToList());
        }

        // GET: Admin/WebsiteTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebsiteType websiteType = db.WebsiteTypes.Find(id);
            if (websiteType == null)
            {
                return HttpNotFound();
            }
            return View(websiteType);
        }

        // GET: Admin/WebsiteTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/WebsiteTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WebsiteTypeId,Type,Visible")] WebsiteType websiteType)
        {
            if (ModelState.IsValid)
            {
                db.WebsiteTypes.Add(websiteType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(websiteType);
        }

        // GET: Admin/WebsiteTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebsiteType websiteType = db.WebsiteTypes.Find(id);
            if (websiteType == null)
            {
                return HttpNotFound();
            }
            return View(websiteType);
        }

        // POST: Admin/WebsiteTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WebsiteTypeId,Type,Visible")] WebsiteType websiteType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(websiteType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(websiteType);
        }

        // GET: Admin/WebsiteTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebsiteType websiteType = db.WebsiteTypes.Find(id);
            if (websiteType == null)
            {
                return HttpNotFound();
            }
            return View(websiteType);
        }

        // POST: Admin/WebsiteTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WebsiteType websiteType = db.WebsiteTypes.Find(id);
            db.WebsiteTypes.Remove(websiteType);
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
