﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Link2Web.Models
{
    public class Link
    {
        public int LinkId { get; set; }

        [Display(Name = "WebsiteUrl", ResourceType = typeof (Resources.Resources))]
        public string WebsiteUrl { get; set; }

        [Display(Name = "AnchorText", ResourceType = typeof (Resources.Resources))]
        public string AnchorText { get; set; }

        [Display(Name = "DestinationUrl", ResourceType = typeof (Resources.Resources))]
        public string DestinationUrl { get; set; }

        [Display(Name = "Description", ResourceType = typeof (Resources.Resources))]
        public string Description { get; set; }

        [Display(Name = "CreatedOn", ResourceType = typeof (Resources.Resources))]
        public DateTime CreatedOn { get; set; }

        public LinkStatus StatusId { get; set; }
        public LinkType LinkTypeId { get; set; }
        public WebsiteType WebsiteTypeId { get; set; }
        public LinkContact LinkContactId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Project ProjectId { get; set; }

        public virtual LinkStatus Status { get; set; }
        public virtual LinkType LinkType { get; set; }
        public virtual WebsiteType WebsiteType { get; set; }
        public virtual LinkContact LinkContact { get; set; }
    }
}