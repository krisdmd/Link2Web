using DataTables.Mvc;
using Link2Web.DAL;
using Link2Web.DAL.Repositories;
using Link2Web.Models;
using Link2Web.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace Link2Web.Controllers
{
    public class LinksController : BaseController
    {
        private Link2WebDbContext db = new Link2WebDbContext();
        private ILinkRepository _context;


        public LinksController()
        {
            _context = new LinkRepository(new Link2WebDbContext());

            Mail.Host = "smtp.gmail.com";
            Mail.Username = "ivolink2web@gmail.com";
            Mail.Password = "#IVO#2017";
            Mail.Port = 587;
            Mail.SSL = true;
        }

        // GET: Links
        public ActionResult Index()
        {
            return View(_context.GetLinks());
        }

        // GET: Links/Details/5
        public ActionResult Details(int id)
        {
            Link link = _context.GetLinkById(id);

            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // GET: Links/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name");
            ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "Name");
            ViewBag.LinkTypeId = new SelectList(db.LinkTypes, "LinkTypeId", "Type");

            return View();
        }

        // POST: Links/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "ProjectId,LinkId,UserId,WebsiteUrl,AnchorText,DestinationUrl,Description,CreatedOn,ContactId,LinkTypeId")] Link
                link)
        {
            if (ModelState.IsValid)
            {
                //link.BacklinkFound = MyFunctions.CheckUrlExists("url", "anchor");

                link.UserId = User.Identity.GetUserId();
                db.Links.Add(link);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(link);
        }

        // GET: Links/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", link.ProjectId);
            ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "Name", link.ContactId);
            ViewBag.LinkTypeId = new SelectList(db.LinkTypes, "LinkTypeId", "Type", link.LinkTypeId);

            return View(link);
        }

        // POST: Links/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "ProjectId,LinkId,WebsiteUrl,AnchorText,DestinationUrl,Description,CreatedOn,ContactId,LinkTypeId")] Link link)
        {
            if (ModelState.IsValid)
            {
                link.UserId = User.Identity.GetUserId();
                db.Entry(link).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", link.ProjectId);
            ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "Name", link.ContactId);
            ViewBag.LinkTypeId = new SelectList(db.LinkTypes, "LinkTypeId", "Type", link.LinkTypeId);

            return View(link);
        }

        // GET: Links/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // POST: Links/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Link link = db.Links.Find(id);
            db.Links.Remove(link);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetLinks([ModelBinder(typeof (DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var userId = User.Identity.GetUserId();
            var links =
                db.Links.Select(l => new {l.LinkId, l.AnchorText, l.WebsiteUrl, l.Description, l.UserId, l.LinkTypeId, l.ContactId})
                    .Where(l => l.UserId.Equals(userId))
                    .ToList();

            // Apply filters
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();

                links =
                    links.Where(
                        l =>
                            l.AnchorText.Contains(value) || l.WebsiteUrl.Contains(value) ||
                            l.Description.Contains(value)).ToList();
            }

            var filteredCount = links.Count();

            // Sort
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = string.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != string.Empty ? "," : "";
                orderByString += (column.Data == "WebsiteUrl" ? "WebsiteUrl" : column.Data) +
                                 (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            links = links.OrderBy(orderByString == string.Empty ? "WebsiteUrl asc" : orderByString).ToList();

            // links
            links = links.Skip(requestModel.Start).Take(requestModel.Length).ToList();


            return Json(new DataTablesResponse(requestModel.Draw, links, filteredCount, links.Count),
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendMail(MailViewModel model)
        {
            var smtp = new SmtpClient
            {
                Host = Mail.Host,
                Port = Mail.Port,
                EnableSsl = Mail.SSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Mail.Username, Mail.Password)
            };

            using (var message = new MailMessage(Mail.Username, Mail.Password))
            {
                message.Subject = model.Subject;
                message.Body = model.Body;
                message.IsBodyHtml = false;
                smtp.Send(message);
            }

            return View();
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
