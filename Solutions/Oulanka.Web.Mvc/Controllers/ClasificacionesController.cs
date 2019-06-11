using System;
using System.Web.Mvc;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Locales;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class ClasificacionesController : BaseController
    {
        private readonly IEstandarService _estandarService;
        private IStatusService _statusService;

        public ClasificacionesController(IEstandarService estandarService, IStatusService statusService)
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


        public JsonResult GetClasificaciones(int page = 1, int limit = 10)
        {
            var items = _estandarService.GetClasificacionPagedList(page > 0 ? page - 1 : page, limit);
            var records = items.Items;
            var total = items.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(ClasificacionFormModel clasificacion)
        {
            var item = clasificacion.Id == Guid.Empty
                ? new Clasificacion()
                : _estandarService.GetClasificacion(clasificacion.Id);

            item.Nombre = clasificacion.Nombre;
            item.Descripcion = clasificacion.Descripcion;
            item.Estado = _statusService.Get(clasificacion.EstadoId);

            if (item.Id == Guid.Empty)
            {
                item.CreadoEn = DateTime.Now;
                item.CreadoPor = User.Identity.Name;
            }

            item.ActualizadoEn = DateTime.Now;
            item.ActualizadoPor = User.Identity.Name;

            var confirmation = _estandarService.SaveOrUpdateClasificacion(item);
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult Delete(Guid id)
        {
            var item = _estandarService.GetClasificacion(id);
            if (item == null) return Json(false);

            var itemName = item.Nombre + "|";
            var confirmation = _estandarService.DeleteClasificacion(item.Id);
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