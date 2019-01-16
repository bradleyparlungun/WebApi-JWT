using System.Web.Mvc;

namespace alxbrn_api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "alxbrn-api";

            return View();
        }
    }
}
