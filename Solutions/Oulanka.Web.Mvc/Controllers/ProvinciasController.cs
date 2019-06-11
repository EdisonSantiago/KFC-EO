using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Ubicacion;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class ProvinciasController : BaseController
    {
        private readonly IUbicacionService _ubicacionService;
        private readonly IStatusService _statusService;


        public ProvinciasController(IUbicacionService ubicacionService, IStatusService statusService)
        {
            _ubicacionService = ubicacionService;
            _statusService = statusService;
        }

        // GET
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProvincias(int page = 1, int limit = 10)
        {
            var provincias = _ubicacionService.GetProvinciasPagedList(page > 0 ? page - 1 : page, limit);
            var records = provincias.Items;
            var total = provincias.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }

        public ActionResult New()
        {
            var formModel = new ProvinciaFormModel();
            PopulateFormModelData(formModel, null);

            return View(formModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(ProvinciaFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var provincia = new Provincia
                {
                    Nombre = formModel.Nombre,
                    CreadoPor = User.Identity.Name,
                    ActualizadoPor = User.Identity.Name,
                    CreadoEn = DateTime.Now,
                    ActualizadoEn = DateTime.Now,
                    Estado = _statusService.Get(formModel.Estado),
                    Region = _ubicacionService.GetRegion(formModel.Region)
                };

                var confirmation = _ubicacionService.SaveOrUpdateProvincia(provincia);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Provincia creada con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "provincias");
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
            var provincia = _ubicacionService.GetProvincia(id);

            var formModel = new ProvinciaFormModel();
            PopulateFormModelData(formModel, provincia);

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProvinciaFormModel formModel, Guid id)
        {
            if (ModelState.IsValid)
            {
                var dbProvincia = _ubicacionService.GetProvincia(id);
                dbProvincia.Nombre = formModel.Nombre;
                dbProvincia.Estado = _statusService.Get(formModel.Estado);
                dbProvincia.Region = _ubicacionService.GetRegion(formModel.Region);

                dbProvincia.ActualizadoPor = User.Identity.Name;
                dbProvincia.ActualizadoEn = DateTime.Now;

                var confirmation = _ubicacionService.SaveOrUpdateProvincia(dbProvincia);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Provincia actualizada con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "provincias");
                }
                else
                {
                    this.AddPageMessage(confirmation.Message, PageMessageType.Error, false);
                }
            }

            PopulateFormModelData(formModel, null);

            return View(formModel);
        }

        private void PopulateFormModelData(ProvinciaFormModel formModel, Provincia provincia)
        {
            if (provincia != null)
            {
                formModel.Id = provincia.Id;
                formModel.Nombre = provincia.Nombre;
                formModel.Estado = provincia.Estado.Id;
                formModel.Region = provincia.Region.Id;
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


            var regionesItems = new List<SelectListItem>();
            var regionesTypes = _ubicacionService.GetRegiones();

            foreach (var regionType in regionesTypes)
            {
                var selectItem = new SelectListItem
                {
                    Text = regionType.Nombre,
                    Value = regionType.Id.ToString()
                };

                if (regionType.Id == formModel.Region)
                {
                    selectItem.Selected = true;
                }
                regionesItems.Add(selectItem);
            }

            formModel.Regiones = regionesItems;
        }

    }
}