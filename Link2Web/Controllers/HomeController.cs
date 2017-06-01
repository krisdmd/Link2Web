using Link2Web.BLL;
using Link2Web.Helpers;
using Link2Web.ViewModels;
using System;
using System.Web;
using System.Web.Mvc;

namespace Link2Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var vm = new HomeViewModel();


            if (Session["fbInit"] != null)
            {
                var fbData = new FacebookData();
                vm.FacebookPosts = fbData.GetFacebookPosts("").GetRange(0,5);
            }

            return View(vm);
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.UtcNow.AddHours(250);
                //                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult KeepSessionAlive()
        {
            return new JsonResult { Data = "Success" };
        }

        // GET: Facebook
        public ActionResult CallBack(string code)
        {
            Session["LastController"] = "Home";
            Session["LastAction"] = "Index";
            Session["fbInit"] = true;

            var fbData = new FacebookData();
            fbData.GetFacebookPosts(code);

            return RedirectToAction("Index");
        }

        public void Connect()
        {
            Session["FbCallbackAction"] = "Callback";
            Session["FbCallbackController"] = "Home";

            var fbData = new FacebookData();

            fbData.FacebookOAuth();
        }

    }
}