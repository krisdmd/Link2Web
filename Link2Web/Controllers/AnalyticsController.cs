using Google.Apis.Analytics.v3;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Services;
using Link2Web.BLL;
using Link2Web.Core;
using Link2Web.Models;
using Link2Web.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Link2Web.Controllers
{
    public class AnalyticsController : BaseController
    {
        private AnalyticsService _analyticsService { get; set; }


        public ActionResult Visitors()
        {
            var analyticsData = new GoogleAnalytics();
            var data = analyticsData.GetVisitorsByDate(DateTime.Now.AddDays(-180), DateTime.Now);

            var analyticsVisitor = new AnalyticsVisitors();
            var vm = new AnalyticsViewModel();

            foreach (var row in data.Rows)
            {
                var visitor = new AnalyticsVisitors
                {
                    Hits = int.Parse(row[0])
                }
                vm.AnalyticsVisitors.Add(visitor);
            }


            foreach (var column in data.ColumnHeaders)
            {
                  var c = column;

//                var visitor = new AnalyticsVisitors
//                {
//                    Date = (string) row
//                };

//                foreach (var c in row)
//                {
//                    var xx = row;
//                }
                //analyticsVisitor.Clicks = 
            }

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
