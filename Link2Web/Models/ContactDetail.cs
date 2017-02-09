using System;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace Link2Web.Models
{
    public class ContactDetail
    {
        public int ContactDetailId { get; set; }
        public int CountryId { get; set; }
        public ApplicationUser UserId { get; set; }
        public string Name { get; set; }
        public string ScreenName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }

        public virtual Country Country { get; set; }

    }
}