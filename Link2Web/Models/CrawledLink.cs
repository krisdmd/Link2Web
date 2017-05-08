using System.ComponentModel.DataAnnotations;

namespace Link2Web.Models
{
    public class CrawledLink
    {
        public int CrawledLinkId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
          ErrorMessageResourceName = "IsRequired")]
        [Display(Name = "WebsiteUrl", ResourceType = typeof(Resources.Resources))]
        public string Url { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        [Display(Name = "Title", ResourceType = typeof(Resources.Resources))]
        public string Title { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "IsRequired")]
        [Display(Name = "Status", ResourceType = typeof(Resources.Resources))]
        public string Status { get; set; }

        [Display(Name = "Total", ResourceType = typeof(Resources.Resources))]
        public int Total { get; set; }

        public virtual CrawledLinkStatus CrawledLinkStatus { get; set; }
    }
}