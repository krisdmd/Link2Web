using Link2Web.DAL.Entities;
using Link2Web.Models;
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
        
        public IDbSet<Models.Project> Projects { get; set; }
        public IDbSet<Models.Setting> Settings { get; set; }
        public IDbSet<Models.SettingType> SettingTypes { get; set; }
        public IDbSet<Models.Contact> Contacts { get; set; }
        public IDbSet<Models.Country> Countries { get; set; }
        public IDbSet<Models.Currency> Currencies { get; set; }
        public IDbSet<Models.Link> Links { get; set; }
        public IDbSet<Models.LinkStatus> LinkStatus { get; set; }
        public IDbSet<Models.LinkType> LinkTypes { get; set; }
        public IDbSet<Models.Language> Languages { get; set; }
        public IDbSet<Models.UserSetting> UserSettings { get; set; }
        public IDbSet<Models.CrawledLinkStatus> CrawledLinkStatuses { get; set; }

        public IDbSet<Email> Emails { get; set; }
        public IDbSet<GoogleUser> GoogleUsers { get; set; }

        public static Link2WebDbContext Create()
        {
            return new Link2WebDbContext();
        }

    }
}