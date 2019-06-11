using System.Web.Mvc;

namespace Oulanka.Web.Mvc.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotAuthorized()
        {
            return View();
        }
    }
}