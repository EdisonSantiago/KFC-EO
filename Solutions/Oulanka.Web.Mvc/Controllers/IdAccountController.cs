using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Identity;
using Oulanka.Web.Core.ActiveDirectory;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;
using Oulanka.Web.Core.ViewModels;
using SharpArch.NHibernate.Web.Mvc;

namespace Oulanka.Web.Mvc.Controllers
{
    [Authorize]
    public class IdAccountController : Controller
    {
        private const string XsrfKey = "XsrfKeyId";

        private readonly IEventLogService _eventLogService;
        public UserManager<IdentityUser> UserManager { get; private set; }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public IdAccountController(IEventLogService eventLogService, UserManager<IdentityUser> userManager)
        {
            _eventLogService = eventLogService;
            UserManager = userManager;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [Transaction]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var user = await UserManager.FindAsync(model.UserName, model.Password);
            if (user != null)
            {
                await SignInAsync(user, model.RememberMe);

                var logMessage = $"{model.UserName} logged on.";
                _eventLogService.AddInfo(logMessage,logMessage,EventCategory.ActividadUsuario.ToString(),User.Identity.Name,EventSource.Usuario);

                var pageMessage = $"Bienvenid@ {model.UserName}"; 
                this.AddPageMessage(pageMessage,PageMessageType.Success,true);


                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "El usuario o contraseña son incorrectos");

            return View(model);

        }

        public ActionResult LogOff()
        {
            var logMessage = $"{User.Identity.Name} logged out.";
            _eventLogService.AddInfo(logMessage,logMessage,EventCategory.ActividadUsuario.ToString(),User.Identity.Name,EventSource.Usuario);

            FormsAuthentication.SignOut();

            return RedirectToAction("index", "Home");
        }

        [ChildActionOnly]
        public ActionResult UserStatus()
        {
            var viewModel = new UserStatusViewModel();
            if (Request.IsAuthenticated)
            {
                viewModel.IsAuthenticated = true;

                var adUserInfo = AdUserInfoService.GetAdUserInfo(User.Identity.Name);
                if (adUserInfo != null)
                {
                    viewModel.LoginName = adUserInfo.LoginName;
                    viewModel.DisplayName = adUserInfo.FirstName;
                    viewModel.Thumbnail = adUserInfo.Thumbnail;

                }
            }


            return PartialView("_userStatus", viewModel);
        }

        #region privates

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("index", "Home");
        }

        private async Task SignInAsync(IdentityUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = isPersistent},identity);
        } 

        #endregion

    }
}