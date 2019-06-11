using System;
using System.Web.Mvc;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class CategoriasController : BaseController
    {
        private readonly IEstandarService _estandarService;
        private IStatusService _statusService;

        public CategoriasController(IEstandarService estandarService, IStatusService statusService)
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


        public JsonResult GetCategorias(int page = 1, int limit = 10)
        {
            var items = _estandarService.GetCategoriaPagedList(page > 0 ? page - 1 : page, limit);
            var records = items.Items;
            var total = items.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(CategoriaFormModel categoria)
        {
            var item  = categoria.Id == Guid.Empty
                ? new Categoria()
                : _estandarService.GetCategoria(categoria.Id);

            item.Nombre = categoria.Nombre;
            item.Estado = _statusService.Get(categoria.EstadoId);
            if (item .Id == Guid.Empty)
            {
                item .CreadoEn = DateTime.Now;
                item .CreadoPor = User.Identity.Name;
            }

            item .ActualizadoEn = DateTime.Now;
            item .ActualizadoPor = User.Identity.Name;

            var confirmation = _estandarService.SaveOrUpdateCategoria(item );
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult Delete(Guid id)
        {
            var item = _estandarService.GetCategoria(id);
            if (item == null) return Json(false);

            var itemName = item.Nombre + "|";
            var confirmation = _estandarService.DeleteCategoria(item.Id);
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