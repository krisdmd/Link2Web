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
        public ActionResult Index()
        {
            if (Settings.AnalyticsService == null)
            {
                return RedirectToAction("IndexAsync");
            }

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

                Settings.AnalyticsService = service;

                return RedirectToAction("Index", "Analytics");
            }
            return new RedirectResult(result.RedirectUri);
        }

    }

    public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {
        protected override Google.Apis.Auth.OAuth2.Mvc.FlowMetadata FlowData => new AppFlowMetadata();
    }
}
