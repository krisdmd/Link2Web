using Facebook;
using Link2Web.Core;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace Link2Web.Controllers
{
    public class FacebookController : BaseController
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "388275924892035",
                client_secret = "dbe0ce9eaf969beddb4754115868c31d",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email" // Add other permissions as needed
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return View("Index");
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "388275924892035",
                client_secret = "dbe0ce9eaf969beddb4754115868c31d",
                fb_exchange_token = code,
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            // Store the access token in the session for farther use
            Session["AccessToken"] = accessToken;

            // update the facebook client with the access token so 
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;
            Settings.FacebookAccessToken = accessToken;


            var pageFeed = string.Format("/v2.4/{0}/feed?fields=id,message,attachments", "2301741419964830");
            dynamic response = fb.Get(pageFeed);



            // Get the user's information
//            dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
//            string email = me.email;
//            string firstname = me.first_name;
//            string middlename = me.middle_name;
//            string lastname = me.last_name;

            //db.Insert_customer(firstname, email, null, null, null, null, null, null, null, null, null, null, 1, 1, System.DateTime.Now, 1, System.DateTime.Now);

            return RedirectToAction("Index", "Home");
        }

        // GET: Facebook
        public ActionResult Index()
        {
            return View();
        }
    }
}