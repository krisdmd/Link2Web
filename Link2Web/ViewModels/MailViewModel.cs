using System.ComponentModel.DataAnnotations;

namespace Link2Web.ViewModels
{
    public class MailViewModel
    {
        [Required]
        [Display(Name = "ToEmail", ResourceType = typeof(Resources.Resources))]
        public string ToEmail { get; set; }
        [Required]
        [Display(Name = "Subject", ResourceType = typeof(Resources.Resources))]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "Message", ResourceType = typeof(Resources.Resources))]
        public string Body { get; set; }
    }
}