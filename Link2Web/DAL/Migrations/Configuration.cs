using System.Collections.Generic;
using Link2Web.Areas.Admin.Models;
using Link2Web.Models;

namespace Link2Web.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Link2Web.DAL.Link2WebDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\Migrations";
        }

        protected override void Seed(Link2Web.DAL.Link2WebDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var countries = new List<Country>
            {
                new Country {Name = "Belgium", Code = "BEL"},
                new Country {Name = "Netherlands", Code = "NLD"}
            };

            countries.ForEach(c => context.Countries.AddOrUpdate(c));

            var settingTypes = new List<SettingType>
            {
                new SettingType { Type = "Campaign" },
                new SettingType { Type = "Facebook" },
                new SettingType { Type = "Google" },
                new SettingType { Type = "Project" }
            };

            settingTypes.ForEach(s => context.SettingTypes.AddOrUpdate(s));
        }
    }
}
