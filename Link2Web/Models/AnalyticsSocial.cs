using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Link2Web.Models
{
    public class AnalyticsSocial
    {
        public int AnalyitcsSocialId { get; set; }
        public AnalyticsSocialNetwork  AnalyticsSocialNetwork { get; set; }
        public double GoalValue { get; set; }
    }
}