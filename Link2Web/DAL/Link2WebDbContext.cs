﻿using Link2Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Link2Web.DAL
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class Link2WebDbContext : IdentityDbContext<ApplicationUser>
    {
        public Link2WebDbContext()
            : base("Link2WebContext", throwIfV1Schema: false)
        {
            Database.SetInitializer(new Link2WebInit());

        }
        
        public IDbSet<Project> Projects { get; set; }
        public IDbSet<Setting> Settings { get; set; }
        public IDbSet<SettingType> SettingTypes { get; set; }
        public IDbSet<ContactDetail> ContactDetails { get; set; }
        public IDbSet<Country> Countries { get; set; }
        public IDbSet<Currency> Currencies { get; set; }
        public IDbSet<Link> Links { get; set; }
        public IDbSet<WebsiteType> WebsiteTypes { get; set; }
        public IDbSet<LinkContact> LinkContacts { get; set; }
        public IDbSet<LinkStatus> LinkStatus { get; set; }
        public IDbSet<LinkType> LinkTypes { get; set; }
        public IDbSet<Language> Languages { get; set; }
        public IDbSet<UserSetting> UserSettings { get; set; }
        public IDbSet<CrawledLinkStatus> CrawledLinkStatuses { get; set; }

        public static Link2WebDbContext Create()
        {
            return new Link2WebDbContext();
        }
    }
}