using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Link2Web.Models
{

    public class Country
    {
        public int CountryId { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        public string Name { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        public string Code { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
        public virtual ICollection<EditProfileViewModel> EditProfileViewModels { get; set; }
    }
}