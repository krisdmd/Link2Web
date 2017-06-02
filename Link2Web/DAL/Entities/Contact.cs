using Link2Web.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Link2Web.DAL.Entities
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }

        [ForeignKey("Countries")]
        public int CountryId { get; set; }

        public string UserId { get; set; }

        [MaxLength(60)]
        public string Name { get; set; }

        [MaxLength(60)]
        public string ScreenName { get; set; }

        [MaxLength(60)]
        public string Email { get; set; }


        public string Address { get; set; }


        public string City { get; set; }


        public string Zipcode { get; set; }


        public string Phone { get; set; }

        [MaxLength(255)]
        public string TwitterProfileUrl { get; set; }

        [MaxLength(255)]
        public string FacebookProfileUrl { get; set; }


        [DefaultValue(true)]
        public bool Active { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}