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
    public class CiudadesController : BaseController
    {
        private readonly IUbicacionService _ubicacionService;
        private readonly IStatusService _statusService;

        public CiudadesController(IUbicacionService ubicacionService, IStatusService statusService)
        {
            _ubicacionService = ubicacionService;
            _statusService = statusService;
        }

        // GET
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCiudades(int page = 1, int limit = 10)
        {
            var ciudades = _ubicacionService.GetCiudadesPagedList(page > 0 ? page - 1 : page, limit);
            var records = ciudades.Items;
            var total = ciudades.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }

        public ActionResult New()
        {
            var formModel = new CiudadFormModel();
            PopulateFormModelData(formModel, null);

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(CiudadFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var ciudad = new Ciudad
                {
                    Nombre = formModel.Nombre,
                    CreadoPor = User.Identity.Name,
                    CreadoEn = DateTime.Now,
                    ActualizadoPor = User.Identity.Name,
                    ActualizadoEn = DateTime.Now,
                    Estado = _statusService.Get(formModel.Estado),
                    Provincia = _ubicacionService.GetProvincia(formModel.Provincia)
                };

                var confirmation = _ubicacionService.SaveOrUpdateCiudad(ciudad);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Ciudad creada con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "ciudades");
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
            var ciudad = _ubicacionService.GetCiudad(id);

            var formModel = new CiudadFormModel();
            PopulateFormModelData(formModel, ciudad);

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CiudadFormModel formModel, Guid id)
        {
            if (ModelState.IsValid)
            {
                var dbCiudad = _ubicacionService.GetCiudad(id);
                dbCiudad.Nombre = formModel.Nombre;
                dbCiudad.Estado = _statusService.Get(formModel.Estado);
                dbCiudad.Provincia = _ubicacionService.GetProvincia(formModel.Provincia);

                dbCiudad.ActualizadoPor = User.Identity.Name;
                dbCiudad.ActualizadoEn = DateTime.Now;

                var confirmation = _ubicacionService.SaveOrUpdateCiudad(dbCiudad);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Ciudad actualizada con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "ciudades");
                }
                else
                {
                    this.AddPageMessage(confirmation.Message, PageMessageType.Error, false);
                }

                
            }

            PopulateFormModelData(formModel, null);
            return View(formModel);
        }

        private void PopulateFormModelData(CiudadFormModel formModel, Ciudad ciudad)
        {
            if (ciudad != null)
            {
                formModel.Id = ciudad.Id;
                formModel.Nombre = ciudad.Nombre;
                formModel.Estado = ciudad.Estado.Id;
                formModel.Provincia = ciudad.Provincia.Id;
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


            var provinciasItems = new List<SelectListItem>();
            var provinciasTypes = _ubicacionService.GetProvincias();

            foreach (var provincia in provinciasTypes)
            {
                var selectItem = new SelectListItem
                {
                    Text = provincia.Nombre,
                    Value = provincia.Id.ToString()
                };

                if (provincia.Id == formModel.Provincia)
                {
                    selectItem.Selected = true;
                }
                provinciasItems.Add(selectItem);
            }

            formModel.Provincias = provinciasItems;
        }

    }
}
