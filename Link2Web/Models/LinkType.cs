using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Link2Web.Models
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

        public ICollection<Link> Links { get; set; }
    }
}