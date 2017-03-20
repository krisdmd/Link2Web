﻿using Google.Apis.Analytics.v3;
using Google.Apis.Analytics.v3.Data;
using Link2Web.Core;
using Link2Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Link2Web.BLL
{
    public class GoogleAnalytics
    {
        public AnalyticsService Service { get; set; }


        public AnalyticDataPoint GetAnalyticsData(string profileId, string[] dimensions, string[] metrics, DateTime startDate, DateTime endDate)
        {
            Service = Settings.AnalyticsService;
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
                    rowData = new AnalyticsData
                    {
                        Clicks = row[0],
                        BounceRate = row[1],
                        Date = row[2],
                        AvgSessionDuration = row[3]
                    };

                    data.Rows.Add(rowData);
                }

                //data.Rows.AddRange(); = response.Rows[0];
                data.Rows = new List<AnalyticsData>();


            } while (!string.IsNullOrEmpty(response.NextLink));

            return data;
        }

        private DataResource.GaResource.GetRequest BuildAnalyticRequest(string profileId, string[] dimensions, string[] metrics,
                                                                            DateTime startDate, DateTime endDate, int startIndex)
        {
            DataResource.GaResource.GetRequest request = Service.Data.Ga.Get(profileId, startDate.ToString("yyyy-MM-dd"),
                                                                                endDate.ToString("yyyy-MM-dd"), string.Join(",", metrics));
            request.Dimensions = string.Join(",", dimensions);
            request.StartIndex = startIndex;
            return request;
        }

        public IList<Profile> GetAvailableProfiles()
        {
            var response = Service.Management.Profiles.List("~all", "~all").Execute();
            return response.Items;
        }

        public class AnalyticDataPoint
        {
            public AnalyticDataPoint()
            {
                Rows = new List<AnalyticsData>();
            }

            public IList<GaData.ColumnHeadersData> ColumnHeaders { get; set; }
            public List<AnalyticsData> Rows { get; set; }
        }

        /// <summary>
        /// Get the GoogleAnalytics visitors from a specific datetime
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDatet"></param>
        /// <returns>Return a List from Google Analytics with raw data</returns>
        public AnalyticDataPoint GetVisitorsByDate(DateTime startDate, DateTime endDate)
        {
            Service = Settings.AnalyticsService;
            var data = new AnalyticDataPoint();
            var analyticsData = new GoogleAnalytics();
            var dimensions = new[]
            {
                "ga:date"
            };
            var metrics = new[]
            {
                "ga:users",
                "ga:adClicks",
                "ga:bounceRate"
            };


            if (Service != null)
            {
                analyticsData.Service = Service;
                data = analyticsData.GetAnalyticsData("ga:136022774", dimensions, metrics, startDate, endDate);
            }

            return data;
        }
    }
}