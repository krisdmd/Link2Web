using Google.Apis.Analytics.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Auth.OAuth2.Requests;
using Link2Web.DAL;
using System;
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
                    ClientId = HttpContext.Current.Session["GoogleClientId"].ToString(),
                    ClientSecret = HttpContext.Current.Session["GoogleClientSecret"].ToString()
                },
                Scopes = new[]
                {
                    AnalyticsService.Scope.AnalyticsReadonly,
                    AnalyticsService.Scope.Analytics
                },
               
                //HttpContext.Current.Server.MapPath("~/App_Data/clientsecret.json")
                DataStore = new EfDataStore(),

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
                HttpContext.Current.Session["User"] = user.ToString();
            }

            return user.ToString();

        }

        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }

    public class CustomAuthorizationCodeFlow : GoogleAuthorizationCodeFlow
    {
        public CustomAuthorizationCodeFlow(Initializer initializer) : base(initializer) { }

        public override AuthorizationCodeRequestUrl CreateAuthorizationCodeRequest(String redirectUri)
        {
            return new GoogleAuthorizationCodeRequestUrl(new Uri(AuthorizationServerUrl))
            {
                ClientId = ClientSecrets.ClientId,
                Scope = string.Join(" ", Scopes),
                RedirectUri = redirectUri,
                AccessType = "offline",
                ApprovalPrompt = "force"
            };
        }
    }
}