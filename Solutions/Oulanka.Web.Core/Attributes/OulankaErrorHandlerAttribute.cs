using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Domain.Contracts.Services;

namespace Oulanka.Web.Core.Attributes
{
    public class OulankaErrorHandlerAttribute : HandleErrorAttribute
    {
        private IEventLogService _eventLogService = ServiceLocator.Current.GetInstance<IEventLogService>();

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            // if the request is AJAX return JSON else view.
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception.Message
                    }
                };
            }
            else
            {
                var controllerName = (string) filterContext.RouteData.Values["controller"];
                var actionName = (string) filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary(model),
                    TempData = filterContext.Controller.TempData
                };

                LogError(filterContext, filterContext.Exception);

                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = 500;

                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }
        }

        private void LogError(ExceptionContext filterContext, Exception exception)
        {
          // _eventLogService.AddException();
        }
    }
}