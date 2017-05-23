using Google.Apis.Analytics.v3;
using Google.Apis.Analytics.v3.Data;
using Link2Web.Core;
using Link2Web.Helpers;
using Link2Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Link2Web.BLL
{
    public class GoogleAnalytics
    {
        public AnalyticDataPoint GetAnalyticsData(string profileId, string[] dimensions, string[] metrics, DateTime startDate, DateTime endDate)
        {
            AnalyticDataPoint data = new AnalyticDataPoint();
            if (!profileId.Contains("ga:"))
                profileId = string.Format("ga:{0}", profileId);

            //Make initial call to service.
            //Then check if a next link exists in the response,
            //if so parse and call again using start index param.
            GaData response = null;
            do
            {
                int startIndex = 1;
                if (response != null && !string.IsNullOrEmpty(response.NextLink))
                {
                    Uri uri = new Uri(response.NextLink);
                    var paramerters = uri.Query.Split('&');
                    string s = paramerters.First(i => i.Contains("start-index")).Split('=')[1];
                    startIndex = int.Parse(s);
                }

                var request = BuildAnalyticRequest(profileId, dimensions, metrics, startDate, endDate, startIndex);
                response = request.Execute();
                data.ColumnHeaders = response.ColumnHeaders.ToList();

                foreach (var row in response.Rows)
                {
                    //var datum = MyFunctions.StringToDateTime(row[0], "yyyMMdd");
                    var bounceRate = MyFunctions.GetDouble(row[2], 0);
                    bounceRate = Math.Round(bounceRate, 2);
 
                    var rowData = new AnalyticsData
                    {
                        Dimension = row[0],
                        Users = row[1],
                        //Clicks = row[2],
                        BounceRate = bounceRate,
                        Pageviews = row[3],
                        OrganicSearches = row[4],
                        PageLoadTime = row[5],
                        //Impressions = row[6],
                        PercentNewSessions = row[6],
                        AvgTimeOnPage = row[7]
                        //Date        = datum


                    };

                    data.Rows.Add(rowData);
                }

            }
            while (!string.IsNullOrEmpty(response.NextLink));

            return data;
        }

        private DataResource.GaResource.GetRequest BuildAnalyticRequest(string profileId, string[] dimensions, string[] metrics,
                                                                            DateTime startDate, DateTime endDate, int startIndex)
        {
            var service = new GlobalSettings().GetAnalyticsService();
            DataResource.GaResource.GetRequest request = service.Data.Ga.Get(profileId, startDate.ToString("yyyy-MM-dd"),
                endDate.ToString("yyyy-MM-dd"), string.Join(",", metrics));
            request.Dimensions = string.Join(",", dimensions);
            request.StartIndex = startIndex;
            return request;
        }

        public List<Profile> GetAvailableProfiles()
        {

            var service = new GlobalSettings().GetAnalyticsService();  
            var response = service.Management.Profiles.List("~all", "~all").Execute();
            return response.Items.ToList();
        }

        public class AnalyticDataPoint
        {
            public AnalyticDataPoint()
            {
                Rows = new List<AnalyticsData>();
            }

            public List<GaData.ColumnHeadersData> ColumnHeaders { get; set; }
            public List<AnalyticsData> Rows { get; set; }
        }

        /// <summary>
        /// Get the GoogleAnalytics visitors from a specific datetime
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dimensions"></param>
        /// <returns>Return a List from Google Analytics with raw data</returns>
        public AnalyticDataPoint GetVisitorsData(DateTime startDate, DateTime endDate, string[] dimensions, string[] metrics)
        {
            var analyticsData = new GoogleAnalytics();

            return analyticsData.GetAnalyticsData(HttpContext.Current.Session["ViewProjectId"].ToString(), dimensions, metrics, startDate, endDate);
        }

    }
}