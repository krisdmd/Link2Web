using Google.Apis.Analytics.v3;
using Google.Apis.Analytics.v3.Data;
using Link2Web.Core;
using Link2Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Link2Web.BLL
{
    public class GoogleAnalytics
    {
        public AnalyticDataPoint GetAnalyticsData(string profileId, string[] dimensions, string[] metrics, DateTime startDate, DateTime endDate)
        {
            AnalyticDataPoint data = new AnalyticDataPoint();
            var rowData = new AnalyticsData();
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
                data.ColumnHeaders = response.ColumnHeaders;

                foreach (var row in response.Rows)
                {
                    var datum = MyFunctions.StringToDateTime(row[0], "yyyMMdd");
                    var bounceRate = MyFunctions.StringToDouble(row[3]);

                    rowData = new AnalyticsData
                    {
                        Clicks = row[2],
                        Date = datum,
                        BounceRate = bounceRate,
                        Pageviews = row[4],
                        Impressions = row[5],
                        NewUsers = row[1]
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
            DataResource.GaResource.GetRequest request = Settings.AnalyticsService.Data.Ga.Get(profileId, startDate.ToString("yyyy-MM-dd"),
                                                                                endDate.ToString("yyyy-MM-dd"), string.Join(",", metrics));
            request.Dimensions = string.Join(",", dimensions);
            request.StartIndex = startIndex;
            return request;
        }

        public IList<Profile> GetAvailableProfiles()
        {
            var response = Settings.AnalyticsService.Management.Profiles.List("~all", "~all").Execute();
            return response.Items;
        }

        public class AnalyticDataPoint
        {
            public AnalyticDataPoint()
            {
                Rows = new List<AnalyticsData>();
            }

            public IList<GaData.ColumnHeadersData> ColumnHeaders { get; set; }
            public IList<AnalyticsData> Rows { get; set; }
        }

        /// <summary>
        /// Get the GoogleAnalytics visitors from a specific datetime
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDatet"></param>
        /// <returns>Return a List from Google Analytics with raw data</returns>
        public AnalyticDataPoint GetVisitorsByDate(DateTime startDate, DateTime endDate)
        {
            AnalyticDataPoint data;
            var analyticsData = new GoogleAnalytics();
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
                "ga:impressions"

            };


            data = analyticsData.GetAnalyticsData("ga:136022774", dimensions, metrics, startDate, endDate);

            return data;
        }

    }
}