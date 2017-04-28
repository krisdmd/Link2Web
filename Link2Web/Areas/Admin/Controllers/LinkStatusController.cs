using Link2Web.DAL;
using Link2Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Link2Web.Controllers;

namespace Link2Web.Areas.Admin.Controllers
{
    public class LinkStatusController : BaseController
    {
        private Link2WebDbContext db = new Link2WebDbContext();

        // GET: Admin/LinkStatus
        public ActionResult Index()
        {
            return View(db.LinkStatus.ToList());
        }

        // GET: Admin/LinkStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkStatus linkStatus = db.LinkStatus.Find(id);
            if (linkStatus == null)
            {
                return HttpNotFound();
            }
            return View(linkStatus);
        }

        // GET: Admin/LinkStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LinkStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LinkStatusId,Status,Visible")] LinkStatus linkStatus)
        {
            if (ModelState.IsValid)
            {
                db.LinkStatus.Add(linkStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(linkStatus);
        }

        // GET: Admin/LinkStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkStatus linkStatus = db.LinkStatus.Find(id);
            if (linkStatus == null)
            {
                return HttpNotFound();
            }
            return View(linkStatus);
        }

        // POST: Admin/LinkStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LinkStatusId,Status,Visible")] LinkStatus linkStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(linkStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(linkStatus);
        }

        // GET: Admin/LinkStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkStatus linkStatus = db.LinkStatus.Find(id);
            if (linkStatus == null)
            {
                return HttpNotFound();
            }
            return View(linkStatus);
        }

        // POST: Admin/LinkStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LinkStatus linkStatus = db.LinkStatus.Find(id);
            db.LinkStatus.Remove(linkStatus);
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
