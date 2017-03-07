using System.Web.Mvc;

namespace Link2Web.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }
    }
}