using System;

namespace Link2Web.Models
{
    public class FacebookPost
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public string Story { get; set; }
        public string Picture { get; set; }
        public string Caption { get; set; }
        public string PermalinkUrl { get; set; }
        public string From { get; set; }
        public string Place { get; set; }
        public string Title { get; set; }


        public DateTime Createdtime { get; set; }
    }

}