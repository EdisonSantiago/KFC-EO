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
    public class RegionesController : BaseController
    {
        private readonly IUbicacionService _ubicacionService;
        private readonly IStatusService _statusService;

        public RegionesController(IUbicacionService ubicacionService, IStatusService statusService)
        {
            _ubicacionService = ubicacionService;
            _statusService = statusService;
        }

        // GET
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetRegiones(int page = 1, int limit = 10)
        {
            var regiones = _ubicacionService.GetRegionesPagedList(page > 0 ? page - 1 : page, limit);
            var records = regiones.Items;
            var total = regiones.TotalCount;

            return Json(
                new
                {
                    records,
                    total
                }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult New()
        {
            var formModel = new RegionFormModel();
            PopulateFormModelData(formModel, null);
            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(RegionFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var region = new Region
                {
                    Nombre = formModel.Nombre,
                    CreadoPor = User.Identity.Name,
                    ActualizadoPor = User.Identity.Name,
                    CreadoEn = DateTime.Now,
                    ActualizadoEn = DateTime.Now,
                    Estado = _statusService.Get(formModel.Estado)
                };

                var confirmation = _ubicacionService.SaveOrUpdateRegion(region);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Region creada con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "regiones");
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
            var region = _ubicacionService.GetRegion(id);

            var formModel = new RegionFormModel();
            PopulateFormModelData(formModel, region);

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegionFormModel formModel, Guid id)
        {
            if (ModelState.IsValid)
            {
                var dbRegion = _ubicacionService.GetRegion(id);
                dbRegion.Nombre = formModel.Nombre;
                dbRegion.Estado = _statusService.Get(formModel.Estado);

                dbRegion.ActualizadoEn = DateTime.Now;
                dbRegion.ActualizadoPor = User.Identity.Name;

                var confirmation = _ubicacionService.SaveOrUpdateRegion(dbRegion);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Region actualizada con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "regiones");
                }
                else
                {
                    this.AddPageMessage(confirmation.Message, PageMessageType.Error, false);
                }

            }

            PopulateFormModelData(formModel, null);

            return View(formModel);
        }

        private void PopulateFormModelData(RegionFormModel formModel, Region region)
        {
            if (region != null)
            {
                formModel.Nombre = region.Nombre;
                formModel.Estado = region.Estado.Id;
                formModel.Id = region.Id;
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
        }

    }
}