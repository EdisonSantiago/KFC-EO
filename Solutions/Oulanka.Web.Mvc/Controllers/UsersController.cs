using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Identity;
using Oulanka.Web.Core;
using Oulanka.Web.Core.ActiveDirectory;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;
using Oulanka.Web.Core.ViewModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserAccountService _accountService;
        public UserManager<IdentityUser> UserManager { get; private set; }

        public UsersController(IUserAccountService accountService)
        {
            _accountService = accountService;
        }

        public ActionResult Index()
        {
            var systemUsers = _accountService.GetUsers();
            var adUsersForImport = new List<AdUserInfo>();

            if (Configuration.GetConfig().LdapAuthEnabled)
            {
                IList<AdUserInfo> adUsers;
                try
                {
                    adUsers = AdUserInfoService.GetAdUserList();
                }
                catch (Exception exception)
                {
                    adUsers = new List<AdUserInfo>();
                    this.AddPageMessage("No se puede conectar al Active Directory", PageMessageType.Error, false);
                }


                foreach (var adUser in adUsers)
                {
                    if (systemUsers.All(u => u.NombreUsuario != adUser.LoginName))
                    {
                        adUsersForImport.Add(adUser);
                    }
                }
            }

            var viewModel = new UsersViewModel
            {
                Users = systemUsers,
                AdUsers = adUsersForImport,
                IsLdapAuthEnabled = Configuration.GetConfig().LdapAuthEnabled
            };
            return View(viewModel);
        }

        public ActionResult Show(int id)
        {
            var user = _accountService.GetUserById(id);

            var viewModel = new UserViewModel
            {
                Usuario = user
            };


            return View(viewModel);
        }

        public ActionResult Online()
        {
            var list = _accountService.GetUsers().Where(u => u.EstaEnLinea).ToList();
            var model = new OnlineUsersViewModel
            {
                Users = list
            };
            return View(model);
        }

        public ActionResult New()
        {
            var model = new UserAdminFormModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(UserAdminFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (ModelState.IsValid && IsUserValid(model))
            {


                var newUser = new Usuario
                {
                    NombreUsuario = model.UserName,
                    Email = model.Email,
                    LocalPassword = Crypto.EncryptString(model.Password),
                    NombreMostrar = model.FirstName + " " + model.LastName,
                    CreadoEn = DateTime.Now,
                    ActualizadoEn = DateTime.Now,
                    UltimaActividadEn = DateTime.Now,
                    UltimoLoginEn = DateTime.Now,
                    CuentaAccesosFallidos = 0,
                    EsLdap = false,
                    EstaBloqueado = false,
                    EstaEnLinea = false,
                };

                var confirmation = _accountService.SaveOrUpdateUser(newUser);
                if (confirmation.WasSuccessful)
                {
                    var dbUser = _accountService.GetUser(newUser.NombreUsuario);

                    var newProfile = new PerfilUsuario
                    {
                        Direccion = model.Address,
                        Nombre = model.FirstName,
                        Apellido = model.LastName,
                        Telefono = model.Phone,
                        Usuario = dbUser
                    };

                    var saveProfileConfirm = _accountService.SaveOrUpdateUserProfile(newProfile);

                    this.AddPageMessage(ResourceManager.GetString("users_messages_create_ok"), PageMessageType.Success, true);
                    return RedirectToAction("index", "users");

                }
                else
                {
                    this.AddPageMessage(confirmation.Message, PageMessageType.Error, false);
                    this.EventLogService.AddException("No se puede guardar usuario", confirmation.Message, "users", null, this.User.Identity.Name, EventSource.Administrador);
                }

            }

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var username = id;

            var dbUser = _accountService.GetUser(username);
            if (dbUser != null)
            {
                var model = new UserAdminFormModel
                {
                    UserName = dbUser.NombreUsuario,
                    Email = dbUser.Email,
                    Address = dbUser.PerfilUsuario.Direccion,
                    FirstName = dbUser.PerfilUsuario.Nombre,
                    LastName = dbUser.PerfilUsuario.Apellido,
                    Phone = dbUser.PerfilUsuario.Telefono
                };

                return View(model);
            }
            else
            {
                this.AddPageMessage("usuario no existe!", PageMessageType.Error, true);
                return RedirectToAction("index", "users");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, UserAdminFormModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (ModelState.IsValid && IsUserValid(model))
            {
                var theUser = _accountService.GetUser(id);


                theUser.NombreUsuario = model.UserName;
                theUser.Email = model.Email;
                theUser.NombreMostrar = model.FirstName + " " + model.LastName;
                theUser.ActualizadoEn = DateTime.Now;
                theUser.PerfilUsuario.Nombre = model.FirstName;
                theUser.PerfilUsuario.Apellido = model.LastName;
                theUser.PerfilUsuario.Direccion = model.Address;
                theUser.PerfilUsuario.Telefono = model.Phone;

                if (!string.IsNullOrEmpty(model.Password))
                {
                    var encPassword = Crypto.EncryptString(model.Password);
                    if (encPassword != null && encPassword != theUser.LocalPassword)
                    {
                        theUser.LocalPassword = encPassword;
                    }
                }

                var confirmation = _accountService.SaveOrUpdateUser(theUser);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage(ResourceManager.GetString("users_messages_create_ok"), PageMessageType.Success, true);
                    return RedirectToAction("index", "users");

                }
                else
                {
                    this.AddPageMessage(confirmation.Message, PageMessageType.Error, false);
                    this.EventLogService.AddException("No se puede guardar usuario", confirmation.Message, "users", null, this.User.Identity.Name, EventSource.Administrador);
                }

            }

            return View(model);

        }

        private bool IsUserValid(UserAdminFormModel model)
        {
            var isValid = new List<bool>();

            var userByUserName = _accountService.GetUser(model.UserName);
            if (userByUserName != null)
            {
                isValid.Add(false);
                ModelState.AddModelError("NombreUsuario", ResourceManager.GetString("users_messages_username_exists"));
            }

            var userByEmail = _accountService.GetUserByEmail(model.Email);
            if (userByEmail != null)
            {
                isValid.Add(false);
                ModelState.AddModelError("Email", ResourceManager.GetString("users_messages_useremail_exists"));
            }

            return !isValid.Contains(false);
        }


        #region Json Actions

        public JsonResult GetUsers(int page = 1, int limit = 10)
        {
            var users = _accountService.GetUsersPagedList(page > 0 ? page - 1 : page, limit);
            var records = users.Items;
            var total = users.TotalCount;

            return Json(new
            {
                records,
                total
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangePassword(int userId, string password)
        {
            var user = _accountService.GetUserById(userId);
            user.LocalPassword = Crypto.EncryptString(password);

            var confirmation = _accountService.SaveOrUpdateUser(user);

            return Json(confirmation);
        }

        public JsonResult ImportAdUsers(IList<string> usernames)
        {
            var importedUsers = new List<Usuario>();
            var systemUsers = _accountService.GetUsers();

            foreach (var username in usernames)
            {
                var adInfo = AdUserInfoService.GetAdUserInfo(username);

                var userAccount = new Usuario
                {
                    NombreUsuario = adInfo.LoginName,
                    NombreMostrar = adInfo.DisplayName,
                    Email = adInfo.EmailAddress,
                    EsLdap = true,
                    CreadoEn = DateTime.Now,
                    ActualizadoEn = DateTime.Now,
                    LocalPassword = "",
                    UltimoLoginEn = DateTime.Now.AddYears(-100),
                    UltimaActividadEn = DateTime.Now.AddYears(-100)

                };

                if (systemUsers.All(u => u.NombreUsuario != adInfo.LoginName))
                {
                    var confirmation = _accountService.SaveOrUpdateUser(userAccount);
                    if (confirmation.WasSuccessful)
                    {
                        importedUsers.Add(userAccount);
                    }
                }

            }

            return Json(true);

        }

        public JsonResult SetUserOffline(int id)
        {
            var userById = _accountService.GetUserById(id);
            if (userById != null)
            {
                userById.EstaEnLinea = false;
            }
            var confirmation = _accountService.SaveOrUpdateUser(userById);
            return Json(new { confirmation.WasSuccessful });
        }

        #endregion







    }
}