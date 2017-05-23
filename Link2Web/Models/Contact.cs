using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Link2Web.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required]
        [Display(Name = "Country", ResourceType = typeof (Resources.Resources))]
        public int CountryId { get; set; }

        public ApplicationUser UserId { get; set; }

        [Display(Name = "Name", ResourceType = typeof (Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        public string Name { get; set; }

        public string ScreenName { get; set; }

        [Display(Name = "Email", ResourceType = typeof (Resources.Resources))]
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = @"Email is not valid")]
        public string Email { get; set; }

        [Display(Name = "Address", ResourceType = typeof (Resources.Resources))]
        public string Address { get; set; }

        [Display(Name = "City", ResourceType = typeof (Resources.Resources))]
        public string City { get; set; }

        [Display(Name = "ZipCode", ResourceType = typeof (Resources.Resources))]
        public string Zipcode { get; set; }

        [Display(Name = "Phone", ResourceType = typeof (Resources.Resources))]
        public string Phone { get; set; }

        [Display(Name = "TwitterProfileUrl", ResourceType = typeof(Resources.Resources))]
        public string TwitterProfileUrl { get; set; }

        [Display(Name = "FacebookProfileUrl", ResourceType = typeof(Resources.Resources))]
        public string FacebookProfileUrl { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.Resources))]
        [DefaultValue(true)]
        public bool Active { get; set; }

        public virtual Country Country { get; set; }
        public virtual Link Links { get; set; }
    }
}