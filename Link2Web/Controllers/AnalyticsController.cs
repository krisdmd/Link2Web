using Google.Apis.Analytics.v3;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Link2Web.BLL;
using Link2Web.Core;
using Link2Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Link2Web.ViewModels;

namespace Link2Web.Controllers
{
    public class AnalyticsController : BaseController
    {
        private AnalyticsService _analyticsService { get; set; }


        public ActionResult Visitors()
        {
            var analyticsData = new GoogleAnalytics();
            var data = analyticsData.GetVisitorsByDate(DateTime.Now.AddDays(-180), DateTime.Now);
            var vm = new AnalyticsViewModel { LstAnalyticsData = data.Rows };

            return View(vm);
        }

        public ActionResult GetVisitors([DataSourceRequest]DataSourceRequest request)
        {
            var analyticsData = new GoogleAnalytics();
            var data = analyticsData.GetVisitorsByDate(DateTime.Now.AddDays(-180), DateTime.Now);

            IEnumerable<AnalyticsData> d = data.Rows;
            var total = d.Count();


            //var vm = new AnalyticsViewModel { AnalyticsData = data.Rows };

            var result = new DataSourceResult()
            {
                Data = d,
                Total = total
            };

            return Json(result, JsonRequestBehavior.AllowGet);

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

                return RedirectToAction("Index", "Analytics");
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

        public async Task<ActionResult> IndexAsync(CancellationToken cancellationToken)
        {
            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata()).
                AuthorizeAsync(cancellationToken);

            if (result.Credential != null)
            {
                var service = new AnalyticsService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = result.Credential,
                    ApplicationName = "ASP.NET MVC Sample"
                });

                _analyticsService = service;
                Settings.AnalyticsService = service;

                return RedirectToAction("Index", "Analytics");
            }
            else
            {
                return new RedirectResult(result.RedirectUri);
            }

        }

    }

    public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {
        protected override Google.Apis.Auth.OAuth2.Mvc.FlowMetadata FlowData
        {
            get { return new AppFlowMetadata(); }
        }
    }
}
