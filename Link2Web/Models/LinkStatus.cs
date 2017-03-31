using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Link2Web.Models
{
    public class LinkStatus
    {
        public int LinkStatusId { get; set; }

        [Display(Name = "Status", ResourceType = typeof (Resources.Resources))]
        public string Status { get; set; }

        [Display(Name = "Visible", ResourceType = typeof (Resources.Resources))]
        public bool Visible { get; set; }

        public ICollection<Link> Links { get; set; }
    }   
}