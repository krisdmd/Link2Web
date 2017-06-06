using Link2Web.Areas.Admin.Models;
using Link2Web.DAL;
using Link2Web.DAL.Repositories;
using System.Net;
using System.Web.Mvc;

namespace Link2Web.Areas.Admin.Controllers
{
    public class CurrenciesController : Link2Web.Controllers.BaseController
    {
        private ICurrencyRepository _context;

        public CurrenciesController()
        {
            _context = new CurrencyRepository(new Link2WebDbContext());
        }

        public CurrenciesController(ICurrencyRepository currencyRepository)
        {
            _context = currencyRepository;
        }

        // GET: Admin/Currencies
        public ActionResult Index()
        {
            return View(_context.GetCurrencies());
        }

        // GET: Admin/Currencies/Details/5
        public ActionResult Details(int id)
        {
            Currency currency = _context.GetCurrencyById(id);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        // GET: Admin/Currencies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Currencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CurrencyId,Code,Name,Symbol")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                _context.InsertCurrency(currency);
                _context.Save();
                return RedirectToAction("Index");
            }

            return View(currency);
        }

        // GET: Admin/Currencies/Edit/5
        public ActionResult Edit(int id)
        {
            Currency currency = _context.GetCurrencyById(id);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        // POST: Admin/Currencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CurrencyId,Code,Name,Symbol")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                _context.UpdateCurrency(currency);
                _context.Save();
                return RedirectToAction("Index");
            }
            return View(currency);
        }

        // GET: Admin/Currencies/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = _context.GetCurrencyById(id);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        // POST: Admin/Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _context.DeleteCurrency(id);
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
