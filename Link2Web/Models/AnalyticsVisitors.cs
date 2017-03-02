namespace Link2Web.Models
{
    public class AnalyticsVisitors
    {
        public int AnalyticsVisitorsId { get; set; }
        public double Sessions { get; set; }
        public double NewUsers { get; set; }
        public double Pages { get; set; }
        public double AvgSessionDuration { get; set; }
        public int Impressions { get; set; }
        public int Hits { get; set; }
        public int Clicks { get; set; }
        public double BounceRate { get; set; }

    }
}