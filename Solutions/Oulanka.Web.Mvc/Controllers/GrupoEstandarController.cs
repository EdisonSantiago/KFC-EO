using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Locales;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;
using Oulanka.Web.Core.Models;

namespace Oulanka.Web.Mvc.Controllers
{
    public class GrupoEstandarController : BaseController
    {
        private readonly IEstandarService _estandarService;
        private readonly ILocalService _localService;
        private readonly IStatusService _statusService;
        private readonly IBlobImageService _blobImageService;

        public GrupoEstandarController(
            IEstandarService estandarService,
            IStatusService statusService,
            ILocalService localService,
            IBlobImageService blobImageService)
        {
            _estandarService = estandarService;
            _statusService = statusService;
            _localService = localService;
            _blobImageService = blobImageService;
        }

        // GET
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetGrupos(int page = 1, int limit = 10)
        {
            var items = _estandarService.GetGrupoPagedList(page > 0 ? page - 1 : page, limit);
            var records = items.Items;
            var total = items.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }


        public ActionResult New()
        {
            var formModel = new GrupoEstandarFormModel();
            PopulateFormModelData(formModel, null);

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(GrupoEstandarFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var grupoEstandar = new GrupoEstandar()
                {
                    Codigo = formModel.Codigo,
                    Nombre = formModel.Nombre,
                    CreadoPor = User.Identity.Name,
                    CreadoEn = DateTime.Now,
                    ActualizadoPor = User.Identity.Name,
                    ActualizadoEn = DateTime.Now,
                    Estado = _statusService.Get(formModel.Estado),
                };

                if (!string.IsNullOrEmpty(formModel.Descripcion))
                {
                    grupoEstandar.Descripcion = formModel.Descripcion;
                }

                if (formModel.Imagen != null && formModel.Imagen.ContentLength > 0)
                {
                    var imageUrl = _blobImageService.UploadImage(formModel.Imagen, BlobContainers.General());
                    grupoEstandar.Imagen = imageUrl;
                }

                var confirmation = _estandarService.SaveOrUpdateGrupo(grupoEstandar);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Grupo Estandar creado con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "grupoestandar");
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
            var grupoEstandar = _estandarService.GetGrupo(id);
            var formModel = new GrupoEstandarFormModel();
            PopulateFormModelData(formModel, grupoEstandar);

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, GrupoEstandarFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var dbGrupoEstandar = _estandarService.GetGrupo(id);
                dbGrupoEstandar.Nombre = formModel.Nombre;
                dbGrupoEstandar.Codigo = formModel.Codigo;

                if (!string.IsNullOrEmpty(formModel.Descripcion))
                { dbGrupoEstandar.Descripcion = formModel.Descripcion; }

                if (formModel.Imagen != null && formModel.Imagen.ContentLength > 0)
                {
                    var imageUrl = _blobImageService.UploadImage(formModel.Imagen, BlobContainers.General());
                    dbGrupoEstandar.Imagen = imageUrl;
                }

                dbGrupoEstandar.Estado = _statusService.Get(formModel.Estado);

                dbGrupoEstandar.ActualizadoPor = User.Identity.Name;
                dbGrupoEstandar.ActualizadoEn = DateTime.Now;

                var confirmation = _estandarService.SaveOrUpdateGrupo(dbGrupoEstandar);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Grupo actualizado con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "grupoestandar");
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
            var item = _estandarService.GetGrupo(id);
            if (item == null) return Json(false);

            var itemName = item.Nombre + "|";
            var confirmation = _estandarService.DeleteGrupo(item.Id);
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


        private void PopulateFormModelData(GrupoEstandarFormModel formModel, GrupoEstandar grupoEstandar)
        {
            if (grupoEstandar != null)
            {
                formModel.Id = grupoEstandar.Id;
                formModel.Codigo = grupoEstandar.Codigo;
                formModel.Nombre = grupoEstandar.Nombre;
                formModel.Descripcion = grupoEstandar.Descripcion;
                formModel.ImagenUrl = grupoEstandar.Imagen;
                formModel.Estado = grupoEstandar.Estado.Id;
                //formModel.TipoLocal = grupoEstandar.TipoLocal.Id;
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