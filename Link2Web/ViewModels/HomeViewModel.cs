using Link2Web.Models;
using System.Collections.Generic;

namespace Link2Web.ViewModels
{
    public class HomeViewModel
    {
        public bool fbConnected { get; set; }
        public bool AnalyticsConnected { get; set; }
        public List<FacebookPost> FacebookPosts { get; set; }
        public List<AnalyticsData> AnalyticsData { get; set; }


    }
}