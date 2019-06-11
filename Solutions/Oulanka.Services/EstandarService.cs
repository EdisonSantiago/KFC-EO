using System;
using System.Collections.Generic;
using Oulanka.Domain;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Estandares;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Services
{
    public class EstandarService : IEstandarService
    {
        private readonly IEstandarRepository _estandarRepository;
        private readonly IGrupoEstandarRepository _grupoEstandarRepository;
        private readonly IEventLogService _eventLogService;
        private readonly ICalificacionRepository _calificacionRepository;
        private readonly IClasificacionRepository _clasificacionRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly INivelRepository _nivelRepository;
        private readonly IOpcionRepository _opcionRepository;
        private readonly ISistemaRepository _sistemaRepository;

        public EstandarService(
            IEstandarRepository estandarRepository,
            IEventLogService eventLogService,
            IGrupoEstandarRepository grupoEstandarRepository,
            ICalificacionRepository calificacionRepository,
            ICategoriaRepository categoriaRepository,
            INivelRepository nivelRepository,
            IOpcionRepository opcionRepository,
            ISistemaRepository sistemaRepository, 
            IClasificacionRepository clasificacionRepository)
        {
            _estandarRepository = estandarRepository;
            _eventLogService = eventLogService;
            _grupoEstandarRepository = grupoEstandarRepository;
            _calificacionRepository = calificacionRepository;
            _categoriaRepository = categoriaRepository;
            _nivelRepository = nivelRepository;
            _opcionRepository = opcionRepository;
            _sistemaRepository = sistemaRepository;
            _clasificacionRepository = clasificacionRepository;
        }

        #region Estandar Service

        public PagedList<Estandar> GetPagedList(int page = 0, int limit = 10)
        {
            return _estandarRepository.GetPagedList(page, limit);
        }

        public PagedList<Estandar> GetByGrupoPagedList(Guid grupoId, int page = 0, int limit = 10)
        {
            return _estandarRepository.GetByGrupoPagedList(grupoId, page, limit);
        }

        public PagedList<Estandar> GetByGrupoPagedList(Guid grupoId, TipoEstandar tipoEstandar = TipoEstandar.Contenedor, int page = 0,
            int limit = 10)
        {
            return _estandarRepository.GetByGrupoPagedList(grupoId, tipoEstandar, page, limit);
        }

        public PagedList<Estandar> GetByParentPagedList(Guid parentId, int page = 0, int limit = 10)
        {
            return _estandarRepository.GetByParentPagedList(parentId, page, limit);
        }

        public IList<Estandar> GetByGrupo(Guid grupoId, TipoEstandar tipoEstandar = TipoEstandar.Contenedor)
        {
            return _estandarRepository.GetByGrupo(grupoId, tipoEstandar);
        }

        public IList<Estandar> GetByParent(Guid parentId)
        {
            return _estandarRepository.GetByParent(parentId);
        }

        public IList<Estandar> GetPicklist(Guid parentId)
        {
            return _estandarRepository.GetPicklist(parentId);
        }

        public IList<Estandar> GetList()
        {
            return _estandarRepository.GetAll();
        }

        public Estandar Get(Guid id)
        {
            return _estandarRepository.Get(id);
        }

        public Estandar GetByCodigo(string codigo)
        {
            return _estandarRepository.GetByCodigo(codigo);
        }


        public ActionConfirmation SaveOrUpdate(Estandar estandar)
        {
            if (!estandar.IsValid()) return ActionConfirmation.CreateFailure("estandar no es válida");

            try
            {
                _estandarRepository.SaveOrUpdate(estandar);
                _estandarRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, estandar.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation Delete(Guid id)
        {
            var item = _estandarRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("estandar no existe");

            try
            {
                _estandarRepository.Delete(item.Id);
                _estandarRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("Delete OK (" + item.Nombre + ")");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        #endregion


        #region Grupo Estandar

        public PagedList<GrupoEstandar> GetGrupoPagedList(int page, int limit)
        {
            return _grupoEstandarRepository.GetPagedList(page, limit);
        }

        public IList<GrupoEstandar> GetGruposList()
        {
            return _grupoEstandarRepository.GetAll(); ;
        }

        public GrupoEstandar GetGrupo(Guid id)
        {
            return _grupoEstandarRepository.Get(id);
        }

        public GrupoEstandar GetGrupo(string codigo)
        {
            return _grupoEstandarRepository.GetByCodigo(codigo);
        }

        public ActionConfirmation SaveOrUpdateGrupo(GrupoEstandar grupoEstandar)
        {
            if (!grupoEstandar.IsValid()) return ActionConfirmation.CreateFailure("objeto no es válida");

            try
            {
                _grupoEstandarRepository.SaveOrUpdate(grupoEstandar);
                _grupoEstandarRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, grupoEstandar.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation DeleteGrupo(Guid id)
        {
            var item = _grupoEstandarRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("objeto no existe");

            try
            {
                _grupoEstandarRepository.Delete(item.Id);
                _grupoEstandarRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("Delete OK (" + item.Nombre + ")");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }


        #endregion

        #region Calificaciones

        public PagedList<Calificacion> GetCalificacionesPagedList(int page = 0, int limit = 10)
        {
            return _calificacionRepository.GetPagedList(page, limit);
        }

        public IEnumerable<Calificacion> GetCalificacionList()
        {
            return _calificacionRepository.GetAll();
        }

        public Calificacion GetCalificacion(Guid id)
        {
            return _calificacionRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdateCalificacion(Calificacion item)
        {
            if (!item.IsValid()) return ActionConfirmation.CreateFailure("objeto no es válida");

            try
            {
                _calificacionRepository.SaveOrUpdate(item);
                _calificacionRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation DeleteCalificacion(Guid id)
        {
            var item = _calificacionRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("objeto no existe");

            try
            {
                _calificacionRepository.Delete(item.Id);
                _calificacionRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("Delete OK (" + item.Nombre + ")");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        #endregion

        #region Clasificaciones

        public PagedList<Clasificacion> GetClasificacionPagedList(int page = 0, int limit = 10)
        {
            return _clasificacionRepository.GetPagedList(page, limit);
        }

        public IEnumerable<Clasificacion> GetClasificacionList()
        {
            return _clasificacionRepository.GetAll();
        }

        public Clasificacion GetClasificacion(Guid id)
        {
            return _clasificacionRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdateClasificacion(Clasificacion item)
        {
            if (!item.IsValid()) return ActionConfirmation.CreateFailure("objeto no es válida");

            try
            {
                _clasificacionRepository.SaveOrUpdate(item);
                _clasificacionRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation DeleteClasificacion(Guid id)
        {
            var item = _clasificacionRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("objeto no existe");

            try
            {
                _clasificacionRepository.Delete(item.Id);
                _clasificacionRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("Delete OK (" + item.Nombre + ")");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        #endregion

        #region Categorias

        public PagedList<Categoria> GetCategoriaPagedList(int page = 0, int limit = 10)
        {
            return _categoriaRepository.GetPagedList(page, limit);
        }

        public IEnumerable<Categoria> GetCategoriaList()
        {
            return _categoriaRepository.GetAll();
        }

        public Categoria GetCategoria(Guid id)
        {
            return _categoriaRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdateCategoria(Categoria item)
        {
            if (!item.IsValid()) return ActionConfirmation.CreateFailure("objeto no es válida");

            try
            {
                _categoriaRepository.SaveOrUpdate(item);
                _categoriaRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation DeleteCategoria(Guid id)
        {
            var item = _categoriaRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("objeto no existe");

            try
            {
                _categoriaRepository.Delete(item.Id);
                _categoriaRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("Delete OK (" + item.Nombre + ")");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        #endregion


        #region Niveles

        public PagedList<Nivel> GetNivelPagedList(int page = 0, int limit = 10)
        {
            return _nivelRepository.GetPagedList(page, limit);
        }

        public IEnumerable<Nivel> GetNivelList()
        {
            return _nivelRepository.GetAll();
        }

        public Nivel GetNivel(Guid id)
        {
            return _nivelRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdateNivel(Nivel item)
        {
            if (!item.IsValid()) return ActionConfirmation.CreateFailure("objeto no es válida");

            try
            {
                _nivelRepository.SaveOrUpdate(item);
                _nivelRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation DeleteNivel(Guid id)
        {
            var item = _nivelRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("objeto no existe");

            try
            {
                _nivelRepository.Delete(item.Id);
                _nivelRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("Delete OK (" + item.Nombre + ")");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        #endregion


        #region Opciones

        public PagedList<Opcion> GetOpcionPagedList(int page = 0, int limit = 10)
        {
            return _opcionRepository.GetPagedList(page, limit);
        }

        public IEnumerable<Opcion> GetOpcionList()
        {
            return _opcionRepository.GetAll();
        }

        public Opcion GetOpcion(Guid id)
        {
            return _opcionRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdateOpcion(Opcion item)
        {
            if (!item.IsValid()) return ActionConfirmation.CreateFailure("objeto no es válida");

            try
            {
                _opcionRepository.SaveOrUpdate(item);
                _opcionRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation DeleteOpcion(Guid id)
        {
            var item = _opcionRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("objeto no existe");

            try
            {
                _opcionRepository.Delete(item.Id);
                _opcionRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("Delete OK (" + item.Nombre + ")");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        #endregion

        #region Sistemas

        public PagedList<Sistema> GetSistemaPagedList(int page = 0, int limit = 10)
        {
            return _sistemaRepository.GetPagedList(page, limit);
        }

        public IList<Sistema> GetSistemaList()
        {
            return _sistemaRepository.GetAll();
        }

        public Sistema GetSistema(Guid id)
        {
            return _sistemaRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdateSistema(Sistema item)
        {
            if (!item.IsValid()) return ActionConfirmation.CreateFailure("objeto no es válida");

            try
            {
                _sistemaRepository.SaveOrUpdate(item);
                _sistemaRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation DeleteSistema(Guid id)
        {
            var item = _sistemaRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("objeto no existe");

            try
            {
                _sistemaRepository.Delete(item.Id);
                _sistemaRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("Delete OK (" + item.Nombre + ")");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        #endregion

    }
}