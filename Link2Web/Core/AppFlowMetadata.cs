using Google.Apis.Analytics.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Util.Store;
using Link2Web.Helpers;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Link2Web.Core
{
    public class AppFlowMetadata : FlowMetadata
    {

        private static readonly IAuthorizationCodeFlow flow =
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = GlobalSettings.UserSettings.Where(s => s.Setting == "GoogleClientId").Select(s => s.Value).SingleOrDefault(),
                    ClientSecret = GlobalSettings.UserSettings.Where(s => s.Setting == "GoogleClientSecret").Select(s => s.Value).SingleOrDefault(),

                    //                    ClientId = "818431160125-3r7k172uejge70n8kfn6je7qhpa2h7ld.apps.googleusercontent.com",
                    //                    ClientSecret = "1q3x3wZMbkqevS47jKAuFdqs" 
                },
                Scopes = new[]
                {
                    AnalyticsService.Scope.AnalyticsReadonly
                },
                DataStore = new FileDataStore(HttpContext.Current.Server.MapPath("~/App_Data/clientsecret.json")),


                //DataStore = new FileDataStore("Drive.Api.Auth.Store")

            });

        public override string GetUserId(Controller controller)
        {
            // In this sample we use the session to store the user identifiers.
            // That's not the best practice, because you should have a logic to identify
            // a user. You might want to use "OpenID Connect".
            // You can read more about the protocol in the following link:
            // https://developers.google.com/accounts/docs/OAuth2Login.
            var user = controller.Session["user"];
            if (user == null)
            {
                user = Guid.NewGuid();
                controller.Session["user"] = user;
            }

            return user.ToString();

        }

        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }
}