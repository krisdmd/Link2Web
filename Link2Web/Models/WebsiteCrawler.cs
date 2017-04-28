using System.Collections.Generic;

namespace Link2Web.Models
{
    public class WebsiteCrawler
    {
        public List<CrawledLink> GoodLinks { get; set; }

        public List<CrawledLink> BadLinks { get; set; }

        public List<CrawledLink> WarningLinks { get; set; }
    }
}