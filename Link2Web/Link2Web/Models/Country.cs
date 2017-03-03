using System.Collections.ObjectModel;

namespace Link2Web.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual Collection<ContactDetails> ContactDetails { get; set; }
    }
}