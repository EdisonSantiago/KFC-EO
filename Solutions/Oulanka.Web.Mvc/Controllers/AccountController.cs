using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Web.Core;
using Oulanka.Web.Core.ActiveDirectory;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;
using Oulanka.Web.Core.ViewModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEventLogService _eventLogService;
        private readonly IUserAccountService _userAccountService;
        private readonly IProjectService _projectService;

        public AccountController(
            IEventLogService eventLogService,
            IUserAccountService userAccountService,
            IProjectService projectService)
        {
            _eventLogService = eventLogService;
            _userAccountService = userAccountService;
            _projectService = projectService;
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            var formModel = new ChangePasswordModel
            {
                Usuario = _userAccountService.GetUser(User.Identity.Name)
            };
            return View(formModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel formModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AdUserInfoService.ResetPassword(User.Identity.Name,formModel.OldPassword, formModel.Password);
                    this.AddPageMessage("Contraseña cambiada con éxito", PageMessageType.Success, true);
                    return RedirectToAction("LogOff");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Error al actualizar: " + exception.Message.ToString());
                }
            }

            formModel.Usuario = _userAccountService.GetUser(User.Identity.Name);

            return View(formModel);
        }

        public ActionResult Login()
        {
            var loginModel = new LoginModel();

            PopulateProjects(loginModel);
            loginModel.RememberMe = false;

            var projectCookie = ControllerContext.HttpContext.Request.Cookies["CurrentProjectID"];
            if (projectCookie != null)
            {
                loginModel.ProjectId = int.Parse(projectCookie.Value);
            }

            return View(loginModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                PopulateProjects(model);

                return View(model);
            }

            if (IsLdapUser(model.UserName))
            {
                if (Membership.ValidateUser(model.UserName, model.Password) && IsProjectMember(model.UserName, model.ProjectId))
                {
                    var user = _userAccountService.GetUser(model.UserName);
                    if (!user.EstaEnLinea)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        //CreateAuthCookie(model.NombreUsuario, model.RememberMe);

                        Session.Add("HpDk_User", model.UserName);
                        Session.Add("Current_Project", model.ProjectId);

                        user.UltimoLoginEn = DateTime.Now;
                        user.EstaEnLinea = true;
                        var saveUserConfirmation = _userAccountService.SaveOrUpdateUser(user);


                        var logMessage = $"{model.UserName} inició sesión.";
                        _eventLogService.AddInfo(logMessage, logMessage, EventCategory.ActividadUsuario.ToString(),
                            User.Identity.Name, EventSource.Usuario);

                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/"))
                        {
                            return Redirect(returnUrl);
                        }

                        var pageMessage = $"Bienvenid@ {model.UserName}";
                        this.AddPageMessage(pageMessage, PageMessageType.Success, true);

                        SaveProjectCookie(model.ProjectId);

                        return RedirectByGroup(model.UserName);

                    }
                    else
                    {
                        ModelState.AddModelError("",
                            "Ya existe una sesión de este usuario. Cierre la otra sesión para poder ingresar al sistema");
                        var failMessage = $"{model.UserName} trató de ingresar pero ya existe otra sesión.";
                        _eventLogService.AddInfo(failMessage, failMessage, EventCategory.ActividadUsuario.ToString(),
                            User.Identity.Name, EventSource.Usuario);

                        PopulateProjects(model);

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "El usuario o contraseña son incorrectos");
                    var concurrentMessage = $"{model.UserName} fallo de autenticación al ingresar.";
                    _eventLogService.AddWarn(concurrentMessage, concurrentMessage, EventCategory.ActividadUsuario.ToString(), User.Identity.Name, EventSource.Usuario);

                }
            }
            else
            {
                if (LocalLogin(model.UserName, model.Password, false) && IsProjectMember(model.UserName, model.ProjectId))
                {
                    var user = _userAccountService.GetUser(model.UserName);
                    user.UltimoLoginEn = DateTime.Now;
                    user.EstaEnLinea = true;
                    var saveUserConfirmation = _userAccountService.SaveOrUpdateUser(user);

                    Session.Add("Current_Project", model.ProjectId);
                    SaveProjectCookie(model.ProjectId);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/"))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectByGroup(model.UserName);
                }
            }

            PopulateProjects(model);
            return View(model);

        }

        private void CreateAuthCookie(string userName, bool rememberMe)
        {
            // create encryption cookie         
            var authTicket = new FormsAuthenticationTicket(1,
                    userName,
                    DateTime.Now,
                    DateTime.Now.AddDays(90),
                    rememberMe,
                    string.Empty);

            // add cookie to response stream         
            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            if (authTicket.IsPersistent)
            {
                authCookie.Expires = authTicket.Expiration;
            }

            System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        private bool IsLdapUser(string userName)
        {
            var user = _userAccountService.GetUser(userName);

            return user != null && user.EsLdap;
        }

        private ActionResult RedirectByGroup(string userName)
        {
            var user = _userAccountService.GetUser(userName);
            var urlToRedirect = "";

            if (user.EstaEnGrupo("admins"))
                urlToRedirect = Url.RouteUrl("dashboard");

            else if (user.EstaEnGrupo("operators"))
            {
                //urlToRedirect = Url.Action("panelmy", "tickets");
            }
            else if (user.EstaEnGrupo("users"))
            {
                //urlToRedirect = Url.Action("usernew", "tickets");
            }

            return Redirect(urlToRedirect);
        }

        public ActionResult LogOff()
        {

            if (User.Identity.IsAuthenticated)
            {
                var logMessage = $"{User.Identity.Name} terminó sesión.";
                _eventLogService.AddInfo(logMessage, logMessage, EventCategory.ActividadUsuario.ToString(), User.Identity.Name, EventSource.Usuario);

                var user = _userAccountService.GetUser(User.Identity.Name);
                user.EstaEnLinea = false;

                var saveConfirmation = _userAccountService.SaveOrUpdateUser(user);
            }

            RemoveProjectCookie();
            FormsAuthentication.SignOut();

            Session.Clear();
            Session.Abandon();

            return RedirectToAction("index", "Home");
        }

        [ChildActionOnly]
        public ActionResult UserStatus()
        {
            var viewModel = new UserStatusViewModel();
            if (Request.IsAuthenticated)
            {
                viewModel.IsAuthenticated = true;

                //TODO: Change in production

                //FAKE DATA
                var user = _userAccountService.GetUser(User.Identity.Name);
                if (user != null)
                {
                    viewModel.LoginName = user.NombreUsuario;
                    viewModel.DisplayName = user.NombreMostrar;
                    viewModel.Email = user.Email;
                    viewModel.Usuario = user;
                }
            }


            return PartialView("_userStatus", viewModel);
        }


        private bool LocalLogin(string username, string password, bool rememberMe)
        {
            var user = _userAccountService.GetUser(username);
            if (user == null)
            {
                ModelState.AddModelError("", "Usuario no existe");
                return false;
            }

            if (user.LocalPassword == Crypto.EncryptString(password))
            {
                if (!user.EstaEnLinea)
                {
                    FormsAuthentication.SetAuthCookie(user.NombreUsuario, rememberMe);
                 // CreateAuthCookie(username,rememberMe);
                    Session.Add("HpDk_User", user.NombreUsuario);

                    var logMessage = $"{user.NombreUsuario} inició sesión.";
                    _eventLogService.AddInfo(logMessage, logMessage, EventCategory.ActividadUsuario.ToString(),
                        User.Identity.Name, EventSource.Usuario);

                    var pageMessage = $"Bienvenid@ {user.NombreUsuario}";
                    this.AddPageMessage(pageMessage, PageMessageType.Success, true);

                    return true;
                }
                else
                {
                    ModelState.AddModelError("", "Ya existe una sesión de este usuario. Cierre la otra sesión para poder ingresar al sistema");
                    var failMessage = $"{user.NombreUsuario} trató de ingresar pero ya existe otra sesión.";
                    _eventLogService.AddInfo(failMessage, failMessage, EventCategory.ActividadUsuario.ToString(), User.Identity.Name, EventSource.Usuario);

                    return false;
                }

            }
            else
            {
                ModelState.AddModelError("", "El usuario o la contraseña no son correctos");
                return false;
            }

        }

        #region PROJECTS

        private void SaveProjectCookie(int projectId)
        {
            RemoveProjectCookie();
            var cookie = new HttpCookie("CurrentProjectId")
            {
                Value = projectId.ToString(), 
                Expires = DateTime.Now.AddMinutes(42300)
            };

            ControllerContext.HttpContext.Response.Cookies.Add(cookie);
        }

        private void RemoveProjectCookie()
        {
            var cookie = this.ControllerContext.HttpContext.Request.Cookies["CurrentProjectID"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
        }

        private bool IsProjectMember(string username, int projectId)
        {
            var isMember = new List<bool>();

            var user = _userAccountService.GetUser(username);
            if (user == null)
            {
                isMember.Add(false);
                ModelState.AddModelError("", "Usuario no existe!");
                return false;
            }

            var project = _projectService.GetProject(projectId);
            if (project.Members != null && project.Members.Count > 0)
            {
                if (!project.Members.Any(x => x.Usuario.NombreUsuario == user.NombreUsuario))
                {
                    isMember.Add(false);
                    ModelState.AddModelError("", "No tiene membresía a este proyecto.");
                }
            }
            else
            {
                isMember.Add(false);
                ModelState.AddModelError("", "No tiene acceso a este proyecto.");
            }


            return !isMember.Contains(false);
        }

        private void PopulateProjects(LoginModel loginModel)
        {
            var projects = _projectService.GetProjectsDto();
            var projectItems = new List<SelectListItem>();

            foreach (var project in projects)
            {
                var item = new SelectListItem
                {
                    Text = project.Name,
                    Value = project.Id.ToString(),
                    Selected = project.Id == loginModel.ProjectId
                };

                projectItems.Add(item);
            }
            loginModel.Projects = projectItems;
        }

        #endregion

    }
}