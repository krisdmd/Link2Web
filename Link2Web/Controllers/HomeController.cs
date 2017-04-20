using Admin2Web.Helpers;
using Link2Web.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Link2Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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

        public FileContentResult ProfilePicture()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var bdUsers = HttpContext.GetOwinContext().Get<Link2WebDbContext>();
                var userImage = bdUsers.Users.FirstOrDefault(x => x.Id == userId)?.ProfilePicture;




                if (userImage == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Content/Images/noImg.png");

                    var fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    var br = new BinaryReader(fs);
                    var imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");

                }
                // to get the user details to load user Image 


                return new FileContentResult(userImage, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Content/Images/noImg.png");

                byte[] imageData = null;
                var fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                var br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");

            }
        }
    }
}