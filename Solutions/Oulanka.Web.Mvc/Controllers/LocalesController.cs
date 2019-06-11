using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Locales;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class LocalesController : BaseController
    {
        private readonly ILocalService _localService;
        private readonly ICadenaService _cadenaService;
        private readonly IUbicacionService _ubicacionService;
        private readonly IJerarquiaService _jerarquiaService;
        private readonly IStatusService _statusService;
        private readonly IBlobImageService _blobImageService;

        public LocalesController(
            ILocalService localService,
            IStatusService statusService,
            ICadenaService cadenaService,
            IUbicacionService ubicacionService,
            IJerarquiaService jerarquiaService,
            IBlobImageService blobImageService)
        {
            _localService = localService;
            _statusService = statusService;
            _cadenaService = cadenaService;
            _ubicacionService = ubicacionService;
            _jerarquiaService = jerarquiaService;
            _blobImageService = blobImageService;
        }


        // GET
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetLocales(int page = 1, int limit = 10)
        {
            var locales = _localService.GetPagedList(page > 0 ? page - 1 : page, limit);
            var records = locales.Items;
            var total = locales.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }


        public ActionResult New()
        {
            var formModel = new LocalFormModel();
            PopulateFormModelData(formModel, null);

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(LocalFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var local = new Local()
                {
                    Codigo = formModel.Codigo,
                    Nombre = formModel.Nombre,
                    Descripcion = formModel.Descripcion,
                    Direccion = formModel.Direccion,
                    Telefono = formModel.Telefono,
                    Email = formModel.Email,
                    Ruc = formModel.Ruc,
                    OpClave = formModel.OpClave,
                    AC = formModel.AC,
                    Concepto = formModel.Concepto,
                    CreadoPor = User.Identity.Name,
                    CreadoEn = DateTime.Now,
                    ActualizadoPor = User.Identity.Name,
                    ActualizadoEn = DateTime.Now,
                    Estado = _statusService.Get(formModel.Estado),
                    TipoLocal = _localService.GetTipo(formModel.TipoLocal),
                    Cadena = _cadenaService.Get(formModel.Cadena),
                    Ciudad = _ubicacionService.GetCiudad(formModel.Ciudad),
                    JefeArea = _jerarquiaService.GetJefeArea(formModel.JefeArea),
                    Propietario = _jerarquiaService.GetJefeArea(formModel.JefeArea).Nombre,

                };

                if (formModel.Imagen != null && formModel.Imagen.ContentLength > 0)
                {
                    var imageUrl = _blobImageService.UploadImage(formModel.Imagen, BlobContainers.Locales());
                    local.Imagen = imageUrl;
                }

                if (formModel.Logo != null && formModel.Logo.ContentLength > 0)
                {
                    var logoUrl = _blobImageService.UploadImage(formModel.Logo, BlobContainers.General());
                    local.Logo = logoUrl;
                }

                var confirmation = _localService.SaveOrUpdate(local);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Local creado con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "locales");
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
            var local = _localService.Get(id);
            var formModel = new LocalFormModel();
            PopulateFormModelData(formModel, local);

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, LocalFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var dbLocal = _localService.Get(id);
                dbLocal.Codigo = formModel.Codigo;
                dbLocal.Nombre = formModel.Nombre;
                dbLocal.Descripcion = formModel.Descripcion;
                dbLocal.Direccion = formModel.Direccion;
                dbLocal.Telefono = formModel.Telefono;
                dbLocal.Email = formModel.Email;
                dbLocal.Ruc = formModel.Ruc;
                dbLocal.OpClave = formModel.OpClave;
                dbLocal.AC = formModel.AC;
                dbLocal.Concepto = formModel.Concepto;
                dbLocal.ActualizadoPor = User.Identity.Name;
                dbLocal.ActualizadoEn = DateTime.Now;
                dbLocal.Estado = _statusService.Get(formModel.Estado);
                dbLocal.TipoLocal = _localService.GetTipo(formModel.TipoLocal);
                dbLocal.Cadena = _cadenaService.Get(formModel.Cadena);
                dbLocal.Ciudad = _ubicacionService.GetCiudad(formModel.Ciudad);
                dbLocal.JefeArea = _jerarquiaService.GetJefeArea(formModel.JefeArea);
                dbLocal.Propietario = _jerarquiaService.GetJefeArea(formModel.JefeArea).Nombre;

                if (formModel.Imagen != null && formModel.Imagen.ContentLength > 0)
                {
                    var imageUrl = _blobImageService.UploadImage(formModel.Imagen, BlobContainers.Locales());
                    dbLocal.Imagen = imageUrl;
                }

                if (formModel.Logo != null && formModel.Logo.ContentLength > 0)
                {
                    var logoUrl = _blobImageService.UploadImage(formModel.Logo, BlobContainers.General());
                    dbLocal.Logo = logoUrl;
                }

                var confirmation = _localService.SaveOrUpdate(dbLocal);
                if (confirmation.WasSuccessful)
                {
                    this.AddPageMessage("Local actualizado con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "locales");
                }
                else
                {
                    this.AddPageMessage(confirmation.Message, PageMessageType.Error, false);
                }


            }


            PopulateFormModelData(formModel, null);
            return View(formModel);
        }

        private void PopulateFormModelData(LocalFormModel formModel, Local local)
        {
            if (local != null)
            {
                formModel.Id = local.Id;
                formModel.Nombre = local.Nombre;
                formModel.Codigo = local.Codigo;
                formModel.Direccion = local.Direccion;
                formModel.Descripcion = local.Descripcion;
                formModel.Email = local.Email;
                formModel.ImagenUrl = local.Imagen;
                formModel.LogoUrl = local.Logo;
                formModel.Telefono = local.Telefono;
                formModel.Ruc = local.Ruc;
                formModel.Propietario = local.Propietario;
                formModel.AC = local.AC;
                formModel.Concepto = local.Concepto;
                formModel.Estado = local.Estado.Id;
                formModel.TipoLocal = local.TipoLocal.Id;
                formModel.Cadena = local.Cadena.Id;
                formModel.Ciudad = local.Ciudad.Id;
                formModel.JefeArea = local.JefeArea.Id;
                formModel.OpClave = local.OpClave;
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
            var tipoTypes = _localService.GetTiposList();
            foreach (var tipo in tipoTypes)
            {
                var selectItem = new SelectListItem
                {
                    Text = tipo.Detalle,
                    Value = tipo.Id.ToString()
                };

                if (tipo.Id == formModel.TipoLocal)
                {
                    selectItem.Selected = true;
                }

                tipoItems.Add(selectItem);
            }

            formModel.TiposLocales = tipoItems;

            var cadenas = new List<SelectListItem>();
            var cadenaTypes = _cadenaService.GetList();
            foreach (var tipo in cadenaTypes)
            {
                var selectItem = new SelectListItem
                {
                    Text = tipo.Nombre,
                    Value = tipo.Id.ToString()
                };

                if (tipo.Id == formModel.Cadena)
                {
                    selectItem.Selected = true;
                }

                cadenas.Add(selectItem);
            }

            formModel.Cadenas = cadenas;

            var ciudades = new List<SelectListItem>();
            var ciudadesTypes = _ubicacionService.GetCiudades();
            foreach (var ciudad in ciudadesTypes)
            {
                var selectItem = new SelectListItem
                {
                    Text = ciudad.Nombre,
                    Value = ciudad.Id.ToString()
                };

                if (ciudad.Id == formModel.Ciudad)
                {
                    selectItem.Selected = true;
                }

                ciudades.Add(selectItem);
            }

            formModel.Ciudades = ciudades;

            var jefesArea = new List<SelectListItem>();
            var jefesTypes = _jerarquiaService.GetJefesAreasList();
            foreach (var item in jefesTypes)
            {
                var selectItem = new SelectListItem
                {
                    Text = item.Nombre,
                    Value = item.Id.ToString()
                };

                if (item.Id == formModel.JefeArea)
                {
                    selectItem.Selected = true;
                }

                jefesArea.Add(selectItem);
            }

            formModel.JefesArea = jefesArea;

        }

        public JsonResult Delete(Guid id)
        {
            var item = _localService.Get(id);
            if (item == null) return Json(false);

            var itemName = item.Nombre + "|";
            var confirmation = _localService.Delete(item.Id);
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

        #region Tipos Locales


        public ActionResult Tipos()
        {
            var estados = _statusService.GetItems();
            return View(estados);
        }

        public JsonResult GetTipos(int page = 1, int limit = 10)
        {
            var tipos = _localService.GetTiposPagedList(page > 0 ? page - 1 : page, limit);
            var records = tipos.Items;
            var total = tipos.TotalCount;

            return Json(
                new { records, total }
                , JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveTipo(TipoLocalFormModel tipo)
        {
            TipoLocal tipoLocal = null;
            tipoLocal = tipo.Id == Guid.Empty
                ? new TipoLocal()
                : _localService.GetTipo(tipo.Id);

            tipoLocal.Detalle = tipo.Detalle;
            tipoLocal.Estado = _statusService.Get(tipo.EstadoId);
            if (tipoLocal.Id == Guid.Empty)
            {
                tipoLocal.CreadoEn = DateTime.Now;
                tipoLocal.CreadoPor = User.Identity.Name;
            }

            tipoLocal.ActualizadoEn = DateTime.Now;
            tipoLocal.ActualizadoPor = User.Identity.Name;

            var confirmation = _localService.SaveOrUpdateTipo(tipoLocal);
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult DeleteTipo(Guid id)
        {
            var item = _localService.GetTipo(id);
            if (item == null) return Json(false);

            var itemName = item.Detalle + "|";
            var confirmation = _localService.DeleteTipo(item.Id);
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


        #region Jerarquias

        public JsonResult GetJefesByCadena(string id)
        {
            var jefes = _jerarquiaService.GetJefesAreaByCadena(Guid.Parse(id));
            var items = new List<SelectListItem>();
            foreach (var jefeArea in jefes)
            {
                var item = new SelectListItem
                {
                    Text = jefeArea.Nombre,
                    Value = jefeArea.Id.ToString()
                };
                items.Add(item);
            }

            return Json(new { records = items }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}