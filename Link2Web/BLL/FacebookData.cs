using Facebook;
using Link2Web.Helpers;
using Link2Web.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Link2Web.BLL
{
    public class FacebookData
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(HttpContext.Current.Request.Url);
                var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = urlHelper.Action(HttpContext.Current.Session["LastAction"].ToString(),
                    HttpContext.Current.Session["LastController"].ToString());
                return uriBuilder.Uri;
            }
        }

        public void FacebookOAuth()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = HttpContext.Current.Session["FacebookClientId"].ToString(),
                client_secret = HttpContext.Current.Session["FacebookClientSecret"].ToString(),
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email" // Add other permissions as needed
            });

            HttpContext.Current.Response.Redirect(loginUrl.AbsoluteUri);
        }

        public List<FacebookPost> GetFacebookPosts(string code)
        {
            FacebookClient fb;
            var facebookPosts = new List<FacebookPost>();

            if (HttpContext.Current.Session["AccessToken"] != null)
            {
                fb = new FacebookClient(HttpContext.Current.Session["AccessToken"].ToString());
            }
            else
            {
                fb = new FacebookClient();

                dynamic result = fb.Post("oauth/access_token", new
                {
                    client_id = HttpContext.Current.Session["FacebookClientId"].ToString(),
                    client_secret = HttpContext.Current.Session["FacebookClientSecret"].ToString(),
                    fb_exchange_token = code,
                    redirect_uri = RedirectUri.AbsoluteUri,
                    code = code
                });

                var accessToken = result.access_token;

                // Store the access token in the session for farther use
                HttpContext.Current.Session["AccessToken"] = accessToken;

                // update the facebook client with the access token so 
                // we can make requests on behalf of the user
                fb.AccessToken = accessToken;
                new GlobalSettings().FacebookAccessToken = accessToken;
            }

            //id,message,attachments,picture,created_time,description,
            var pageFeed =
                $"/v2.9/{"2301741419964830"}/posts/?fields=name,link,caption,created_time,message,description,picture,story,permalink_url,place,from";
            dynamic response = fb.Get(pageFeed);

            foreach (var p in response.data)
            {
                if (!string.IsNullOrWhiteSpace(p.name))
                {
                    facebookPosts.Add(new FacebookPost
                    {
                        Link = p.link != null ? p.link.ToString() : "",
                        Caption = p.caption != null ? p.caption.ToString() : "",
                        Message = p.message != null ? p.message.ToString() : "",
                        Createdtime = p.created_time != null ? Convert.ToDateTime(p.created_time) : DateTime.Now,
                        Description = p.description != null ? p.description.ToString() : "",
                        Story = p.story != null ? p.story.ToString() : "",
                        Picture = p.link != null ? p.link.ToString() : "",
                        PermalinkUrl = p.permalink_url != null ? p.permalink_url.ToString() : "",
                        From = p.@from != null ? p.@from.ToString() : "",
                        Place = p.place != null ? p.place.ToString() : "",
                        Name = p.name != null ? p.name.ToString() : ""
                    });
                }
            }

            return facebookPosts;
        }
    }
}