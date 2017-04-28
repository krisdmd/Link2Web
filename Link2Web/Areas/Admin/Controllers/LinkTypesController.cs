using Link2Web.DAL;
using Link2Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Link2Web.Controllers;

namespace Link2Web.Areas.Admin.Controllers
{
    public class LinkTypesController : BaseController
    {
        private Link2WebDbContext db = new Link2WebDbContext();

        // GET: Admin/LinkTypes
        public ActionResult Index()
        {
            return View(db.LinkTypes.ToList());
        }

        // GET: Admin/LinkTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkType linkType = db.LinkTypes.Find(id);
            if (linkType == null)
            {
                return HttpNotFound();
            }
            return View(linkType);
        }

        // GET: Admin/LinkTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LinkTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LinkTypeId,Type,Visible")] LinkType linkType)
        {
            if (ModelState.IsValid)
            {
                db.LinkTypes.Add(linkType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(linkType);
        }

        // GET: Admin/LinkTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkType linkType = db.LinkTypes.Find(id);
            if (linkType == null)
            {
                return HttpNotFound();
            }
            return View(linkType);
        }

        // POST: Admin/LinkTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LinkTypeId,Type,Visible")] LinkType linkType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(linkType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(linkType);
        }

        // GET: Admin/LinkTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkType linkType = db.LinkTypes.Find(id);
            if (linkType == null)
            {
                return HttpNotFound();
            }
            return View(linkType);
        }

        // POST: Admin/LinkTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LinkType linkType = db.LinkTypes.Find(id);
            db.LinkTypes.Remove(linkType);
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
