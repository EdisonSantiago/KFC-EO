using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Ubicacion;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;
using WebGrease.Activities;

namespace Oulanka.Web.Mvc.Controllers
{
    public class CadenasController : BaseController
    {
        private readonly ICadenaService _cadenaService;
        private readonly IStatusService _statusService;
        private readonly IBlobImageService _blobImageService;

        public CadenasController(
            ICadenaService cadenaService,
            IStatusService statusService,
            IBlobImageService blobImageService)
        {
            _cadenaService = cadenaService;
            _statusService = statusService;
            _blobImageService = blobImageService;
        }

        // GET
        public ActionResult Index()
        {
            return View();

        }

        public JsonResult GetCadenas(int page = 1, int limit = 10)
        {
            var cadenas = _cadenaService.GetPagedList(page > 0 ? page - 1 : page, limit);
            var records = cadenas.Items;
            var total = cadenas.TotalCount;

            return Json(
                new { records, total }
            , JsonRequestBehavior.AllowGet);
        }

        public ActionResult New()
        {
            var formModel = new CadenaFormModel();
            PopulateFormModelData(formModel, null);

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(CadenaFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var cadena = new Cadena
                {
                    Nombre = formModel.Nombre,
                    Descripcion = formModel.Descripcion,
                    FechaFundacion = formModel.FechaFundacion,
                    CreadoPor = User.Identity.Name,
                    CreadoEn = DateTime.Now,
                    ActualizadoPor = User.Identity.Name,
                    ActualizadoEn = DateTime.Now,
                    Estado = _statusService.Get(formModel.Estado),
                };

                if (formModel.Logo != null && formModel.Logo.ContentLength > 0)
                {
                    var imageUrl = _blobImageService.UploadImage(formModel.Logo, BlobContainers.General());
                    cadena.Logo = imageUrl;
                }

                if (formModel.Manual != null && formModel.Manual.ContentLength > 0)
                {
                    var manualUrl = _blobImageService.UploadImage(formModel.Manual, BlobContainers.General());
                    cadena.Manual = manualUrl;
                }


                var confirmation = _cadenaService.SaveOrUpdate(cadena);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Cadena creada con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "cadenas");
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
            var cadena = _cadenaService.Get(id);
            var formModel = new CadenaFormModel();
            PopulateFormModelData(formModel, cadena);

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, CadenaFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var dbCadena = _cadenaService.Get(id);
                dbCadena.Nombre = formModel.Nombre;
                dbCadena.Descripcion = formModel.Descripcion;
                dbCadena.FechaFundacion = formModel.FechaFundacion;

                dbCadena.ActualizadoPor = User.Identity.Name;
                dbCadena.ActualizadoEn = DateTime.Now;

                if (formModel.Logo != null && formModel.Logo.ContentLength > 0)
                {
                    var imageUrl = _blobImageService.UploadImage(formModel.Logo, BlobContainers.General());
                    dbCadena.Logo = imageUrl;
                }

                if (formModel.Manual != null && formModel.Manual.ContentLength > 0)
                {
                    var manualUrl = _blobImageService.UploadImage(formModel.Manual, BlobContainers.General());
                    dbCadena.Manual = manualUrl;
                }
                    
                var confirmation = _cadenaService.SaveOrUpdate(dbCadena);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Cadena actualizada con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "cadenas");
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
            var item = _cadenaService.Get(id);
            if (item == null) return Json(false);

            var itemName = item.Nombre + "|";
            var confirmation = _cadenaService.Delete(item.Id);
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

        private void PopulateFormModelData(CadenaFormModel formModel, Cadena cadena)
        {
            if (cadena != null)
            {
                formModel.Id = cadena.Id;
                formModel.Nombre = cadena.Nombre;
                formModel.Descripcion = cadena.Descripcion;
                formModel.Estado = cadena.Estado.Id;
                formModel.FechaFundacion = cadena.FechaFundacion;

                formModel.LogoUrl = cadena.Logo;
                formModel.ManualUrl = cadena.Manual;
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