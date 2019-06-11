using System.Web.Mvc;
using Oulanka.Domain.Enums;
using Oulanka.Web.Core.Enums;

namespace Oulanka.Web.Core.Controllers
{
    public static class ControllerHelper
    {
        public static void AddPageMessage(this Controller controller, string message, PageMessageType messageType)
        {
            AddPageMessage(controller, message, messageType, false);
        }

        public static void AddPageMessage(this Controller controller, string message, PageMessageType messageType, bool isDismissable)
        {
            controller.TempData[GlobalViewDataProperty.PageMessage.ToString()] = message.ToLowerInvariant();
            controller.TempData[GlobalViewDataProperty.MessageType.ToString()] = messageType.ToString().ToLowerInvariant();
            controller.TempData[GlobalViewDataProperty.IsDismissable.ToString()] = isDismissable.ToString().ToLowerInvariant();
        }
    }
}