namespace Link2Web.Models
{
    public class CrawledLink
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }
    }
}