using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Locales;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class EquiposController : BaseController
    {
        private readonly IEquipoService _equipoService;
        private readonly IStatusService _statusService;

        public EquiposController(IEquipoService equipoService, IStatusService statusService)
        {
            _equipoService = equipoService;
            _statusService = statusService;
        }

        // GET
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEquipos(int page = 1, int limit = 10)
        {
            var equipos = _equipoService.GetPagedList(page > 0 ? page - 1 : page, limit);
            var records = equipos.Items;
            var total = equipos.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }


        public ActionResult New()
        {
            var formModel = new EquipoFormModel();
            PopulateFormModelData(formModel, null);

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(EquipoFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var equipo = new Equipo
                {
                    Modelo = formModel.Modelo,
                    Descripcion = formModel.Descripcion,
                    Utilidad = formModel.Utilidad,
                    Control = formModel.Control,
                    Cantidad = formModel.Cantidad,
                    CreadoPor = User.Identity.Name,
                    CreadoEn = DateTime.Now,
                    ActualizadoPor = User.Identity.Name,
                    ActualizadoEn = DateTime.Now,
                    Estado = _statusService.Get(formModel.Estado),
                    TipoEquipo = _equipoService.GetTipoEquipo(formModel.TipoEquipo),
                };


                var confirmation = _equipoService.SaveOrUpdate(equipo);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Equipo creada con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "equipos");
                }
                else
                {
                    this.AddPageMessage(confirmation.Message, PageMessageType.Error, false);
                }
            }
            
            PopulateFormModelData(formModel, null);
            return View(formModel);
        }


        public ActionResult Edit(Guid id)
        {
            var equipo = _equipoService.Get(id);
            var formModel = new EquipoFormModel();
            PopulateFormModelData(formModel, equipo);

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, EquipoFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var dbEquipo = _equipoService.Get(id);
                dbEquipo.Modelo = formModel.Modelo;
                dbEquipo.Descripcion = formModel.Descripcion;
                dbEquipo.Utilidad = formModel.Utilidad;
                dbEquipo.Control = formModel.Control;
                dbEquipo.Cantidad = formModel.Cantidad;
                dbEquipo.Estado = _statusService.Get(formModel.Estado);
                dbEquipo.TipoEquipo = _equipoService.GetTipoEquipo(formModel.TipoEquipo);

                dbEquipo.ActualizadoPor = User.Identity.Name;
                dbEquipo.ActualizadoEn = DateTime.Now;

                var confirmation = _equipoService.SaveOrUpdate(dbEquipo);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Equipo actualizado con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "equipos");
                }
                else
                {
                    this.AddPageMessage(confirmation.Message, PageMessageType.Error, false);
                }
            }

            PopulateFormModelData(formModel, null);
            return View(formModel);
        }

        public JsonResult Delete(Guid id)
        {
            var item = _equipoService.Get(id);
            if (item == null) return Json(false);

            var itemName = item.Modelo + "|";
            var confirmation = _equipoService.Delete(item.Id);
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

        private void PopulateFormModelData(EquipoFormModel formModel, Equipo equipo)
        {
            if (equipo != null)
            {
                formModel.Id = equipo.Id;
                formModel.Modelo = equipo.Modelo;
                formModel.Descripcion = equipo.Descripcion;
                formModel.Cantidad = equipo.Cantidad;
                formModel.Control = equipo.Control;
                formModel.Utilidad = equipo.Utilidad;
                formModel.Estado = equipo.Estado.Id;
                formModel.TipoEquipo = equipo.TipoEquipo.Id;
            }

            var estadoItems = new List<SelectListItem>();
            var estadoTypes = _statusService.GetItems(GrupoStatus.Global);

            foreach (var estadoType in estadoTypes)
            {
                var selectItem = new SelectListItem
                {
                    Text = estadoType.Nombre,
                    Value = estadoType.Id.ToString()
                };

                if (estadoType.Id == formModel.Estado)
                {
                    selectItem.Selected = true;
                }

                estadoItems.Add(selectItem);
            }
            formModel.Estados = estadoItems;

            var tipoItems = new List<SelectListItem>();
            var tipoTypes = _equipoService.GetTipoEquipoList();

            foreach (var tipo in tipoTypes)
            {
                var selectItem = new SelectListItem
                {
                    Text = tipo.Nombre,
                    Value = tipo.Id.ToString()
                };

                if (tipo.Id == formModel.Estado)
                {
                    selectItem.Selected = true;
                }

                tipoItems.Add(selectItem);
            }
            formModel.TiposEquipos = tipoItems;
        }

        #region Tipos Equipos

        public ActionResult Tipos()
        {
            var estados = _statusService.GetItems();
            return View(estados);
        }

        public JsonResult GetTipos(int page = 1, int limit = 10)
        {
            var tipos = _equipoService.GetTipoEquipoPagedList(page > 0 ? page - 1 : page, limit);
            var records = tipos.Items;
            var total = tipos.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveTipo(TipoEquipoFormModel tipo)
        {
            TipoEquipo tipoEquipo = null;
            tipoEquipo = tipo.Id == Guid.Empty
                ? new TipoEquipo()
                : _equipoService.GetTipoEquipo(tipo.Id);

            tipoEquipo.Nombre = tipo.Nombre;
            tipoEquipo.Descripcion = tipo.Descripcion;
            tipoEquipo.Estado = _statusService.Get(tipo.EstadoId);
            if (tipoEquipo.Id == Guid.Empty)
            {
                tipoEquipo.CreadoEn = DateTime.Now;
                tipoEquipo.CreadoPor = User.Identity.Name;
            }

            tipoEquipo.ActualizadoEn = DateTime.Now;
            tipoEquipo.ActualizadoPor = User.Identity.Name;

            var confirmation = _equipoService.SaveOrUpdateTipoEquipo(tipoEquipo);
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult DeleteTipo(Guid id)
        {
            var item = _equipoService.GetTipoEquipo(id);
            if (item == null) return Json(false);

            var itemName = item.Nombre + "|";
            var confirmation = _equipoService.DeleteTipoEquipo(item.Id);
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