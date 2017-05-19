using Google.Apis.Analytics.v3;
using Link2Web.DAL;
using System.Linq;
using System.Web;

namespace Link2Web.Helpers
{
    public class GlobalSettings
    {
        public static bool HideCreateProjectDialog { get; set; }

        public static bool Init;


        public static AnalyticsService AnalyticsService { get; set; }

        public static string FacebookAccessToken
        {
            get
            {
                return HttpContext.Current.Session["FaceBookAccessToken"] == null
                    ? string.Empty
                    : HttpContext.Current.Session["FaceBookAccessToken"].ToString();
            }
            set { System.Web.HttpContext.Current.Session["FaceBookAccessToken"] = value; }
        }


        public void GetSettings(string userId)
        {
            var db = new Link2WebDbContext();

            if (string.IsNullOrEmpty(userId))
            {
                return;
            }

            var userSettings = db.UserSettings.Where(u => u.UserId.Contains(userId) && u.Value != null).ToList();

            foreach (var s in userSettings)
            {
                HttpContext.Current.Session[s.Setting] = s.Value;

                if (s.Setting == "CurrentProject")
                {
                    HttpContext.Current.Session["ViewProjectId"] = db.Projects.FirstOrDefault(p => p.ProjectId == s.ValueInt).ViewProfileId;
                }
            }

            Init = true;
        }

//        public void GetIntSettings(string userId)
//        {
//            var db = new Link2WebDbContext();
//
//            if (string.IsNullOrEmpty(userId))
//            {
//                return;
//            }
//
//            var userSettings = db.UserSettings.Where(u => u.UserId.Contains(userId) && string.IsNullOrEmpty(u.Value)).ToList();
//
//            foreach (var s in userSettings)
//            {
//                HttpContext.Current.Session[s.Setting] = s.ValueInt;
//            }
//
//            Init = true;
//        }


        public static void Clear()
        {
            FacebookAccessToken = null;
            AnalyticsService = null;
            Init = false;
        }
    }
}