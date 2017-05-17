using Link2Web.Controllers;
using Link2Web.DAL;
using Link2Web.DAL.Repositories;
using Link2Web.Models;
using System.Web.Mvc;

namespace Link2Web.Areas.Admin.Controllers
{
    public class CountriesController : BaseController
    {
        private ICountryRepository _context;

        public CountriesController()
        {
            _context = new CountryRepository(new Link2WebDbContext());
        }

        public CountriesController(ICountryRepository countryRepository)
        {
            _context = countryRepository;
        }


        // GET: Country
        public ActionResult Index()
        {
            return View(_context.GetCountries());
        }

        // GET: Admin/Countries/Details/5
        public ActionResult Details(int id)
        {
            Country country = _context.GetCountryById(id);

            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: Admin/Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryId,Name,Code")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.InsertCountry(country);
                _context.Save();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        // GET: Admin/Countries/Edit/5
        public ActionResult Edit(int id)
        {
            Country country = _context.GetCountryById(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Admin/Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryId,Name,Code")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.UpdateCountry(country);
                _context.Save();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: Admin/Countries/Delete/5
        public ActionResult Delete(int id)
        {
            Country country = _context.GetCountryById(id);

            if (country == null)
            {
                return HttpNotFound();
            }

            return View(country);
        }

        // POST: Admin/Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _context.DeleteCountry(id);
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
