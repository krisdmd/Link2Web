using Admin2Web.Helpers;
using Link2Web.DAL;
using System;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Link2Web.Areas.Admin.Controllers
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

                base.OnActionExecuted(filterContext);
            }
        }

    }
}