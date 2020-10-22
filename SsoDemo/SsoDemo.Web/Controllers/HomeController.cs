using System.Configuration;
using System.Web.Mvc;

namespace SsoDemo.Web.Controllers
{
    public class HomeController : Controller
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

        [Authorize]
        public ActionResult Auth()
        {
            return Redirect("~/");
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session.Clear();

            var logOffUrl = ConfigurationManager.AppSettings["AppLogOffUrl"];

            return Redirect(logOffUrl);
        }
    }
}