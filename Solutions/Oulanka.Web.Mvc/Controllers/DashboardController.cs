using System.Web.Mvc;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Web.Core.Attributes;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.ViewModels;

namespace Oulanka.Web.Mvc.Controllers
{
    [AuthorizeGroup(Groups = "admins,operators")]
    public class DashboardController : BaseController
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(
            IDashboardService dashboardService
            )
        {
            _dashboardService = dashboardService;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            this.LogEnterToAction();

            if (this.CurrentProjectId <= 0)
                return RedirectToAction("logoff", "account");

            var viewModel = new DashboardViewModel
            {
            };

            return View(viewModel);
        }

        #region Ajax Calls








        #endregion
    }
}