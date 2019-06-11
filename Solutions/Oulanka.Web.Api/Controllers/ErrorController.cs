using System.Web.Mvc;

namespace Oulanka.Web.Api.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotAuthorized()
        {
            return View();
        }
    }
}