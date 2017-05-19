using System.Data.Entity;

namespace Link2Web.DAL
{
    public class Link2WebInit: CreateDatabaseIfNotExists<Link2WebDbContext>
    {
        protected override void Seed(Link2WebDbContext context)
        {
            //  This method will be called after migrating to the latest version.

//            var userSettings = new List<UserSetting>
//            {
//                new UserSetting
//                {
//                    Setting = "ClientId",
//                    Value = "818431160125-qs1ahg8oemfnkfc3g1l41e7vv85qp0k7.apps.googleusercontent.com",
//                    UserId = "6884d1c8-4b72-42d4-8927-973d9a88b906"
//                },
//                new UserSetting
//                {
//                    Setting = "CLientSecret",
//                    Value = "kP72hOc2VHvS5zgAkt-v4EdQ",
//                    UserId = "6884d1c8-4b72-42d4-8927-973d9a88b906"
//                }
//            };
//
//            userSettings.ForEach(s => context.UserSettings.AddOrUpdate(s));
//
//            var linktypes = new List<LinkType>
//            {
//                new LinkType { Type = "None" },
//                new LinkType { Type = "Blog comment" },
//                new LinkType { Type = "Competitor backlink" },
//                new LinkType { Type = "Paid permanent" },
//                new LinkType { Type = "Paid temporary" }
//            };

//            linktypes.ForEach(t => context.LinkTypes.AddOrUpdate(t));


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