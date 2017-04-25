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

        [Display(Name = "ViewProfile", ResourceType = typeof(Resources.Resources))]
        [Required]
        public string ViewProfileId { get; set; }

        [Display(Name = "DisplayName", ResourceType = typeof (Resources.Resources))]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Email", ResourceType = typeof (Resources.Resources))]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Country", ResourceType = typeof (Resources.Resources))]
        public int CountryId { get; set; }

        [Display(Name = "Website", ResourceType = typeof (Resources.Resources))]
        [Required]
        public string Url { get; set; }

        [HiddenInput(DisplayValue = false)]

        [Display(Name = "PreviewImage", ResourceType = typeof (Resources.Resources))]
        public string PreviewImage { get; set; }

        [Display(Name = "Note", ResourceType = typeof (Resources.Resources))]
        public string Note { get; set; }

        [Display(Name = "Currency", ResourceType = typeof(Resources.Resources))]
        public int CurrencyId { get; set; }

        [Display(Name = "Language", ResourceType = typeof(Resources.Resources))]
        public int LanguageId { get; set; }

        [HiddenInput(DisplayValue = false)]
        [ScaffoldColumn(false)]
        public DateTime Created { get; set; }

        [HiddenInput(DisplayValue = false)]
        [ScaffoldColumn(false)]
        public DateTime Modified { get; set; }

        [DefaultValue(true)]
        [ScaffoldColumn(false)]
        [Display(Name = "Visible", ResourceType = typeof (Resources.Resources))]
        public bool Visible { get; set; }

        public virtual Country Country { get; set; }
        public virtual Currency Currency { get; set; }

        public virtual Language Language { get; set; }
        public virtual Link Link { get; set; }

        public Project()
        {
            Created = DateTime.Now;
            Modified = DateTime.Now;
            Visible = true;
        }
    }
}