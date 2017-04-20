using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Link2Web.Models
{
    public class EditProfileViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password",
            ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

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
        public Country CountryId { get; set; }

        [Display(Name = "Phone", ResourceType = typeof (Resources.Resources))]
        public string Phone { get; set; }

        [Display(Name = "ProfilePicture", ResourceType = typeof (Resources.Resources))]
        public byte[] ProfilePicture { get; set; }


        public virtual Country Country { get; set; }
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}