using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Link2Web.Models
{
    public class AnalyticsAllTraffic
    {
        public int AnalyticsAllTrafficId { get; set; }
        public double Sessions { get; set; }
        public double NewUsers { get; set; }
        public double Pages { get; set; }
        public double AvgSessionDuration { get; set; }
    }
}