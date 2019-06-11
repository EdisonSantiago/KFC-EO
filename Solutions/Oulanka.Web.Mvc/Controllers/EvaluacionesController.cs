using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Domain.Models.Locales;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;
using Oulanka.Web.Core.ViewModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class EvaluacionesController : BaseController
    {
        private readonly IEvaluacionService _evaluacionService;
        private readonly ICadenaService _cadenaService;
        private readonly ILocalService _localService;
        private readonly IStatusService _statusService;

        public EvaluacionesController(
            IEvaluacionService evaluacionService,
            IStatusService statusService,
            ICadenaService cadenaService,
            ILocalService localService)
        {
            _evaluacionService = evaluacionService;
            _statusService = statusService;
            _cadenaService = cadenaService;
            _localService = localService;
        }

        // GET
        public ActionResult Index()
        {
            var estados = _statusService.GetItems();
            return View(estados);
        }

        public ActionResult Show(Guid id)
        {
            var viewModel = new EvaluacionViewModel();
            var evaluacion = _evaluacionService.Get(id);
            viewModel.Evaluacion = evaluacion;

            return View(viewModel);
        }

        public JsonResult Gethistorial(Guid? parent, int page = 1, int limit = 10)
        {
            PagedList<EvaluacionDto> items = new PagedList<EvaluacionDto>();
            var records = new List<EvaluacionDto>();
            long total = 0;

            if (parent.HasValue)
            {
                var parentId = parent.Value;

                var cadena = _cadenaService.Get(parentId);
                if (cadena != null)
                {
                    items = _evaluacionService.GetByCadenaPagedList(cadena.Id, page > 0 ? page - 1 : page, limit);
                }
                else
                {
                    var local = _localService.Get(parentId);
                    if (local != null)
                    {
                        items = _evaluacionService.GetByLocalPagedList(local.Id, page > 0 ? page - 1 : page, limit);
                    }
                }

                if (items != null)
                {
                    records = items.Items;
                    total = items.TotalCount;
                }
            }

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTree()
        {
            var nodeList = new List<OulankaTreeNode>();

            var cadenas = _cadenaService.GetList().OrderBy(x => x.Nombre);
            foreach (var cadena in cadenas)
            {
                var node = new OulankaTreeNode
                {
                    Id = cadena.Id,
                    Text = cadena.Nombre,
                    NodeType = "cadena"
                };

                var locales = _localService.GetList(cadena.Id);
                foreach (var local in locales)
                {
                    node.Nodes.Add(new OulankaTreeNode
                    {
                        Id = local.Id,
                        Text = local.Nombre,
                        ParentId = cadena.Id,
                        NodeType = "local"
                    });
                }

                nodeList.Add(node);
            }


            return Json(nodeList, JsonRequestBehavior.AllowGet);
        }


        #region Tipos Evaluacion

        public ActionResult Tipos()
        {
            var estados = _statusService.GetItems();
            return View(estados);
        }

        public JsonResult GetTipos(int page = 1, int limit = 10)
        {
            var tipos = _evaluacionService.GetTipoEvaluacionPagedList(page > 0 ? page - 1 : page, limit);
            var records = tipos.Items;
            var total = tipos.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveTipo(TipoEvaluacionFormModel tipo)
        {
            TipoEvaluacion tipoEvaluacion = null;
            tipoEvaluacion = tipo.Id == Guid.Empty
                ? new TipoEvaluacion()
                : _evaluacionService.GetTipoEvaluacion(tipo.Id);

            tipoEvaluacion.Nombre = tipo.Nombre;
            tipoEvaluacion.Descripcion = tipo.Descripcion;
            tipoEvaluacion.Observaciones = tipo.Observaciones;
            tipoEvaluacion.Estado = _statusService.Get(tipo.EstadoId);
            if (tipoEvaluacion.Id == Guid.Empty)
            {
                tipoEvaluacion.CreadoEn = DateTime.Now;
                tipoEvaluacion.CreadoPor = User.Identity.Name;
            }

            tipoEvaluacion.ActualizadoEn = DateTime.Now;
            tipoEvaluacion.ActualizadoPor = User.Identity.Name;

            var confirmation = _evaluacionService.SaveOrUpdateTipoEvaluacion(tipoEvaluacion);
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult DeleteTipo(Guid id)
        {
            var item = _evaluacionService.GetTipoEvaluacion(id);
            if (item == null) return Json(false);

            var itemName = item.Nombre + "|";
            var confirmation = _evaluacionService.DeleteTipoEvaluacion(item.Id);
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


        #endregion


    }


}