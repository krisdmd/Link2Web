using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Link2Web.Models
{
    public class AnalyticsChannels
    {
        public int AnalyticsChannelsId { get; set; }
        public string Name { get; set; }
        [DefaultValue(true)]
        [ScaffoldColumn(false)]
        public bool Visible { get; set; }

        public ICollection<AnalyticsVisitors> AnalyticsVisitors { get; set; }
    }
}