using Google.Apis.Analytics.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Link2Web.DAL;
using System.Linq;
using System.Threading;
using System.Web;

namespace Link2Web.Helpers
{
    public class GlobalSettings
    {
        public static bool HideCreateProjectDialog { get; set; }

        public static bool Init;

        public static string FacebookAccessToken
        {
            get
            {
                return HttpContext.Current.Session["FaceBookAccessToken"] == null
                    ? string.Empty
                    : HttpContext.Current.Session["FaceBookAccessToken"].ToString();
            }
            set { HttpContext.Current.Session["FaceBookAccessToken"] = value; }
        }

        public AnalyticsService GetAnalyticsService()
        {
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = HttpContext.Current.Session["GoogleClientId"].ToString(),
                ClientSecret = HttpContext.Current.Session["GoogleClientSecret"].ToString()
            },
                new[]
                {
                    AnalyticsService.Scope.Analytics,
                },
                "user", CancellationToken.None,
                new FileDataStore(HttpContext.Current.Server.MapPath("~/App_Data/clientsecret.json"))).Result;


            var service = new AnalyticsService
                (new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Analytics",
                });

            return service;
        }


        public void GetSettingsFromDb(string userId)
        {
            var db = new Link2WebDbContext();

            if (string.IsNullOrEmpty(userId))
            {
                return;
            }

            var userSettings = db.UserSettings.Where(u => u.UserId.Contains(userId)).ToList();

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

        public static void Clear()
        {
            FacebookAccessToken = null;
            Init = false;
        }
    }
}