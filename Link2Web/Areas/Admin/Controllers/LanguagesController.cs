using Link2Web.Areas.Admin.Models;
using Link2Web.Controllers;
using Link2Web.DAL;
using Link2Web.DAL.Repositories;
using System.Web.Mvc;

namespace Link2Web.Areas.Admin.Controllers
{
    public class LanguagesController : BaseController
    {
        private ILanguageRepository _context;

        public LanguagesController()
        {
            _context = new LanguageRepository(new Link2WebDbContext());
        }

        // GET: Admin/Languages
        public ActionResult Index()
        {
            return View(_context.GetLanguages());
        }

        // GET: Admin/Languages/Details/5
        public ActionResult Details(int id)
        {
            Language language = _context.GetLanguageById(id);

            if (language == null)
            {
                return HttpNotFound();
            }

            return View(language);
        }

        // GET: Admin/Languages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Languages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LanguageId,Name,Code")] Language language)
        {
            if (ModelState.IsValid)
            {
                _context.InsertLanguage(language);
                _context.Save();
                return RedirectToAction("Index");
            }

            return View(language);
        }

        // GET: Admin/Languages/Edit/5
        public ActionResult Edit(int id)
        {
            Language language = _context.GetLanguageById(id);

            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        // POST: Admin/Languages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LanguageId,Name,Code")] Language language)
        {
            if (ModelState.IsValid)
            {
                _context.UpdateLanguage(language);
                _context.Save();
                return RedirectToAction("Index");
            }
            return View(language);
        }

        // GET: Admin/Languages/Delete/5
        public ActionResult Delete(int id)
        {
            Language language = _context.GetLanguageById(id);

            if (language == null)
            {
                return HttpNotFound();
            }

            return View(language);
        }

        // POST: Admin/Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _context.GetLanguageById(id);
            _context.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
