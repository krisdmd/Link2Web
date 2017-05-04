using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Link2Web.Models
{
    public class CrawledLinkStatus
    {
        public int CrawledLinkStatusId { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resources.Resources))]
        public string Status { get; set; }
        [DefaultValue(true)]
        [ScaffoldColumn(false)]
        [Display(Name = "Visible", ResourceType = typeof(Resources.Resources))]
        public bool Visible { get; set; }

        public virtual ICollection<CrawledLink> CrawledLinks { get; set; }
    }
}