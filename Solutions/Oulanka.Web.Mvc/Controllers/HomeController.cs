using System;
using System.Web.Mvc;
using System.Web.Security;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models;
using Oulanka.Web.Core.ViewModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IProjectService _projectService;


        public int CurrentProjectId => GetCurrentProjectId();

        private int GetCurrentProjectId()
        {
            var projectId = 0;
            if (Session["Current_Project"] != null)
            {
                projectId = int.Parse(Session["Current_Project"].ToString());
            }

            if (projectId == 0)
            {
                var cookie = ControllerContext.HttpContext.Request.Cookies["CurrentProjectID"];
                if (cookie != null)
                {
                    projectId = int.Parse(cookie.Value);
                }
            }

            return projectId;
        }

        public Project CurrentProject => GetCurrentProject(CurrentProjectId);

        private Project GetCurrentProject(int currentProjectId)
        {
            if (currentProjectId == 0)
            {
                FormsAuthentication.SignOut();

                Session.Clear();
                Session.Abandon();

                Response.Redirect("~/");
            };

            var project = _projectService.GetProject(currentProjectId);
            return project;
        }
        public HomeController(
            IUserAccountService userAccountService, 
            IProjectService projectService)
        {
            _userAccountService = userAccountService;
            _projectService = projectService;
        }

        #region Public Methods and Operators

        [Authorize]
        public ActionResult Index()
        {
            return RedirectFromGroup();
        }

        private ActionResult RedirectFromGroup()
        {
            var user = _userAccountService.GetUser(User.Identity.Name);

            if (user.EstaEnGrupo("users"))
               // return RedirectToAction("UserMy", "tickets");

            if (user.EstaEnGrupo("operators"))
               // return RedirectToAction("panelmy", "tickets");

            if (user.EstaEnGrupo("admins"))
                return RedirectToAction("index", "dashboard");

            return View();
        }

        #endregion


        public ActionResult Sidebar()
        {
            var user = _userAccountService.GetUser(User.Identity.Name);
            var viewModel = new SideBarViewModel
            {
                Usuario = user,
            };

            return PartialView("base/_sidebar", viewModel);
        }

        [ChildActionOnly]
        public ActionResult UserHeader()
        {
            var user = _userAccountService.GetUser(User.Identity.Name);
            user.UltimaActividadEn = DateTime.Now;
            _userAccountService.SaveOrUpdateUser(user);

            var viewModel = new UserHeaderViewModel()
            {
                Usuario = user,
                CurrentProject = CurrentProject
            };

            return PartialView("base/_headerUser", viewModel);
        }

        [ChildActionOnly]
        public ActionResult PanelHeader()
        {
            var user = _userAccountService.GetUser(User.Identity.Name);
            user.UltimaActividadEn = DateTime.Now;
            _userAccountService.SaveOrUpdateUser(user);

            var viewModel = new PanelHeaderViewModel()
            {
                Usuario = user,
                CurrentProject = CurrentProject
            };

            return PartialView("base/_header", viewModel);
        }
    }
}