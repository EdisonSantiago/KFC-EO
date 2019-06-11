using System;
using System.Web.Mvc;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Domain.Models.Personal;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;
using Oulanka.Web.Core.ViewModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class PosicionesController : BaseController
    {
        private readonly IEvaluacionService _evaluacionService;
        private readonly ICadenaService _cadenaService;
        private readonly IStatusService _statusService;

        public PosicionesController(
            IEvaluacionService evaluacionService, 
            IStatusService statusService, 
            ICadenaService cadenaService)
        {
            _evaluacionService = evaluacionService;
            _statusService = statusService;
            _cadenaService = cadenaService;
        }


        // GET
        public ActionResult Index()
        {
            var viewModel = new PosicionesViewModel
            {
                Estados = _statusService.GetItems(),
                Cadenas = _cadenaService.GetList()
            };
            return View(viewModel);
        }

        public JsonResult GetPosiciones(int page = 1, int limit = 10)
        {
            var items = _evaluacionService.GetPosicionPagedList(page > 0 ? page - 1 : page, limit);
            var records = items.Items;
            var total = items.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(PosicionFormModel posicion)
        {
            Posicion item = null;
            item = posicion.Id == Guid.Empty
                ? new Posicion()
                : _evaluacionService.GetPosicion(posicion.Id);

            item.Nombre = posicion.Nombre;
            item.Descripcion = posicion.Descripcion;
            item.Cadena = _cadenaService.Get(posicion.CadenaId);
            item.Estado = _statusService.Get(posicion.EstadoId);
            if (item.Id == Guid.Empty)
            {
                item.CreadoEn = DateTime.Now;
                item.CreadoPor = User.Identity.Name;
            }

            item.ActualizadoEn = DateTime.Now;
            item.ActualizadoPor = User.Identity.Name;

            var confirmation = _evaluacionService.SaveOrUpdatePosicion(item);
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult Delete(Guid id)
        {
            var item = _evaluacionService.GetPosicion(id);
            if (item == null) return Json(false);

            var itemName = item.Nombre + "|";
            var confirmation = _evaluacionService.DeletePosicion(item.Id);
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