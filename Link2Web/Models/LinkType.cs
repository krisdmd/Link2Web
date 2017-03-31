﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Link2Web.Models
{
    public class LinkType
    {
        public int LinkTypeId { get; set; }
        [Display(Name = "Type", ResourceType = typeof(Resources.Resources))]
        public string Type { get; set; }
        [Display(Name = "Visible", ResourceType = typeof(Resources.Resources))]
        public bool Visible { get; set; }

        public ICollection<Link> Links { get; set; }
    }
}