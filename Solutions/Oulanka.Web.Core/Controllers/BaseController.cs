using System;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Configuration;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;

namespace Oulanka.Web.Core.Controllers
{

    [Authorize]
    [HandleError(View = "Error")]
    public class BaseController : Controller
    {
        public IProjectService ProjectService => ServiceLocator.Current.GetInstance<IProjectService>();
        public IEventLogService EventLogService => ServiceLocator.Current.GetInstance<IEventLogService>();
        public IConfigurationSettings Configuration => ServiceLocator.Current.GetInstance<IConfigurationSettings>();
        public ISettingService Settings => ServiceLocator.Current.GetInstance<ISettingService>();
        public IUserAccountService UserAccountService => ServiceLocator.Current.GetInstance<IUserAccountService>();
        public Usuario UsuarioAccount => User.Identity.IsAuthenticated
            ? UserAccountService.GetUser(User.Identity.Name)
            : null;

        public IEmailService EmailService => ServiceLocator.Current.GetInstance<IEmailService>();

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

        public Guid DefaultStatus => GetDefaultStatus();

        private Guid GetDefaultStatus()
        {
            var statusGuid = Guid.Empty;
            var defaultStatusId = Settings.Get("global", "estado_default_id").Value;
            if (!string.IsNullOrEmpty(defaultStatusId))
            {
                statusGuid = Guid.Parse(defaultStatusId);
            }

            return statusGuid;
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

            var project = ProjectService.GetProject(currentProjectId);
            return project;
        }

        public void LogEnterToAction(string message = "")
        {
            var username = "anónimo";
            if (Request.IsAuthenticated)
            {
                username = User.Identity.Name;
            }

            var route = Request.RequestContext.RouteData;
            var controller = route.GetRequiredString("controller");
            var action = route.GetRequiredString("action");

            var logMessage = $"{username} ingresó a {controller.ToLower()} - {action.ToLower()}";

            if (!string.IsNullOrEmpty(message))
                logMessage = logMessage + " | " + message;

            EventLogService.AddInfo(logMessage, logMessage, EventCategory.ActividadUsuario.ToString(), username, EventSource.Usuario.ToString());

        }

        public void LogSaveObjectAction(string message = "", string savedObject = "")
        {
            var username = "anónimo";
            if (Request.IsAuthenticated)
            {
                username = User.Identity.Name;
            }

            var route = Request.RequestContext.RouteData;
            var controller = route.GetRequiredString("controller");
            var action = route.GetRequiredString("action");

            var logMessage = $"{username} guardó objeto {savedObject} usando {controller.ToLower()} - {action.ToLower()}";

            if (!string.IsNullOrEmpty(message))
                logMessage = logMessage + " | " + message;

            EventLogService.AddInfo(logMessage, logMessage, EventCategory.ActividadUsuario.ToString(), username, EventSource.Usuario, savedObject);
        }

        public void LogErrorObjectAction(string message = "", string savedObject = "")
        {
            var username = "anónimo";
            if (Request.IsAuthenticated)
            {
                username = User.Identity.Name;
            }

            var route = Request.RequestContext.RouteData;
            var controller = route.GetRequiredString("controller");
            var action = route.GetRequiredString("action");

            var logMessage = $"{username} error al eliminar objeto {savedObject} usando {controller.ToLower()} - {action.ToLower()}";

            if (!string.IsNullOrEmpty(message))
                logMessage = logMessage + " | " + message;

            EventLogService.AddException(
                logMessage, 
                logMessage, 
                EventCategory.ActividadUsuario.ToString(), 
                new Exception(message), 
                username, 
                EventSource.Usuario, savedObject);

        }
    }
}
