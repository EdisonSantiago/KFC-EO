using System;
using System.Web.Mvc;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Locales;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class CalificacionesController : BaseController
    {
        private readonly IEstandarService _estandarService;
        private IStatusService _statusService;

        public CalificacionesController(IEstandarService estandarService, IStatusService statusService)
        {
            _estandarService = estandarService;
            _statusService = statusService;
        }

        // GET
        public ActionResult Index()
        {
            var estados = _statusService.GetItems();
            return View(estados);
        }


        public JsonResult GetCalificaciones(int page = 1, int limit = 10)
        {
            var items = _estandarService.GetCalificacionesPagedList(page > 0 ? page - 1 : page, limit);
            var records = items.Items;
            var total = items.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(CalificacionFormModel calificacion)
        {
            var item = calificacion.Id == Guid.Empty
                ? new Calificacion()
                : _estandarService.GetCalificacion(calificacion.Id);

            item.Nombre = calificacion.Nombre;
            item.Descripcion = calificacion.Descripcion;
            item.Estado = _statusService.Get(calificacion.EstadoId);
            if (item.Id == Guid.Empty)
            {
                item.CreadoEn = DateTime.Now;
                item.CreadoPor = User.Identity.Name;
            }

            item.ActualizadoEn = DateTime.Now;
            item.ActualizadoPor = User.Identity.Name;

            var confirmation = _estandarService.SaveOrUpdateCalificacion(item);
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult Delete(Guid id)
        {
            var item = _estandarService.GetCalificacion(id);
            if (item == null) return Json(false);

            var itemName = item.Nombre + "|";
            var confirmation = _estandarService.DeleteCalificacion(item.Id);
            if (confirmation.WasSuccessful)
            {
                LogSaveObjectAction(savedObject: $"{itemName} deleted");
                return Json(new { status = confirmation.WasSuccessful, message = confirmation.Message });
            }
            else
            {
                LogErrorObjectAction(confirmation.Message, $"{itemName}");
            }

            return Json(new { status = confirmation.WasSuccessful, message = confirmation.Message });
        }

    }


}