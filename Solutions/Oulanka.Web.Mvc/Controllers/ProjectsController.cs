using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;
using Oulanka.Web.Core.ViewModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IUserAccountService _userAccountService;

        public ProjectsController(IProjectService projectService, IUserAccountService userAccountService)
        {
            _projectService = projectService;
            _userAccountService = userAccountService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProjects(int page = 1, int limit = 10)
        {
            var projects = _projectService.GetProjectsPagedList(page > 0 ? page - 1 : page, limit);
            var records = projects.Items;
            var total = projects.TotalCount;

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Members(string id)
        {
            var project = _projectService.GetProjectByIdentifier(id);

            var viewModel = new ProjectMemberViewModel
            {
                Project = project,
                UsersToAssign = GetUsersToAssign(project),
                GroupsToAssign = GetGroupsToAssign(project)
            };

            return View(viewModel);
        }

        public ActionResult New()
        {
            var formModel = new ProjectFormModel();
            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(ProjectFormModel formModel)
        {
            if (ModelState.IsValid && IsProjectValid(formModel))
            {
                var project = new Project
                {
                    Name = formModel.Name,
                    Description = formModel.Description,
                    Identifier = formModel.Identifier,
                    IsPrivate = false,
                    Status = (short) Status.Online,
                    CreatedBy = User.Identity.Name,
                    UpdateBy = User.Identity.Name,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };


                var confirmation = _projectService.SaveOrUpdate(project);
                if (confirmation.WasSuccessful)
                {
                    var newProject = _projectService.GetProjectByIdentifier(project.Identifier);
                    var user = _userAccountService.GetUser(User.Identity.Name);
                    var group = _userAccountService.GetGroupByName("admins");

                    var member = new ProjectMember
                    {
                        Project = newProject,
                        Usuario = user,
                        Grupo = group,
                        CreatedBy = User.Identity.Name,
                        CreatedOn = DateTime.Now
                    };

                    _projectService.SaveOrUpdateMember(member);


                    this.AddPageMessage("Proyecto creado con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "projects");
                }
                else
                {
                    this.AddPageMessage(confirmation.Message, PageMessageType.Error, false);

                }
            }

            return View(formModel);
        }

        public ActionResult Edit(int id)
        {
            var project = _projectService.GetProject(id);
            var formModel = new ProjectFormModel
            {
                Description = project.Description,
                Identifier = project.Identifier,
                Name = project.Name,
                Id = project.Id
            };
            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProjectFormModel formModel)
        {
            if (ModelState.IsValid && IsEditProjectValid(formModel))
            {
                var project = _projectService.GetProject(formModel.Id);

                project.Name = formModel.Name;
                project.Description = formModel.Description;
                project.Identifier = formModel.Identifier;
                project.Status = (short)Status.Online;
                project.UpdateBy = User.Identity.Name;
                project.UpdatedOn = DateTime.Now;



                var confirmation = _projectService.SaveOrUpdate(project);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Proyecto actualizado con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "projects");
                }
                else
                {
                    this.AddPageMessage(confirmation.Message, PageMessageType.Error, false);
                }
            }

            return View(formModel);
        }

        private bool IsProjectValid(ProjectFormModel formModel)
        {
            var isValid = new List<bool>();

            var similar = _projectService.GetProjectByIdentifier(formModel.Identifier);
            if (similar != null)
            {
                isValid.Add(false);
                ModelState.AddModelError("","Ya existe un proyecto con el mismo nombre.");
            }

            return !isValid.Contains(false);
        }

        private bool IsEditProjectValid(ProjectFormModel formModel)
        {
            var isValid = new List<bool>();

            var similar = _projectService.GetProjectByIdentifier(formModel.Identifier);
            if (similar != null && similar.Id != formModel.Id)
            {
                isValid.Add(false);
                ModelState.AddModelError("", "Ya existe un proyecto con el mismo nombre.");
            }

            return !isValid.Contains(false);
        }

        private IList<SelectListItem> GetGroupsToAssign(Project project)
        {
            var groups = UserAccountService.GetGroups();

            return (from @group in groups
                    select new SelectListItem
                    {
                        Text = $"{@group.Descripcion}",
                        Value = @group.Id.ToString()
                    }).ToList();
        }

        private IList<SelectListItem> GetUsersToAssign(Project project)
        {
            var users = UserAccountService.GetUsers();

            var list = new List<SelectListItem>();
            foreach (var user in users)
            {
                list.Add(new SelectListItem
                {
                    Text = $"{user.NombreMostrar} ({user.Email})", Value = user.Id.ToString()
                });}
            return list;

        }


        public JsonResult AssignMember(int projectId, int userId, int groupId)
        {
            var project = _projectService.GetProject(projectId);
            var user = _userAccountService.GetUserById(userId);
            var group = _userAccountService.GetGroup(groupId);

            var members = project.Members;
            var userExists = members.Any(x => Equals(x.Usuario, user) && Equals(x.Grupo, @group));

            if (!userExists)
            {
                var member = new ProjectMember
                {
                    Project = project,
                    Usuario = user,
                    Grupo = @group,
                    CreatedBy = User.Identity.Name,
                    CreatedOn = DateTime.Now
                };
                var confirmation = _projectService.SaveOrUpdateMember(member);
                return Json(confirmation.WasSuccessful);

            }
            else
            {
                throw new Exception(" Usuario ya existe");
            }


        }
    }
}