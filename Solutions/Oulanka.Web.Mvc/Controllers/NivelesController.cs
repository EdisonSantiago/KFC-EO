using System;
using System.Web.Mvc;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class NivelesController : BaseController
    {
        private readonly IEstandarService _estandarService;
        private IStatusService _statusService;

        public NivelesController(IEstandarService estandarService, IStatusService statusService)
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


        public JsonResult GetNiveles(int page = 1, int limit = 10)
        {
            var items = _estandarService.GetNivelPagedList(page > 0 ? page - 1 : page, limit);
            var records = items.Items;
            var total = items.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(NivelFormModel nivel)
        {
            var item = nivel.Id == Guid.Empty
                ? new Nivel()
                : _estandarService.GetNivel(nivel.Id);

            item.Nombre = nivel.Nombre;
            item.Descripcion = nivel.Descripcion;
            item.Estado = _statusService.Get(nivel.EstadoId);

            if (item.Id == Guid.Empty)
            {
                item.CreadoEn = DateTime.Now;
                item.CreadoPor = User.Identity.Name;
            }

            item.ActualizadoEn = DateTime.Now;
            item.ActualizadoPor = User.Identity.Name;

            var confirmation = _estandarService.SaveOrUpdateNivel(item);
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult Delete(Guid id)
        {
            var item = _estandarService.GetNivel(id);
            if (item == null) return Json(false);

            var itemName = item.Nombre + "|";
            var confirmation = _estandarService.DeleteNivel(item.Id);
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

    }


}