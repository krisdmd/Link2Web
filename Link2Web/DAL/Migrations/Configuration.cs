namespace Link2Web.DAL.Migrations
{
    using System.Data.Entity.Migrations;

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

//            var userSettings = new List<UserSetting>
//            {
//                new UserSetting
//                {
//                    Setting = "ClientId",
//                    Value = "818431160125-cvgq5mtg850mi227tlrr95er14vs68d5.apps.googleusercontent.com",
//                    UserId = "17682bd4-9d9b-4341-b003-5500b7f797bd"
//                },
//                new UserSetting
//                {
//                    Setting = "CLientSecret",
//                    Value = "5KuiV58j2LI9mCfSJBrBXzxf",
//                    UserId = "17682bd4-9d9b-4341-b003-5500b7f797bd"
//                }
//            };
//
//            userSettings.ForEach(s => context.UserSettings.AddOrUpdate(s));

//            var linktypes = new List<LinkType>
//            {
//                new LinkType { Type = "None" },
//                new LinkType { Type = "Blog comment" },
//                new LinkType { Type = "Competitor backlink" },
//                new LinkType { Type = "Paid permanent" },
//                new LinkType { Type = "Paid temporary" }
//            };
//
//            linktypes.ForEach(t => context.LinkTypes.AddOrUpdate(t));
//
//
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
//
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
