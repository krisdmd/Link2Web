using Link2Web.DAL;
using Link2Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Link2Web.Helpers
{
    public class GlobalSettings
    {
        public static bool HideCreateProjectDialog { get; set; }
        public static string UserId { get; set; }

        public static bool Initialized;

        public static int ProjectId { get; set; }
        public static string ProjectViewId { get; set; }
        public static string GoogleClientId { get; set; }
        public static string GoogleClientSecret { get; set; }
        public static string FacebookClientId { get; set; }
        public static string FacebookSecret { get; set; }

        //Set active when the settings are initialised.
        public static bool Active { get; set; }


        public static List<UserSetting> UserSettings = new List<UserSetting>();

        public GlobalSettings(string userId)
        {
            Initialized = false;
            UserId = userId;
        }

        public GlobalSettings()
        {
            Initialized = false;
        }


        /// Loads all the settings from the db
        private void RetrieveAllSettings()
        {
            if (UserId != null)
            {
                var db = new Link2WebDbContext();

                var userSettings = db.UserSettings.Where(u => u.UserId.Contains(UserId));

                foreach (var s in userSettings)
                {
                    UserSettings.Add(new UserSetting
                    {
                        UserId = s.UserId,
                        Value = s.Value,
                        ValueInt = s.ValueInt,
                        Setting = s.Setting
                    });
                }
            }
        }

        public void Init()
        {
            if (!Initialized)
                RetrieveAllSettings();
            Initialized = true;
        }
    }
}