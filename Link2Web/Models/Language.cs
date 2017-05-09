using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Link2Web.Models
{
    public class Language
    {
        public int LanguageId { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        public string Name { get; set; }

        [Display(Name = "Code", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        public string Code { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Default", ResourceType = typeof(Resources.Resources))]
        public bool Default { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}