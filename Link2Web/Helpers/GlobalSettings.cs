using Google.Apis.Analytics.v3;
using Google.Apis.Auth.OAuth2;
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

        public static bool InitAnalytics;

        public static AnalyticsService AnalyticsService { get; set; }

        public string FacebookAccessToken
        {
            get
            {
                return HttpContext.Current.Session["FaceBookAccessToken"] == null
                    ? string.Empty
                    : HttpContext.Current.Session["FaceBookAccessToken"].ToString();
            }
            set { HttpContext.Current.Session["FaceBookAccessToken"] = value; }
        }

        public UserCredential GetAnalyticsService()
        {
            string[] scopes = new string[]
            {
                AnalyticsService.Scope.AnalyticsReadonly,
            };

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = HttpContext.Current.Session["GoogleClientId"].ToString(),
                ClientSecret = HttpContext.Current.Session["GoogleClientSecret"].ToString()
            },
                scopes,
                "user", CancellationToken.None,
                new FileDataStore(HttpContext.Current.Server.MapPath("~/App_Data/clientsecret.json"))).Result;


            return credential;
        }

        public void GetAccessToken()
        {
//            var token = "";
//            // Oauth2 Autentication.
//            using (var stream = new System.IO.FileStream("client_secret.json", System.IO.FileMode.Open, System.IO.FileAccess.Read))
//            {
//                var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
//                GoogleClientSecrets.Load(stream).Secrets,
//                new[] { AnalyticsService.Scope.AnalyticsReadonly },
//                HttpContext.Current.Session["User"].ToString(), CancellationToken.None, StoredRefreshToken).Result;
//            }
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

            Init = false;
        }

    }
}