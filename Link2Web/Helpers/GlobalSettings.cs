using Google.Apis.Analytics.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Link2Web.DAL;
using System;
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


        /// <summary>
        /// Authenticate to Google Using Oauth2
        /// Documentation https://developers.google.com/accounts/docs/OAuth2
        /// </summary>
        /// <param name="datastore">datastore to use </param>
        /// <returns></returns>
        public static AnalyticsService AuthenticateOauth()
        {
            var db = new EfDataStore();
            string[] scopes = {AnalyticsService.Scope.AnalyticsReadonly}; // view your basic profile info.

            try
            {
                // here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData%
                UserCredential credential =
                    GoogleWebAuthorizationBroker.AuthorizeAsync(
                        new ClientSecrets
                        {
                            ClientId = HttpContext.Current.Session["GoogleClientId"].ToString(),
                            ClientSecret = HttpContext.Current.Session["GoogleClientSecret"].ToString()
                        }
                        , scopes
                        , HttpContext.Current.Session["User"].ToString()
                        , CancellationToken.None
                        , db).Result;

                AnalyticsService service = new AnalyticsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "K2Web",
                });
                return service;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return null;
            }
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