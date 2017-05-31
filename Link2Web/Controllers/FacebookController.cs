using Facebook;
using Link2Web.BLL;
using Link2Web.ViewModels;
using System.Web.Mvc;
using System.Web.Security;

namespace Link2Web.Controllers
{
    public class FacebookController : BaseController
    {

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
            var fbData = new FacebookData();

            vm.FacebookPosts =  fbData.GetFacebookPosts(code);

            return View("Index", vm);
        }

        // GET: Facebook
        public ActionResult Index(string code)
        {

            if (Session["AccessToken"] == null)
            {
                return RedirectToAction("Connect");
            }

            var fb = new FacebookClient();
            var vm = new FacebookViewModel();
            var fbData = new FacebookData();

            vm.FacebookPosts = fbData.GetFacebookPosts(code);
            return View(vm);
        }

        public void Connect()
        {
            Session["LastController"] = "Facebook";
            Session["LastAction"] = "FacebookCallback";

            var fbData = new FacebookData();

            fbData.FacebookOAuth();
        }
    }
}