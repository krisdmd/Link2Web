using System.ComponentModel.DataAnnotations;

namespace Link2Web.Models
{
    public class ContactDetails
    {
        public int Id { get; set; }
        public ApplicationUser UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string ScreenName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public Country Country { get; set; }

    }
}