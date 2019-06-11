using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NHibernate.Util;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models.Jerarquias;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;
using Oulanka.Web.Core.ViewModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class JerarquiasController : BaseController
    {
        private readonly IJerarquiaService _jerarquiaService;
        private readonly ICadenaService _cadenaService;
        private readonly IStatusService _statusService;

        public JerarquiasController(
            IJerarquiaService jerarquiaService,
            IStatusService statusService,
            ICadenaService cadenaService)
        {
            _jerarquiaService = jerarquiaService;
            _statusService = statusService;
            _cadenaService = cadenaService;
        }

        // GET
        public ActionResult Index()
        {
            var viewModel = new JerarquiaViewModel()
            {
                GerenteGeneral = _jerarquiaService.GetGeneral(),
                Estados = _statusService.GetItems(GrupoStatus.Global),
                Cadenas = _cadenaService.GetList()
            };

            return View(viewModel);
        }

        #region JSON Results 

        public JsonResult GetGenerales(int page = 1, int limit = 10)
        {
            var items = _jerarquiaService.GetGeneralPagedList(page > 0 ? page - 1 : page, limit);
            var records = items.Items;
            var total = items.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNodeType(Guid id)
        {
            var nodeType = "";
            var isRoot = false;
            var actions = new List<string>();
            var parentId = Guid.Empty;
            var nodeId = Guid.Empty;

            var general = _jerarquiaService.GetGeneral(id);
            if (general != null)
            {
                nodeId = general.Id;
                nodeType = "general";
                isRoot = true;
                actions.Add("edit");
            }
            else if (_jerarquiaService.GetNacional(id) != null)
            {
                var nacional = _jerarquiaService.GetNacional(id);
                nodeId = nacional.Id;
                nodeType = "nacional";
                parentId = nacional.GerenteGeneral.Id;
                isRoot = false;
                actions.Add("edit");
                actions.Add("delete");
            }
            else if (_jerarquiaService.GetRegional(id) != null)
            {
                var regional = _jerarquiaService.GetRegional(id);
                nodeId = regional.Id;
                nodeType = "regional";
                parentId = regional.GerenteNacional.Id;
                isRoot = false;
                actions.Add("edit");
                actions.Add("delete");
            }
            return Json(new { nodeId = nodeId, parentId, nodeType, isRoot, actions }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Gerente Nacional

        public JsonResult GetNacional(Guid id)
        {
            var nacional = _jerarquiaService.GetNacional(id);
            return Json(new { nacional }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNacionalesAll()
        {
            var general = _jerarquiaService.GetGeneral();
            var nacionales = general.GerentesNacionales;


            return Json(new { nacionales }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetNacionales(string parentId, int page = 1, int limit = 10)
        {
            var parentGuid = Guid.Parse(parentId);
            var items = _jerarquiaService.GetNacionalesPagedList(parentGuid, page > 0 ? page - 1 : page, limit);
            var records = items.Items;
            var total = items.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveNacional(GerenteNacionalFormModel nacional)
        {
            var gerenteNacional = nacional.Id == Guid.Empty
                ? new GerenteNacional()
                : _jerarquiaService.GetNacional(nacional.Id);

            gerenteNacional.Nombre = nacional.Nombre;
            gerenteNacional.Descripcion = nacional.Descripcion;
            gerenteNacional.GerenteGeneral = _jerarquiaService.GetGeneral();
            gerenteNacional.Cadena = _cadenaService.Get(nacional.CadenaId);
            gerenteNacional.Estado = _statusService.Get(nacional.EstadoId);
            if (gerenteNacional.Id == Guid.Empty)
            {
                gerenteNacional.CreadoEn = DateTime.Now;
                gerenteNacional.CreadoPor = User.Identity.Name;
            }

            gerenteNacional.ActualizadoEn = DateTime.Now;
            gerenteNacional.ActualizadoPor = User.Identity.Name;

            var confirmation = _jerarquiaService.SaveOrUpdateNacional(gerenteNacional);
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult DeleteNacional(Guid id)
        {
            var item = _jerarquiaService.GetNacional(id);
            if (item == null) return Json(false);

            var itemName = item.Nombre + "|";
            var confirmation = _jerarquiaService.DeleteNacional(item.Id);
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

        #region Gerente Regional

        public JsonResult GetRegional(Guid id)
        {
            var regional = _jerarquiaService.GetRegional(id);
            return Json(new { regional }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRegionalesAll()
        {
            var general = _jerarquiaService.GetGeneral();
            var nacionales = general.GerentesNacionales;

            var regionales = new List<GerenteRegional>();

            foreach (var gerenteNacional in nacionales)
            {
                if (gerenteNacional.GerentesRegionales.Any())
                {
                    regionales.AddRange(gerenteNacional.GerentesRegionales);
                }
            }

            return Json(new { regionales }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRegionales(string parentId, int page = 1, int limit = 10)
        {
            var parentGuid = Guid.Parse(parentId);
            var items = _jerarquiaService.GetRegionalesPagedList(parentGuid, page > 0 ? page - 1 : page, limit);
            var records = items.Items;
            var total = items.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveRegional(GerenteRegionalFormModel regional)
        {
            var gerenteRegional = regional.Id == Guid.Empty
                ? new GerenteRegional()
                : _jerarquiaService.GetRegional(regional.Id);

            gerenteRegional.Nombre = regional.Nombre;
            gerenteRegional.Descripcion = regional.Descripcion;
            gerenteRegional.Estado = _statusService.Get(regional.EstadoId);
            gerenteRegional.GerenteNacional = _jerarquiaService.GetNacional(regional.NacionalId);
            if (gerenteRegional.Id == Guid.Empty)
            {
                gerenteRegional.CreadoEn = DateTime.Now;
                gerenteRegional.CreadoPor = User.Identity.Name;
            }

            gerenteRegional.ActualizadoEn = DateTime.Now;
            gerenteRegional.ActualizadoPor = User.Identity.Name;

            var confirmation = _jerarquiaService.SaveOrUpdateRegional(gerenteRegional);
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult DeleteRegional(Guid id)
        {
            var item = _jerarquiaService.GetRegional(id);
            if (item == null) return Json(false);

            var itemName = item.Nombre + "|";
            var confirmation = _jerarquiaService.DeleteRegional(item.Id);
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

        #region JefeArea

        public JsonResult GetGerenciasTree()
        {
            var nodeList = new List<OulankaTreeNode>();
            var general = _jerarquiaService.GetGeneral();

            var generalNode = new OulankaTreeNode
            {
                Id = general.Id,
                Text = $"<strong>GG</strong> {general.Nombre}",
                NodeType = "general"
            };

            var nacionales = _jerarquiaService.GetNacionalesList(general.Id);
            foreach (var nacional in nacionales)
            {
                var nacionalNode = new OulankaTreeNode
                {
                    Id = nacional.Id,
                    Text = $"<strong>GN</strong> {nacional.Nombre} ({nacional.CadenaNombre})",
                    NodeType = "nacional",
                    ParentId = general.Id
                };

                var regionales = _jerarquiaService.GetRegionalesList(nacional.Id);
                foreach (var regional in regionales)
                {
                    var regionalNode = new OulankaTreeNode
                    {
                        Id = regional.Id,
                        Text = $"<strong>GR</strong> {regional.Nombre}",
                        NodeType = "regional",
                        ParentId = nacional.Id
                    };

                    nacionalNode.Nodes.Add(regionalNode);
                }

                generalNode.Nodes.Add(nacionalNode);
            }

            nodeList.Add(generalNode);

            return Json(nodeList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJefesAreas(string parentId = "", int page = 1, int limit = 10)
        {
            var parentGuid = !string.IsNullOrEmpty(parentId)
                ? Guid.Parse(parentId)
                : _jerarquiaService.GetGeneral().Id;

            var items = _jerarquiaService.GetJefesAreasPagedList(parentGuid, page > 0 ? page - 1 : page, limit);
            var records = items.Items;
            var total = items.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveGeneral(GerenteGeneralFormModel general)
        {
            var gerenteGeneral = general.Id == Guid.Empty
                ? new GerenteGeneral()
                : _jerarquiaService.GetGeneral(general.Id);

            gerenteGeneral.Nombre = general.Nombre;
            gerenteGeneral.Descripcion = general.Descripcion;
            gerenteGeneral.Estado = _statusService.Get(general.EstadoId);
            if (gerenteGeneral.Id == Guid.Empty)
            {
                gerenteGeneral.CreadoEn = DateTime.Now;
                gerenteGeneral.CreadoPor = User.Identity.Name;
            }

            gerenteGeneral.ActualizadoEn = DateTime.Now;
            gerenteGeneral.ActualizadoPor = User.Identity.Name;

            var confirmation = _jerarquiaService.SaveOrUpdateGeneral(gerenteGeneral);
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult SaveJefeArea(JefeAreaFormModel jefe)
        {
            var jefeArea = jefe.Id == Guid.Empty
                ? new JefeArea()
                : _jerarquiaService.GetJefeArea(jefe.Id);

            jefeArea.Nombre = jefe.Nombre;
            jefeArea.Descripcion = jefe.Descripcion;
            jefeArea.Estado = _statusService.Get(jefe.EstadoId);
            jefeArea.GerenteRegional = _jerarquiaService.GetRegional(jefe.RegionalId);

            if (jefeArea.Id == Guid.Empty)
            {
                jefeArea.CreadoEn = DateTime.Now;
                jefeArea.CreadoPor = User.Identity.Name;
            }

            jefeArea.ActualizadoEn = DateTime.Now;
            jefeArea.ActualizadoPor = User.Identity.Name;

            var confirmation = _jerarquiaService.SaveOrUpdateJefeArea(jefeArea);
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult DeleteJefeArea(Guid id)
        {
            var item = _jerarquiaService.GetJefeArea(id);
            if (item == null) return Json(false);

            var itemName = item.Nombre + "|";
            var confirmation = _jerarquiaService.DeleteJefeArea(item.Id);
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