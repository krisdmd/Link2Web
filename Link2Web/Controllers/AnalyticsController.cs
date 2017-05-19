﻿using DataTables.Mvc;
using Google.Apis.Analytics.v3;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Services;
using Link2Web.BLL;
using Link2Web.Core;
using Link2Web.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Link2Web.Controllers
{
    public class AnalyticsController : BaseController
    {
        public ActionResult Index()
        {
            if (GlobalSettings.AnalyticsService == null)
            {
                return RedirectToAction("IndexAsync");
            }

            return View();
        }

        public ActionResult VisitorsByTopReferer()
        {
            if (GlobalSettings.AnalyticsService == null)
            {
                TempData["LastController"] = "Analytics";
                TempData["LastAction"] = "VisitorsByTopReferer";
                return RedirectToAction("IndexAsync");
            }

            return View();
        }

        public ActionResult VisitorsByKeyword()
        {
            if (GlobalSettings.AnalyticsService == null)
            {
                TempData["LastController"] = "Analytics";
                TempData["LastAction"] = "VisitorsByKeyword";
                return RedirectToAction("IndexAsync");
            }

            return View();
        }

        public ActionResult VisitorsByBrowser()
        {

            if (GlobalSettings.AnalyticsService == null)
            {
                TempData["LastController"] = "Analytics";
                TempData["LastAction"] = "VisitorsByBrowser";
                return RedirectToAction("IndexAsync");
            }

            return View();
        }

        public ActionResult GetVisitors([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
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
            var data = analyticsData.GetVisitorsData(DateTime.Now.AddDays(-180), DateTime.Now, dimensions, metrics).Rows;

            // Apply filters
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                data = data.Where(a => a.Dimension.Contains(value) || a.OrganicSearches.Contains(value) || a.Users.Contains(value)).ToList();
            }

            var filteredCount = data.Count;

            // Sort
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = string.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != string.Empty ? "," : "";
                orderByString += (column.Data == "Dimension" ? "Dimension" : column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            data = data.OrderBy(orderByString == string.Empty ? "dimension asc" : orderByString).ToList();

            // Paging
            data = data.Skip(requestModel.Start).Take(requestModel.Length).ToList();


            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, data.Count), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVisitorsByKeyword([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
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
            var data = analyticsData.GetVisitorsData(DateTime.Now.AddDays(-180), DateTime.Now, dimensions, metrics).Rows;

            // Apply filters
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                data = data.Where(a => a.Dimension.Contains(value) || a.OrganicSearches.Contains(value) || a.Users.Contains(value)).ToList();
            }

            var filteredCount = data.Count;

            // Sort
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = string.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != string.Empty ? "," : "";
                orderByString += (column.Data == "Dimension" ? "Dimension" : column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            data = data.OrderBy(orderByString == string.Empty ? "dimension asc" : orderByString).ToList();

            // Paging
            data = data.Skip(requestModel.Start).Take(requestModel.Length).ToList();



            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, data.Count), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVisitorsByTopReferer([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
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
            var data = analyticsData.GetVisitorsData(DateTime.Now.AddDays(-180), DateTime.Now, dimensions, metrics).Rows;

            // Apply filters
            if (requestModel.Search.Value != String.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                data = data.Where(a => a.Dimension.Contains(value) || a.OrganicSearches.Contains(value) || a.Users.Contains(value)).ToList();
            }

            var filteredCount = data.Count;

            // Sort
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = string.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != string.Empty ? "," : "";
                orderByString += (column.Data == "Dimension" ? "Dimension" : column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            data = data.OrderBy(orderByString == string.Empty ? "dimension asc" : orderByString).ToList();

            // Paging
            data = data.Skip(requestModel.Start).Take(requestModel.Length).ToList();



            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, data.Count), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVisitorsByBrowser([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
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
            var data = analyticsData.GetVisitorsData(DateTime.Now.AddDays(-180), DateTime.Now, dimensions, metrics).Rows;

            // Apply filters
            if (requestModel.Search.Value != String.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                data = data.Where(a => a.Dimension.Contains(value) || a.OrganicSearches.Contains(value) || a.Users.Contains(value)).ToList();
            }

            var filteredCount = data.Count;

            // Sort
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = string.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != string.Empty ? "," : "";
                orderByString += (column.Data == "Dimension" ? "Dimension" : column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            data = data.OrderBy(orderByString == string.Empty ? "dimension asc" : orderByString).ToList();

            // Paging
            data = data.Skip(requestModel.Start).Take(requestModel.Length).ToList();



            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, data.Count), JsonRequestBehavior.AllowGet);

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

                GlobalSettings.AnalyticsService = service;

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
