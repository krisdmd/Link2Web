using Link2Web.Models;
using System.Collections.Generic;

namespace Link2Web.ViewModels
{
    public class AnalyticsViewModel
    {
        public int Id { get; set; }
        public AnalyticsData AnalyticsData { get; set; }
        public List<AnalyticsData> LstAnalyticsData { get; set; }
    }
}