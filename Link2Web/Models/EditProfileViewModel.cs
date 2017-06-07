using System.ComponentModel.DataAnnotations;

namespace Link2Web.Models
{
    public class EditProfileViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resources.Resources))]
        [MaxLength(60, ErrorMessage = "Email cannot be longer than 60 characters.")]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof (Resources.Resources))]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof (Resources.Resources))]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Address", ResourceType = typeof (Resources.Resources))]
        public string Address { get; set; }

        [Display(Name = "City", ResourceType = typeof (Resources.Resources))]
        public string City { get; set; }

        [Display(Name = "ZipCode", ResourceType = typeof (Resources.Resources))]
        public string Zipcode { get; set; }

        [Display(Name = "Country", ResourceType = typeof (Resources.Resources))]
        [Required]
        public int CountryId { get; set; }

        [Display(Name = "Phone", ResourceType = typeof (Resources.Resources))]
        public string Phone { get; set; }

        [Display(Name = "TwitterProfileUrl", ResourceType = typeof(Resources.Resources))]
        public string TwitterProfileUrl { get; set; }

        [Display(Name = "FacebookProfileUrl", ResourceType = typeof(Resources.Resources))]
        public string FacebookProfileUrl { get; set; }

        [Display(Name = "ProfilePicture", ResourceType = typeof (Resources.Resources))]
        public byte[] ProfilePicture { get; set; }

        public virtual Country Country { get; set; }
    }
}