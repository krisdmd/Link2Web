using Facebook;
using Link2Web.Helpers;
using Link2Web.Models;
using Link2Web.ViewModels;
using System;
using System.Collections.Generic;
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
                client_id = System.Web.HttpContext.Current.Session["FacebookClientId"].ToString(),
                client_secret = System.Web.HttpContext.Current.Session["FacebookClientSecret"].ToString(),
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
            var vm = new FacebookViewModel();

            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = System.Web.HttpContext.Current.Session["FacebookClientId"].ToString(),
                client_secret = System.Web.HttpContext.Current.Session["FacebookClientSecret"].ToString(),
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
            GlobalSettings.FacebookAccessToken = accessToken;

            //id,message,attachments,picture,created_time,description,
            var pageFeed = $"/v2.8/{"2301741419964830"}/feed?fields=name,link,caption";
            dynamic response = fb.Get(pageFeed);

            //var fbResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookData>(response.ToString());

            var facebookPosts = new List<FacebookPost>();

            foreach (var p in response.data)
            {
                facebookPosts.Add(new FacebookPost
                {
                    Link = p.link != null ? p.link.ToString() : "",
                    Caption = p.link != null ? p.link.ToString() : "",
                    Message = p.link != null ? p.link.ToString() : "",
                    Createdtime = p.link != null ? p.link.ToString() : "",
                    Description = p.link != null ? p.link.ToString() : "",
                    Picture = p.link != null ? p.link.ToString() : "",
                    Name = p.link != null ? p.link.ToString() : ""

                });
            }

            vm.FacebookPosts = facebookPosts;


            return View("Index", vm);
        }

        // GET: Facebook
        public ActionResult Index()
        {
            return View();
        }
    }
}