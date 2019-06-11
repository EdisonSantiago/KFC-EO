using System.Web.Mvc;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Mvc.Models;

namespace Oulanka.Web.Mvc.Controllers
{
    public class StartupController : BaseController
    {
        public StartupController()
        {
        }

        public ActionResult Index()
        {
            var viewModel = new StartupViewModel
            {
                DisplayName = User.Identity.Name,
                UserName = User.Identity.Name,
                UserId = User.Identity.Name
            };
            return View(viewModel);
        }


        
       

    }
}