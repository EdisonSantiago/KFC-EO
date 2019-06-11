using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;
using Oulanka.Web.Core.ViewModels;
using SharpArchContrib.Core.MultiTenant;

namespace Oulanka.Web.Mvc.Controllers
{
    public class OpcionesController : BaseController
    {
        private readonly IEstandarService _estandarService;
        private IStatusService _statusService;

        public OpcionesController(IEstandarService estandarService, IStatusService statusService)
        {
            _estandarService = estandarService;
            _statusService = statusService;
        }

        // GET
        public ActionResult Index()
        {
            var estados = _statusService.GetItems();
            var tipoOpciones = new Dictionary<int, string>();
            foreach (var name in Enum.GetNames(typeof(TipoOpcion)))
            {
                tipoOpciones.Add((int)Enum.Parse(typeof(TipoOpcion), name), name);
            }

            var viewModel = new OpcionesViewModel
            {
                Estados = estados,
                TipoOpciones = tipoOpciones
            };

            return View(viewModel);
        }


        public JsonResult GetOpciones(int page = 1, int limit = 10)
        {
            var items = _estandarService.GetOpcionPagedList(page > 0 ? page - 1 : page, limit);
            var records = items.Items;
            var total = items.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(OpcionFormModel opcion)
        {
            var item = opcion.Id == Guid.Empty
                ? new Opcion()
                : _estandarService.GetOpcion(opcion.Id);

            item.Nombre = opcion.Nombre;
            item.Etiqueta = opcion.Etiqueta;
            item.Valor = opcion.Valor;
            item.TipoOpcion = opcion.TipoOpcion;
            item.Estado = _statusService.Get(opcion.EstadoId);

            if (item.Id == Guid.Empty)
            {
                item.CreadoEn = DateTime.Now;
                item.CreadoPor = User.Identity.Name;
            }

            item.ActualizadoEn = DateTime.Now;
            item.ActualizadoPor = User.Identity.Name;

            var confirmation = _estandarService.SaveOrUpdateOpcion(item);
            if (confirmation.WasSuccessful)
            {
                LogSaveObjectAction(savedObject: $"{item.Nombre} saved");
            }
            else
            {
                LogErrorObjectAction(confirmation.Message, $"{item.Nombre}");
            }
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult Delete(Guid id)
        {
            var item = _estandarService.GetOpcion(id);
            if (item == null) return Json(false);

            var itemName = item.Nombre + "|";
            var confirmation = _estandarService.DeleteOpcion(item.Id);
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