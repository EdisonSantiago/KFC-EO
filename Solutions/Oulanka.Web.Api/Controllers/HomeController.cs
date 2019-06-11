using System;
using System.Web.Mvc;
using System.Web.Security;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models;
using Oulanka.Web.Core.ViewModels;

namespace Oulanka.Web.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IProjectService _projectService;


        public HomeController(
            IUserAccountService userAccountService, 
            IProjectService projectService)
        {
            _userAccountService = userAccountService;
            _projectService = projectService;
        }

        #region Public Methods and Operators

        public ActionResult Index()
        {
            return View();
        }


        #endregion



    }
}