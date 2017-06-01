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

        // GET: Facebook
        public ActionResult Index()
        {

            
            var vm = new FacebookViewModel();


            if (Session["fbInit"] != null)
            {
                var fbData = new FacebookData();
                vm.FacebookPosts = fbData.GetFacebookPosts("");
            }


            return View(vm);
        }

        // GET: Facebook
        public ActionResult CallBack(string code)
        {
            Session["LastController"] = "Facebook";
            Session["LastAction"] = "Index";
            Session["fbInit"] = true;

            var fbData = new FacebookData();
            fbData.GetFacebookPosts(code);

            return RedirectToAction("Index");
        }

        public void Connect()
        {
            Session["FbCallbackAction"] = "Callback";
            Session["FbCallbackController"] = "Facebook";

            var fbData = new FacebookData();

            fbData.FacebookOAuth();
        }

    }
}