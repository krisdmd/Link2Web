using System.ComponentModel.DataAnnotations;

namespace Link2Web.DAL.Entities
{
    public class GoogleUser
    {
        public string Username { get; set; }

        [MaxLength(500)]
        public string RefreshToken { get; set; }

        [Key]
        [MaxLength(500)]
        public string UserId { get; set; }
    }
}