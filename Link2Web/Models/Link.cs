﻿using Link2Web.Areas.Admin.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Link2Web.Models
{
    public class Link
    {

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required]
     
        public int LinkId { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Resources.Resources))]
        [HiddenInput(DisplayValue = false)]

        public LinkStatus StatusId { get; set; }
        [HiddenInput(DisplayValue = false)]

        [Required]

        [Display(Name = "LinkType", ResourceType = typeof(Resources.Resources))]
        public int LinkTypeId { get; set; }

        [Display(Name = "Contact", ResourceType = typeof(Resources.Resources))]
        [HiddenInput(DisplayValue = false)]
        public int ContactId { get; set; }


        [Display(Name = "Project", ResourceType = typeof(Resources.Resources))]
        [HiddenInput(DisplayValue = false)]
        public int ProjectId { get; set; }

        [Display(Name = "WebsiteUrl", ResourceType = typeof (Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        public string WebsiteUrl { get; set; }

        [Display(Name = "AnchorText", ResourceType = typeof (Resources.Resources))]
        [MaxLength(40, ErrorMessage = @"AnchorText cannot be longer than 40 characters.")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        public string AnchorText { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        [Display(Name = "DestinationUrl", ResourceType = typeof (Resources.Resources))]
        public string DestinationUrl { get; set; }

        [Display(Name = "Description", ResourceType = typeof (Resources.Resources))]
        public string Description { get; set; }
        [HiddenInput(DisplayValue = false)]
        [ScaffoldColumn(false)]

        [Display(Name = "CreatedOn", ResourceType = typeof (Resources.Resources))]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "BacklinkFound", ResourceType = typeof (Resources.Resources))]
        [DefaultValue(false)]
        public bool BacklinkFound { get; set; }
        public virtual Country Countries { get; set; }


        public Link()
        {
            CreatedOn = DateTime.Now;
        }
    }
}