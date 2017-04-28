using Google.Apis.Analytics.v3;

namespace Link2Web.Core
{
    public static class Settings
    {
        public static AnalyticsService AnalyticsService { get; set; }
        public static string FacebookAccessToken
        {
            get
            {
                return System.Web.HttpContext.Current.Session["FaceBookAccessToken"] == null
                    ? string.Empty
                    : System.Web.HttpContext.Current.Session["FaceBookAccessToken"].ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["FaceBookAccessToken"] = value;
            }
        }
    }
}