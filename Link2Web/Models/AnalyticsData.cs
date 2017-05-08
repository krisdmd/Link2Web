using System;

namespace Link2Web.Models
{
    public class AnalyticsData
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Sessions { get; set; }
        public string NewUsers { get; set; }
        public string Pages { get; set; }
        public string AvgSessionDuration { get; set; }
        public string Impressions { get; set; }
        public string Hits { get; set; }
        public string Clicks { get; set; }
        public double BounceRate { get; set; }
        public string Keywords { get; set; }
        public string Pageviews { get; set; }
        public string Browser { get; set; }
        public string OrganicSearches { get; set; }
        public double AvgTimeOnPage { get; set; }
        public string FullReferer { get; set; }
    }
}