using DataTables.Mvc;
using Link2Web.DAL;
using Link2Web.DAL.Repositories;
using Link2Web.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace Link2Web.Controllers
{
    public class ContactsController : BaseController
    {
        private IContactRepository _context;

        public ContactsController()
        {
            _context = new ContactRepository(new Link2WebDbContext());
        }

        // GET: Contacts
        public ActionResult Index()
        {
            return View();
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int id)
        {
            Contact contact = _context.GetContactById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(_context.GetContacts(), "CountryId", "Name");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactId,CountryId,Name,ScreenName,Email,Address,City,Zipcode,Phone")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.UserId = User.Identity.GetUserId();
                _context.InsertContact(contact);
                _context.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(_context.GetContacts(), "CountryId", "Name", contact.CountryId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int id)
        {
            Contact contact = _context.GetContactById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(_context.GetContacts(), "CountryId", "Name", contact.CountryId);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactId,CountryId,Name,ScreenName,Email,Address,City,Zipcode,Phone")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.UserId = User.Identity.GetUserId();
                _context.UpdateContact(contact);
                _context.Save();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(_context.GetContacts(), "CountryId", "Name", contact.CountryId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int id)
        {
            Contact contact = _context.GetContactById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _context.DeleteContact(id);
            _context.Save();
            return RedirectToAction("Index");
        }

        public JsonResult GetContacts([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var userId = User.Identity.GetUserId();
            //.Where(c => c.UserId.Equals(userId)).
            var contacts = _context.GetContacts().Select(c => new {c.ContactId, c.Name, c.Email, c.City, c.UserId}).ToList().FindAll(c => c.UserId.Contains(userId));

            // Apply filters
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();

                contacts = contacts.Where(c => c.Name.Contains(value) || c.City.Contains(value) || c.Email.Contains(value)).ToList();

            }

            var filteredCount = contacts.Count;

            // Sort
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = string.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != string.Empty ? "," : "";
                orderByString += (column.Data == "Name" ? "Name" : column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            contacts = contacts.OrderBy(orderByString == string.Empty ? "Name asc" : orderByString).ToList();

            // Paging
            contacts = contacts.Skip(requestModel.Start).Take(requestModel.Length).ToList();



            return Json(new DataTablesResponse(requestModel.Draw, contacts, filteredCount, contacts.Count), JsonRequestBehavior.AllowGet);

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

    }
}
