using Google.Apis.Analytics.v3;
using Link2Web.BLL;
using System;
using System.Web.Mvc;

namespace Link2Web.Controllers
{
    public class AnalyticsController : Controller
    {
        private AnalyticsService _analyticsService { get; set; }


        public ActionResult Visitors()
        {
            var analyticsData = new GoogleAnalytics();
            var data = analyticsData.GetVisitorsByDate(DateTime.Now.AddDays(-180), DateTime.Now);


            //data.ColumnHeaders.FirstOrDefault().Name

            return View();
        }


        // GET: Analytics
        public ActionResult Index()
        {
            return View();
        }

        // GET: Analytics/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Analytics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Analytics/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Analytics/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Analytics/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Analytics/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Analytics/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
