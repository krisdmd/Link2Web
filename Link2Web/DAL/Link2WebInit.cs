using Link2Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Link2Web.DAL
{
    public class Link2WebInit: CreateDatabaseIfNotExists<Link2WebDbContext>
    {
        protected override void Seed(Link2WebDbContext context)
        {
            //  This method will be called after migrating to the latest version.

//            var countries = new List<Country>
//            {
//                new Country {Name = "Belgium", Code = "BEL"},
//                new Country {Name = "Netherlands", Code = "NLD"}
//            };
//
//            countries.ForEach(c => context.Countries.AddOrUpdate(c));
//
//            var settingTypes = new List<SettingType>
//            {
//                new SettingType { Type = "User preferences" },
//                new SettingType { Type = "Campaign" },
//                new SettingType { Type = "Facebook" },
//                new SettingType { Type = "Google" },
//                new SettingType { Type = "Project" }
//            };
//
//            settingTypes.ForEach(s => context.SettingTypes.AddOrUpdate(s));

//            var currencies = new List<Currency>
//            {
//                new Currency { Name = "Euro", Code = "EUR", Symbol = "€"},
//                new Currency { Name = "US Dollar", Code = "USD", Symbol = "$"},
//                new Currency { Name = "British Pound Sterling", Code = "GBP", Symbol = "£"}
//            };
//
//            currencies.ForEach(c => context.Currencies.AddOrUpdate(c));
        }
    }
}