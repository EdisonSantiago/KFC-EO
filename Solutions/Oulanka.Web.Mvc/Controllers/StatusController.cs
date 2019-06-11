using System;
using System.Web.Mvc;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models;
using Oulanka.Web.Core.Controllers;

namespace Oulanka.Web.Mvc.Controllers
{
    public class StatusController : BaseController
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        // GET
        public ActionResult Index()
        {
            this.LogEnterToAction();
            return View();
        }

        public JsonResult GetStatusList()
        {
            var statusList = _statusService.GetItems();
            return Json(statusList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveStatus(Estado status)
        {
            Estado dbStatus = null;
            dbStatus = status.Id == Guid.Empty
                ? new Estado()
                : _statusService.Get(status.Id);

            dbStatus.Nombre = status.Nombre;
            dbStatus.Grupo = status.Grupo;
            dbStatus.Descripcion = status.Nombre;
        
            if (status.Id == Guid.Empty)
            {
                dbStatus.CreadoEn = DateTime.Now;
                dbStatus.CreadoPor = User.Identity.Name;
            }

            dbStatus.ActualizadoEn = DateTime.Now;
            dbStatus.ActualizadoPor = User.Identity.Name;

            var confirmation = _statusService.SaveOrUpdate(dbStatus);
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult Delete(Guid id)
        {
            var item = _statusService.Get(id);
            if (item == null)
                return Json(false);

            var itemName = item.Grupo + "|" + item.Nombre;
            var confirmation = _statusService.Delete(item.Id);
            if (confirmation.WasSuccessful)
            {
                LogSaveObjectAction(savedObject: $"{itemName} deleted");
            }
            else
            {
                LogErrorObjectAction(confirmation.Message, $"{itemName}");
            }

            return Json(new { status = confirmation.WasSuccessful, message = confirmation.Message });
        }

        public JsonResult GetStatus(Guid id)
        {
            var status = _statusService.Get(id);
            return Json(status,JsonRequestBehavior.AllowGet);
        }
    }
}