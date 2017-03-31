using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Link2Web.Models
{
    public class LinkContact
    {
        public int LinkContactId { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof (Resources.Resources))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof (Resources.Resources))]
        public string LastName { get; set; }

        [Display(Name = "Email", ResourceType = typeof (Resources.Resources))]
        public string Email { get; set; }

        [Display(Name = "TwitterProfileUrl", ResourceType = typeof (Resources.Resources))]
        public string TwitterProfileUrl { get; set; }

        [Display(Name = "FacebookProfileUrl", ResourceType = typeof (Resources.Resources))]
        public string FacebookProfileUrl { get; set; }

        [Display(Name = "Active", ResourceType = typeof (Resources.Resources))]
        [DefaultValue(true)]
        public bool Active { get; set; }

        public ICollection<Link> Links { get; set; }
    }
}