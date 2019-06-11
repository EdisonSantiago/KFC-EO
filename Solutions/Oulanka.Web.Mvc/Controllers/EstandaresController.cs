using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Locales;
using Oulanka.Web.Core.Controllers;
using Oulanka.Web.Core.FormModels;
using Oulanka.Web.Core.Models;
using Oulanka.Web.Core.ViewModels;

namespace Oulanka.Web.Mvc.Controllers
{
    public class EstandaresController : BaseController
    {
        private readonly IEstandarService _estandarService;
        private readonly IStatusService _statusService;
        private readonly ILocalService _localService;

        public EstandaresController(
            IEstandarService estandarService,
            IStatusService statusService,
            ILocalService localService)
        {
            _estandarService = estandarService;
            _statusService = statusService;
            _localService = localService;
        }


        // GET
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New(string parentId = "")
        {
            var formModel = new EstandarFormModel();
            PopulateFormModelData(formModel, null);

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult New(EstandarFormModel formModel, string parentId = "")
        {
            if (ModelState.IsValid && IsStandardValid(formModel))
            {
                var estandar = new Estandar
                {
                    Codigo = formModel.Codigo,
                    Nombre = formModel.Nombre,
                    Descripcion = !string.IsNullOrEmpty(formModel.Descripcion) ? formModel.Descripcion : string.Empty,
                    NotasEspeciales = !string.IsNullOrEmpty(formModel.NotasEspeciales) ? formModel.NotasEspeciales : string.Empty,
                    CreadoEn = DateTime.Now,
                    TipoEstandar = formModel.TipoEstandar,
                    GrupoEstandar = _estandarService.GetGrupo(formModel.GrupoEstandar),
                    ActualizadoEn = DateTime.Now,
                    CreadoPor = User.Identity.Name,
                    ActualizadoPor = User.Identity.Name,
                    Estado = _statusService.Get(formModel.Estado),
                    Nivel = _estandarService.GetNivel(formModel.Nivel),
                    Categoria = _estandarService.GetCategoria(formModel.Categoria),
                    Clasificacion = _estandarService.GetClasificacion(formModel.Clasificacion)
                };

                var sistemas = new List<Sistema>();

                if (formModel.Sistema != null && formModel.Sistema.Length > 0)
                {
                    foreach (var sistemaId in formModel.Sistema)
                    {
                        var sistema = _estandarService.GetSistema(Guid.Parse(sistemaId));
                        if (sistema != null)
                        {
                            sistemas.Add(sistema);
                        }
                    }
                }

                var tipoLocales = new List<TipoLocal>();
                if (formModel.TipoLocal != null && formModel.TipoLocal.Length > 0)
                {
                    foreach (var tipoLocalId in formModel.TipoLocal)
                    {
                        var tipoLocal = _localService.GetTipo(Guid.Parse(tipoLocalId));
                        if (tipoLocal != null)
                        {
                            tipoLocales.Add(tipoLocal);
                        }
                    }
                }


                var confirmation = _estandarService.SaveOrUpdate(estandar);
                if (confirmation.WasSuccessful)
                {
                    estandar.Sistemas = sistemas;
                    estandar.TipoLocales = tipoLocales;

                    _estandarService.SaveOrUpdate(estandar);

                    if (!string.IsNullOrEmpty(parentId))
                    {
                        var parentGuid = Guid.Parse(parentId);
                        var parent = _estandarService.Get(parentGuid);
                        if (parent != null)
                        {
                            estandar.EstandarPadre = parent;
                            _estandarService.SaveOrUpdate(estandar);
                        }
                    }

                    this.AddPageMessage("Estandar creado con éxito", PageMessageType.Success, true);

                    if (!string.IsNullOrEmpty(parentId))
                    {
                        var parentGuid = Guid.Parse(parentId);
                        var parent = _estandarService.Get(parentGuid);
                        if (parent != null)
                        {
                            return RedirectToAction("show", "estandares", new { id = parent.Id });
                        }
                        else
                        {
                            return RedirectToAction("index", "estandares");
                        }
                    }
                    else
                    {
                        return RedirectToAction("index", "estandares");
                    }
                }
                else
                {
                    this.AddPageMessage(confirmation.Message, PageMessageType.Error, false);
                }
            }

            PopulateFormModelData(formModel, null);
            return View(formModel);
        }

        private bool IsStandardValid(EstandarFormModel formModel)
        {
            var isValid = new List<bool>();

            if (formModel.Id == Guid.Empty)
            {
                var dbStandard = _estandarService.GetByCodigo(formModel.Codigo);
                if (dbStandard != null)
                {
                    isValid.Add(false);
                    ModelState.AddModelError("Codigo", "Ya existe un item con ese código");
                }
            }

            return !isValid.Contains(false);
        }

        private void PopulateFormModelData(EstandarFormModel formModel, Estandar estandar)
        {
            if (estandar != null)
            {
                formModel.Id = estandar.Id;
                formModel.Codigo = estandar.Codigo;
                formModel.Descripcion = estandar.Descripcion;
                formModel.NotasEspeciales = estandar.NotasEspeciales;
                formModel.Nombre = estandar.Nombre;
                formModel.GrupoEstandar = estandar.GrupoEstandar.Id;
                formModel.TipoEstandar = estandar.TipoEstandar;
                formModel.Categoria = estandar.Categoria.Id;
                formModel.Nivel = estandar.Nivel.Id;
                formModel.Estado = estandar.Estado.Id;
                formModel.Clasificacion = estandar.Clasificacion.Id;
            }

            var grupoItems = new List<SelectListItem>();
            var grupoTypes = _estandarService.GetGruposList();
            foreach (var grupoEstandar in grupoTypes)
            {
                var selectItem = new SelectListItem
                {
                    Text = grupoEstandar.Nombre,
                    Value = grupoEstandar.Id.ToString()
                };

                if (grupoEstandar.Id == formModel.GrupoEstandar)
                {
                    selectItem.Selected = true;
                }

                grupoItems.Add(selectItem);
            }

            formModel.GrupoEstandares = grupoItems;

            var tipoItems = Enum.GetValues(typeof(TipoEstandar)).Cast<int>()
                .Select(e => new SelectListItem()
                {
                    Text = Enum.GetName(typeof(TipoEstandar), e),
                    Value = e.ToString()
                }).ToList();

            foreach (var selectListItem in tipoItems)
            {
                if (selectListItem.Value == formModel.TipoEstandar.ToString())
                {
                    selectListItem.Selected = true;
                }
            }

            formModel.TipoEstandares = tipoItems;

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

            var nivelItems = new List<SelectListItem>();
            var nivelTypes = _estandarService.GetNivelList();
            foreach (var nivel in nivelTypes)
            {
                var selectItem = new SelectListItem
                {
                    Text = nivel.Nombre,
                    Value = nivel.Id.ToString()
                };

                if (nivel.Id == formModel.Nivel)
                {
                    selectItem.Selected = true;
                }

                nivelItems.Add(selectItem);
            }

            formModel.Niveles = nivelItems;

            var categoriaItems = new List<SelectListItem>();
            var categoriaTypes = _estandarService.GetCategoriaList();
            foreach (var categoria in categoriaTypes)
            {
                var selectItem = new SelectListItem
                {
                    Text = categoria.Nombre,
                    Value = categoria.Id.ToString()
                };

                if (categoria.Id == formModel.Categoria)
                {
                    selectItem.Selected = true;
                }

                categoriaItems.Add(selectItem);
            }

            formModel.Categorias = categoriaItems;

            var clasificacionesItems = new List<SelectListItem>();
            var clasificacionesTypes = _estandarService.GetClasificacionList();
            foreach (var clasificacion in clasificacionesTypes)
            {
                var selectItem = new SelectListItem
                {
                    Text = clasificacion.Nombre,
                    Value = clasificacion.Id.ToString()
                };

                if (clasificacion.Id == formModel.Clasificacion)
                {
                    selectItem.Selected = true;
                }

                clasificacionesItems.Add(selectItem);
            }

            formModel.Clasificaciones = clasificacionesItems;

            var sistemas = _estandarService.GetSistemaList();
            var checkItems = new List<CheckBoxModel>();
            foreach (var sistema in sistemas)
            {
                var checkItem = new CheckBoxModel
                {
                    Value = sistema.Id,
                    Text = sistema.Nombre
                };
                if (estandar != null)
                { checkItem.IsChecked = estandar.Sistemas.Contains(sistema); }
                checkItems.Add(checkItem);
            }

            formModel.Sistemas = new CheckBoxList { CheckBoxItems = checkItems };

            var tipoLocales = _localService.GetTiposList();
            var checkLocalItems = new List<CheckBoxModel>();
            foreach (var tipoLocal in tipoLocales)
            {
                var checkItem = new CheckBoxModel
                {
                    Value = tipoLocal.Id,
                    Text = tipoLocal.Detalle
                };
                if (estandar != null)
                { checkItem.IsChecked = estandar.TipoLocales.Contains(tipoLocal); }
                checkLocalItems.Add(checkItem);
            }

            formModel.TipoLocales = new CheckBoxList { CheckBoxItems = checkLocalItems };
        }



        public ActionResult Edit(Guid id)
        {
            var estandar = _estandarService.Get(id);
            var formModel = new EstandarFormModel();

            PopulateFormModelData(formModel, estandar);


            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Guid id, EstandarFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var estandar = _estandarService.Get(id);
                if (estandar != null)
                {
                    estandar.Codigo = formModel.Codigo;
                    estandar.Nombre = formModel.Nombre;
                    estandar.Descripcion = formModel.Descripcion;
                    estandar.NotasEspeciales = formModel.NotasEspeciales;
                    estandar.TipoEstandar = formModel.TipoEstandar;
                    estandar.GrupoEstandar = _estandarService.GetGrupo(formModel.GrupoEstandar);
                    estandar.ActualizadoEn = DateTime.Now;
                    estandar.ActualizadoPor = User.Identity.Name;
                    estandar.Estado = _statusService.Get(formModel.Estado);
                    estandar.Nivel = _estandarService.GetNivel(formModel.Nivel);
                    estandar.Categoria = _estandarService.GetCategoria(formModel.Categoria);
                    estandar.Clasificacion = _estandarService.GetClasificacion(formModel.Clasificacion);
                }

                // SISTEMAS
                foreach (var sistemaId in formModel.Sistema)
                {
                    var sistema = _estandarService.GetSistema(Guid.Parse(sistemaId));
                    if (!estandar.Sistemas.Contains(sistema))
                        estandar.Sistemas.Add(sistema);
                }

                var sistemasToRemove = new List<Sistema>();
                foreach (var sistema in estandar.Sistemas)
                {
                    if (!formModel.Sistema.Contains(sistema.Id.ToString()))
                    {
                        sistemasToRemove.Add(sistema);
                    }
                }

                foreach (var sistema in sistemasToRemove)
                {
                    estandar.Sistemas.Remove(sistema);
                }

                // TIPO LOCALES
                foreach (var tipoLocalId in formModel.TipoLocal)
                {
                    var tipoLocal = _localService.GetTipo(Guid.Parse(tipoLocalId));
                    if (!estandar.TipoLocales.Contains(tipoLocal))
                        estandar.TipoLocales.Add(tipoLocal);
                }

                var tipoLocalesToRemove = new List<TipoLocal>();
                foreach (var tipoLocal in estandar.TipoLocales)
                {
                    if (!formModel.TipoLocal.Contains(tipoLocal.Id.ToString()))
                    {
                        tipoLocalesToRemove.Add(tipoLocal);
                    }
                }

                foreach (var tipoLocal in tipoLocalesToRemove)
                {
                    estandar.TipoLocales.Remove(tipoLocal);
                }


                var confirmation = _estandarService.SaveOrUpdate(estandar);
                if (confirmation.WasSuccessful)
                {
                    _estandarService.SaveOrUpdate(estandar);

                    this.AddPageMessage("Estandar actualizado con éxito", PageMessageType.Success, true);
                    return RedirectToAction("index", "estandares");
                }
                else
                {
                    this.AddPageMessage(confirmation.Message, PageMessageType.Error, false);
                }
            }

            PopulateFormModelData(formModel, null);
            return View(formModel);
        }


        public ActionResult Show(Guid id)
        {
            var estandar = _estandarService.Get(id);
            var viewModel = new EstandarViewModel
            {
                EstandarId = estandar.Id,
                CodigoEstandar = estandar.Codigo,
                Dto = Mapper.Map<Estandar, EstandarDto>(estandar)
            };

            return View(viewModel);
        }


        #region Json Data

        public JsonResult GetSubEstandares(Guid? parentId, int page = 1, int limit = 10)
        {
            PagedList<Estandar> estandares = new PagedList<Estandar>();

            if (parentId.HasValue)
            {
                var parent = _estandarService.Get(parentId.Value);
                if (parent != null)
                {
                    estandares = _estandarService.GetByParentPagedList(parent.Id, page > 0 ? page - 1 : page, limit);

                }
            }

            var records = estandares.Items;
            var total = estandares.TotalCount;

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEstandares(Guid? grupoId, int page = 1, int limit = 10)
        {
            PagedList<Estandar> estandares = new PagedList<Estandar>();

            if (grupoId.HasValue)
            {
                var grupo = _estandarService.GetGrupo(grupoId.Value);
                if (grupo != null)
                {
                    estandares = _estandarService.GetByGrupoPagedList(grupo.Id, TipoEstandar.Contenedor, page > 0 ? page - 1 : page, limit);

                }
                //estandares = _estandarService.GetPagedList(page > 0 ? page - 1 : page, limit);
            }
            else
            {

                estandares = _estandarService.GetPagedList(page > 0 ? page - 1 : page, limit);
                /* var grupoGuid = Guid.Parse(grupoId.GetValueOrDefault().ToString());
                 estandares = _estandarService.GetByGrupoPagedList(grupoGuid, page > 0 ? page - 1 : page, limit);*/
            }

            var records = estandares.Items;
            var total = estandares.TotalCount;

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTree()
        {
            var nodeList = new List<OulankaTreeNode>();


            var grupos = _estandarService.GetGruposList();
            foreach (var grupoEstandar in grupos)
            {
                var node = new OulankaTreeNode
                {
                    Id = grupoEstandar.Id,
                    Text = grupoEstandar.Nombre,
                    NodeType = "grupo"
                };
                nodeList.Add(node);
            }

            return Json(nodeList, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}