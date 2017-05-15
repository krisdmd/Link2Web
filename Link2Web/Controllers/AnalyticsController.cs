using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using Google.Apis.Analytics.v3;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Services;
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

            return View();
        }

        public ActionResult VisitorsByTopReferer()
        {
            if (Settings.AnalyticsService == null)
            {
                TempData["LastController"] = "Analytics";
                TempData["LastAction"] = "VisitorsByTopReferer";
                return RedirectToAction("IndexAsync");
            }

            return View();
        }

        public ActionResult VisitorsByKeyword()
        {
            if (Settings.AnalyticsService == null)
            {
                TempData["LastController"] = "Analytics";
                TempData["LastAction"] = "VisitorsByKeyword";
                return RedirectToAction("IndexAsync");
            }

            return View();
        }

        public ActionResult VisitorsByBrowser()
        {

            if (Settings.AnalyticsService == null)
            {
                TempData["LastController"] = "Analytics";
                TempData["LastAction"] = "VisitorsByBrowser";
                return RedirectToAction("IndexAsync");
            }

            return View();
        }

        public ActionResult GetVisitors()
        {
            var dimensions = new[]
            {
                "ga:date"
            };

            var metrics = new[]
            {
                "ga:users",
                "ga:adClicks",
                "ga:bounceRate",
                "ga:pageviews",
                "ga:organicSearches",
                "ga:impressions",
                "ga:percentNewSessions",
                "ga:avgTimeOnPage"
            };

            var analyticsData = new GoogleAnalytics();
            var data = analyticsData.GetVisitorsData(DateTime.Now.AddDays(-180), DateTime.Now, dimensions, metrics);

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

        public ActionResult GetVisitorsByKeyword()
        {
            var dimensions = new[]
            {
                "ga:keyword"
            };

            var metrics = new[]
            {
                "ga:users",
                "ga:adClicks",
                "ga:bounceRate",
                "ga:pageviews",
                "ga:organicSearches",
                "ga:impressions",
                "ga:percentNewSessions",
                "ga:avgTimeOnPage"
            };

            var analyticsData = new GoogleAnalytics();
            var data = analyticsData.GetVisitorsData(DateTime.Now.AddDays(-180), DateTime.Now, dimensions, metrics);

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

        public ActionResult GetVisitorsByTopReferer()
        {
            var dimensions = new[]
            {
                "ga:fullReferrer"
            };

            var metrics = new[]
            {
                "ga:users",
                "ga:adClicks",
                "ga:bounceRate",
                "ga:pageviews",
                "ga:organicSearches",
                "ga:impressions",
                "ga:percentNewSessions",
                "ga:avgTimeOnPage"
            };

            var analyticsData = new GoogleAnalytics();
            var data = analyticsData.GetVisitorsData(DateTime.Now.AddDays(-180), DateTime.Now, dimensions, metrics);

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

        public JsonResult GetVisitorsByBrowser(IDataTablesRequest request)
        {
//            JsonResult result = new JsonResult();

//            //var search = Request.Form.GetValues("search[value]")?[0];
//            var draw = Request.Form.GetValues("draw")?[0];
//            var order = Request.Form.GetValues("order[0][column]")?[0];
//            var orderDir = Request.Form.GetValues("order[0][dir]")?[0];
//            var startRec = Convert.ToInt32(Request.Form.GetValues("start")?[0]);
//            var pageSize = Convert.ToInt32(Request.Form.GetValues("length")?[0]);
//
//            //var sortColumn = Request.Form.GetValues(name: "columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
//            var search = Request.Form.GetValues("search")?.FirstOrDefault();

            var dimensions = new[]
            {
                "ga:browser"
            };

            var metrics = new[]
            {
                "ga:users",
                "ga:pageviews",
                "ga:bounceRate",
                "ga:organicSearches",
                "ga:pageLoadTime",
                "ga:percentNewSessions",
                "ga:avgTimeOnPage"
            };

            var analyticsData = new GoogleAnalytics();
            var data = analyticsData.GetVisitorsData(DateTime.Now.AddDays(-180), DateTime.Now, dimensions, metrics);

            var d = data.Rows;


            var response = DataTablesResponse.Create(request, d.Count, 0, 0);

            if (request != null)
            {
                var filteredData = d.Where(item => item.Dimension.Contains(request.Search.Value));
                var dataPage = filteredData.Skip(request.Start).Take(request.Length);
                response = DataTablesResponse.Create(request, d.Count, filteredData.Count(), dataPage);
            }
            else
            {
                response = DataTablesResponse.Create(request, d.Count, 0, 0);
            }
        





            //            if (!string.IsNullOrEmpty(search) &&
            //                !string.IsNullOrWhiteSpace(search))
            //            {
            //                // Apply search   
            //                d = d.Where(s => s.Dimension.ToString().ToLower().Contains(search.ToLower()) ||
            //                                 s.OrganicSearches.ToLower().Contains(search.ToLower()) ||
            //                                 s.Pageviews.ToString().ToLower().Contains(search.ToLower()) ||
            //                                 s.PageLoadTime.ToString().ToLower().Contains(search.ToLower())).ToList();
            //            }

            // Sorting.   
            // d = SortByColumnWithOrder(order, orderDir, d);
            //draw = Convert.ToInt32(draw)


                        var res =
                            new
                            {
                                recordsTotal = d.Count,
                                //recordsFiltered = 5,
                                data = d.ToArray()
                            };
            
            
                        return Json(res, JsonRequestBehavior.AllowGet);

            //return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);

        }


        private List<AnalyticsData> SortByColumnWithOrder(string order, string orderDir, List<AnalyticsData> data)
        {
            // Initialization.   
            List<AnalyticsData> lst = new List<AnalyticsData>();
            try
            {
                // Sorting   
                switch (order)
                {
                    case "0":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase)
                            ? data.OrderByDescending(p => p.Dimension).ToList()
                            : data.OrderBy(p => p.Dimension).ToList();
                        break;
                    case "1":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase)
                            ? data.OrderByDescending(p => p.Users).ToList()
                            : data.OrderBy(p => p.Users).ToList();
                        break;
                    case "2":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase)
                            ? data.OrderByDescending(p => p.BounceRate).ToList()
                            : data.OrderBy(p => p.BounceRate).ToList();
                        break;
                    case "3":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase)
                            ? data.OrderByDescending(p => p.OrganicSearches).ToList()
                            : data.OrderBy(p => p.OrganicSearches).ToList();
                        break;
                    default:
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase)
                            ? data.OrderByDescending(p => p.Dimension).ToList()
                            : data.OrderBy(p => p.Dimension).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.   
                Console.Write(ex);
            }
            // info.   
            return lst;
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
                    ApplicationName = "Analytics"
                });

                Settings.AnalyticsService = service;

                return RedirectToAction(TempData["LastAction"] != null ? TempData["LastAction"].ToString() : "Index",
                    TempData["LastController"] != null ? TempData["LastController"].ToString() : "Analytics");

            }
            return new RedirectResult(result.RedirectUri);
        }
    }
    public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {
        protected override FlowMetadata FlowData => new AppFlowMetadata();
    }
}
