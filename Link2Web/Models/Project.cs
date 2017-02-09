using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Link2Web.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public ApplicationUser UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        //public TimeZoneInfo TimeZone { get; set; }
        public string Url { get; set; }
        [DisplayName("Preview image")]
        public string PreviewImage { get; set; }
        public string Note { get; set; }
        [HiddenInput(DisplayValue = false)]
        [ScaffoldColumn(false)]
        public DateTime Created { get; set; }
        [HiddenInput(DisplayValue = false)]
        [ScaffoldColumn(false)]
        public DateTime Modified { get; set; }
        [DefaultValue(true)]
        [ScaffoldColumn(false)]
        public Boolean Visible { get; set; }

        public virtual Country Country { get; set; } 
    }
}