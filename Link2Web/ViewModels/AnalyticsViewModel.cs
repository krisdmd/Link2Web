﻿using Link2Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Link2Web.ViewModels
{
    public class AnalyticsViewModel
    {
        public int Id { get; set; }
        public AnalyticsData AnalyticsData { get; set; }
        public IList<AnalyticsData> LstAnalyticsData { get; set; }
    }
}