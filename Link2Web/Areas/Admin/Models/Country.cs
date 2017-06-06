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
    }
}