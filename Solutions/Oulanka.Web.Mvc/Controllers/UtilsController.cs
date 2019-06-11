using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Web.Core;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.ViewModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class UtilsController : BaseController
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IProjectService _projectService;

        public UtilsController(IUserAccountService userAccountService, IProjectService projectService)
        {
            _userAccountService = userAccountService;
            _projectService = projectService;
        }



        public JsonResult GetUsers()
        {
            var project = _projectService.GetProject(this.CurrentProjectId);
            var users = project.Members.Where(x => x.Grupo.Nombre == "users").Select(x => x.Usuario).OrderBy(c => c.NombreMostrar);
            var items = new List<SelectListItem>();
            foreach (var user in users)
            {
                var item = new SelectListItem
                {
                    Text = user.NombreMostrar,
                    Value = user.NombreMostrar
                };
                items.Add(item);
            }

            return Json(new { items });
        }

        public JsonResult GetOperators()
        {
            var project = _projectService.GetProject(this.CurrentProjectId);
            var users = project.Members.Where(x => x.Grupo.Nombre == "operators").Select(x => x.Usuario).OrderBy(c => c.NombreMostrar);
            var items = new List<SelectListItem>();
            foreach (var user in users)
            {
                var item = new SelectListItem
                {
                    Text = user.NombreMostrar,
                    Value = user.NombreMostrar
                };
                items.Add(item);
            }

            return Json(new { items });
        }

        [ChildActionOnly]
        public ActionResult GetGlobalScriptVars()
        {
            var viewModel = new GlobalScriptVarsViewModel
            {
                DefaultStatusId = this.DefaultStatus
            };


            return PartialView("_globalScriptVars", viewModel);
        }
    }
}