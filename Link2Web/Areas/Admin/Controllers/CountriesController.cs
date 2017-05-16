using Link2Web.Controllers;
using Link2Web.DAL;
using Link2Web.Models;
using System.Web.Mvc;

namespace Link2Web.Areas.Admin.Controllers
{
    public class CountriesController : BaseController
    {
        private ICountryRepository db;

        public CountriesController()
        {
            db = new CountryRepository();
        }

        public CountriesController(ICountryRepository countryRepository)
        {
            db = countryRepository;
        }


        // GET: Country
        public ActionResult Index()
        {
            return View(db.GetCountries());
        }

        // GET: Admin/Countries/Details/5
        public ActionResult Details(int id)
        {
            Country country = db.GetCountryById(id);

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
                db.InsertCountry(country);
                db.Save();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        // GET: Admin/Countries/Edit/5
        public ActionResult Edit(int id)
        {
            Country country = db.GetCountryById(id);
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
                db.UpdateCountry(country);
                db.Save();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: Admin/Countries/Delete/5
        public ActionResult Delete(int id)
        {
            Country country = db.GetCountryById(id);

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
            Country country = db.GetCountryById(id);
            db.DeleteCountry(id);
            db.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
