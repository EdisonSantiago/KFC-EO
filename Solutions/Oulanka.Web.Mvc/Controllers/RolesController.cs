using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.ViewModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class RolesController : BaseController
    {
        private readonly IUserAccountService _userAccountService;

        public RolesController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public ActionResult Index()
        {
            var viewModel = new RoleViewModel
            {
                Groups = _userAccountService.GetGroups()
            };

            return View(viewModel);
        }

        public ActionResult Show(int id)
        {
            var group = _userAccountService.GetGroup(id);
            var usersToAssign = GetUsersToAssign(group);
            var viewModel = new GroupViewModel
            {
                Grupo = _userAccountService.GetGroup(id),
                UsersToAssign = usersToAssign
            };
            return View(viewModel);
        }

        private IList<SelectListItem> GetUsersToAssign(Grupo grupo)
        {
            var users = _userAccountService.GetUsers();


            return (from user in users
                    where !grupo.Usuarios.Contains(user)
                    select new SelectListItem
                    {
                        Text = $"{user.NombreMostrar} ({user.Email})",
                        Value = user.Id.ToString()
                    }).ToList();
        }

        public JsonResult AssignUserToRole(int groupId, int userId)
        {
            var group = _userAccountService.GetGroup(groupId);
            var user = _userAccountService.GetUserById(userId);

            if (!group.Usuarios.Contains(user))
            {
                group.Usuarios.Add(user);
            }

            if (!user.Grupos.Contains(group))
            {
                user.Grupos.Add(group);
            }

            var confirmation = _userAccountService.SaveOrUpdateGroup(group);
            _userAccountService.SaveOrUpdateUser(user);

            return Json(confirmation.WasSuccessful, JsonRequestBehavior.AllowGet);
        }
    }
}