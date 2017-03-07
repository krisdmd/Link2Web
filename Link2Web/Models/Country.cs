using System.Collections.Generic;

namespace Link2Web.Models
{

    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<ContactDetail> ContactDetails { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}