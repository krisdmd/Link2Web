using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Link2Web.Models
{
    public class Link
    {
        public int LinkId { get; set; }

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

        [Display(Name = "CreatedOn", ResourceType = typeof (Resources.Resources))]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "BacklinkFound", ResourceType = typeof (Resources.Resources))]
        [DefaultValue(false)]
        public bool BacklinkFound { get; set; }

        [Display(Name = "Status", ResourceType = typeof (Resources.Resources))]
        public LinkStatus StatusId { get; set; }

        [Display(Name = "Type", ResourceType = typeof (Resources.Resources))]
        public LinkType LinkTypeId { get; set; }

        [Display(Name = "Contact", ResourceType = typeof (Resources.Resources))]
        public Contact ContactId { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Key, ForeignKey("Project")]
        public int ProjectId { get; set; }

        [Display(Name = "Status", ResourceType = typeof (Resources.Resources))]
        public virtual LinkStatus Status { get; set; }

        public virtual LinkType LinkType { get; set; }
        public virtual Contact Contact { get; set; }

        public virtual Project Project { get; set; }

        public Link()
        {
            CreatedOn = DateTime.Now;
        }
    }
}