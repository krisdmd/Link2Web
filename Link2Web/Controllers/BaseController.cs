using Admin2Web.Helpers;
using Link2Web.DAL;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Link2Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        // GET: Base
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe


            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;


            return base.BeginExecuteCore(callback, state);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var context = new Link2WebDbContext();
                var username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                {
                    var user = context.Users.SingleOrDefault(u => u.UserName == username);
                    if (user != null)
                    {
                        var fullName = string.Concat(new[] {user.FirstName, " ", user.LastName});
                        ViewData.Add("FullName", fullName);
                    }
                }
                else
                {
                    ViewData.Add("FullName", "");
                }
            }

            base.OnActionExecuted(filterContext);
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