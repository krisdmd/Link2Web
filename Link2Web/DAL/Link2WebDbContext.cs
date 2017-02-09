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

        public DbSet<Project> Projects { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SettingType> SettingTypes { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<Country> Countries { get; set; }

        
        public static Link2WebDbContext Create()
        {
            return new Link2WebDbContext();
        }

    }
}