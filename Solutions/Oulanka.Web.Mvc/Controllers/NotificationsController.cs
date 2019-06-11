using System;
using System.Web.Mvc;
using Oulanka.Domain.Enums;
using Oulanka.Web.Core.Controllers;

namespace Oulanka.Web.Mvc.Controllers
{
    public class NotificationsController : BaseController 
    {
        public JsonResult AddPageMessageType(string message,string type, bool isDismissable)
        {
            var messageType = (PageMessageType) Enum.Parse(typeof (PageMessageType), type);

            this.AddPageMessage(message,messageType,isDismissable);

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}