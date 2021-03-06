﻿using DataTables.Mvc;
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
        private ILinkRepository _context;
        private IContactRepository _contactsContext;
        private ILinkTypeRepository _linkTypeContext;
        private IProjectRepository _projectContext;

        public LinksController()
        {
            _context = new LinkRepository(new Link2WebDbContext());
            _contactsContext = new ContactRepository(new Link2WebDbContext());
            _linkTypeContext = new LinkTypeRepository(new Link2WebDbContext());
            _projectContext = new ProjectRepository(new Link2WebDbContext());


            Mail.Host = "smtp.gmail.com";
            Mail.Username = "ivolink2web@gmail.com";
            Mail.Password = "#IVO#2017";
            Mail.Port = 587;
            Mail.SSL = true;
        }

        // GET: Links
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            return View(_context.GetLinks().Where(l => l.ProjectId == (int)Session["ProjectId"] && l.UserId.Equals(userId)));
        }

        // GET: Links/Details/5
        public ActionResult Details(int id)
        {
            var userId = User.Identity.GetUserId();
            Link link = _context.GetLinkById(id, userId);

            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // GET: Links/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();

            ViewBag.ProjectId = new SelectList(_projectContext.GetProjects().Where(p => p.UserId.Equals(userId)), "ProjectId", "Name");
            ViewBag.ContactId = new SelectList(_contactsContext.GetContacts().Where(c => c.UserId.Equals(userId)), "ContactId", "Name");
            ViewBag.LinkTypeId = new SelectList(_linkTypeContext.GetLinkTypes(), "LinkTypeId", "Type");

            return View();
        }

        // POST: Links/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(
                Include =
                    "ProjectId,LinkId,UserId,WebsiteUrl,AnchorText,DestinationUrl,Description,CreatedOn,ContactId,LinkTypeId"
                )] Link
                link)
        {

            var userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                link.UserId = User.Identity.GetUserId();
                _context.InsertLink(link);
                _context.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(_projectContext.GetProjects().Where(p => p.UserId.Equals(userId)), "ProjectId", "Name", link.ProjectId);
            ViewBag.ContactId = new SelectList(_contactsContext.GetContacts().Where(c => c.UserId.Equals(userId)), "ContactId", "Name", link.ContactId);
            ViewBag.LinkTypeId = new SelectList(_linkTypeContext.GetLinkTypes(), "LinkTypeId", "Type", link.LinkTypeId);

            return View(link);
        }

        // GET: Links/Edit/5
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            Link link = _context.GetLinkById(id, userId);

            if (link == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProjectId = new SelectList(_projectContext.GetProjects().Where(p => p.UserId.Equals(userId)), "ProjectId", "Name", link.ProjectId);
            ViewBag.ContactId = new SelectList(_contactsContext.GetContacts().Where(c => c.UserId.Equals(userId)), "ContactId", "Name", link.ContactId);
            ViewBag.LinkTypeId = new SelectList(_linkTypeContext.GetLinkTypes(), "LinkTypeId", "Type", link.LinkTypeId);

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
            var userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                link.UserId = User.Identity.GetUserId();
                _context.UpdateLink(link);
                _context.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(_projectContext.GetProjects().Where(p => p.UserId.Equals(userId)), "ProjectId", "Name", link.ProjectId);
            ViewBag.ContactId = new SelectList(_contactsContext.GetContacts().Where(c => c.UserId.Equals(userId)), "ContactId", "Name", link.ContactId);
            ViewBag.LinkTypeId = new SelectList(_linkTypeContext.GetLinkTypes(), "LinkTypeId", "Type", link.LinkTypeId);

            return View(link);
        }

        // GET: Links/Delete/5
        public ActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            Link link = _context.GetLinkById(id, userId);

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
            _context.DeleteLink(id);
            _context.Save();
            return RedirectToAction("Index");
        }

        public JsonResult GetLinks([ModelBinder(typeof (DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var userId = User.Identity.GetUserId();
            var links =
                _context.GetLinks().Select(l => new {l.LinkId, l.AnchorText, l.WebsiteUrl, l.Description, l.UserId, l.ProjectId})
                    .Where(l => l.UserId.Equals(userId) && l.ProjectId == (int)Session["ProjectId"])
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
            _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
