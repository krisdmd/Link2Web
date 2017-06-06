using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Link2Web.Areas.Admin.Models
{
    public class LinkStatus
    {
        public int LinkStatusId { get; set; }

        [Display(Name = "Status", ResourceType = typeof (Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        public string Status { get; set; }

        [Display(Name = "Visible", ResourceType = typeof (Resources.Resources))]
        [DefaultValue(true)]
        public bool Visible { get; set; }
    }   
}