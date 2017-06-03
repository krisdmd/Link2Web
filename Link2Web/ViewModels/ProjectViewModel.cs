using Link2Web.Areas.Admin.Models;
using Link2Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Link2Web.ViewModels
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }
        public ApplicationUser UserId { get; set; }

        [Display(Name = "DisplayName", ResourceType = typeof(Resources.Resources))]
        [Required]
        public string ViewProfileId { get; set; }

        [Display(Name = "ViewProfile", ResourceType = typeof(Resources.Resources))]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resources.Resources))]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Resources.Resources))]
        public int CountryId { get; set; }

        [Display(Name = "Website", ResourceType = typeof(Resources.Resources))]
        [Required]
        public string Url { get; set; }

        [Display(Name = "Note", ResourceType = typeof(Resources.Resources))]
        public string Note { get; set; }

        [Display(Name = "Currency", ResourceType = typeof(Resources.Resources))]
        public int CurrencyId { get; set; }

        [Display(Name = "Language", ResourceType = typeof(Resources.Resources))]
        public int LanguageId { get; set; }

        public virtual Country Country { get; set; }
        //public virtual CurrencyModel CurrencyModel { get; set; }
        public virtual Language Language { get; set; }
        public virtual Link Link { get; set; }
    }
}
