using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Link2Web.Areas.Admin.Models
{
    public class LinkType
    {
        [ReadOnly(true)]
        public int LinkTypeId { get; set; }
        [Display(Name = "Type", ResourceType = typeof(Resources.Resources))]
        [Required]
        public string Type { get; set; }
        [Display(Name = "Visible", ResourceType = typeof(Resources.Resources))]
        [DefaultValue(true)]
        public bool Visible { get; set; }
    }
}