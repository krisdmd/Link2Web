using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Link2Web.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public ApplicationUser UserId { get; set; }
        [Display(Name = "DisplayName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Display(Name = "Email", ResourceType = typeof(Resources.Resources))]

        public string Email { get; set; }
        [Display(Name = "Country", ResourceType = typeof(Resources.Resources))]
        public int CountryId { get; set; }
        //public TimeZoneInfo TimeZone { get; set; }
        [Display(Name = "Website", ResourceType = typeof(Resources.Resources))]
        public string Url { get; set; }
        [Display(Name = "PreviewImage", ResourceType = typeof(Resources.Resources))]

        [DisplayName("Preview image")]
        public string PreviewImage { get; set; }
        [Display(Name = "Note", ResourceType = typeof(Resources.Resources))]
        public string Note { get; set; }
        [HiddenInput(DisplayValue = false)]
        [ScaffoldColumn(false)]
        public DateTime Created { get; set; }
        [HiddenInput(DisplayValue = false)]
        [ScaffoldColumn(false)]
        public DateTime Modified { get; set; }
        [DefaultValue(true)]
        [ScaffoldColumn(false)]
        [Display(Name = "Visible", ResourceType = typeof(Resources.Resources))]
        public Boolean Visible { get; set; }

        public virtual Country Country { get; set; }
        public virtual Currency Currency { get; set; }
    }
}