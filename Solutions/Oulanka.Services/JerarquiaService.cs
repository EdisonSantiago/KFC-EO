using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using FluentNHibernate.Conventions.Instances;
using Oulanka.Domain;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Jerarquias;

namespace Oulanka.Services
{
    public class JerarquiaService : IJerarquiaService
    {
        private readonly IGerenteGeneralRepository _generalRepository;
        private readonly IGerenteNacionalRepository _nacionalRepository;
        private readonly IGerenteRegionalRepository _regionalRepository;
        private readonly IJefeAreaRepository _jefeAreaRepository;
        private readonly IStatusService _statusService;
        private readonly IEventLogService _eventLogService;
        private readonly ICadenaService _cadenaService;
        private readonly ILocalService _localService;

        public JerarquiaService(
            IStatusService statusService,
            IJefeAreaRepository jefeAreaRepository,
            IGerenteRegionalRepository regionalRepository,
            IGerenteNacionalRepository nacionalRepository,
            IGerenteGeneralRepository generalRepository,
            IEventLogService eventLogService,
            ICadenaService cadenaService, ILocalService localService)
        {
            _statusService = statusService;
            _jefeAreaRepository = jefeAreaRepository;
            _regionalRepository = regionalRepository;
            _nacionalRepository = nacionalRepository;
            _generalRepository = generalRepository;
            _eventLogService = eventLogService;
            _cadenaService = cadenaService;
            _localService = localService;
        }

        #region General

        public PagedList<GerenteGeneral> GetGeneralPagedList(int page = 0, int limit = 10)
        {
            return _generalRepository.GetPagedList(page, limit);
        }

        public GerenteGeneral GetGeneral()
        {
            var list = _generalRepository.GetList();
            return list.Any()
                ? list.First()
                : null;
        }

        public GerenteGeneral GetGeneral(Guid id)
        {
            return _generalRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdateGeneral(GerenteGeneral gerenteGeneral)
        {
            if (!gerenteGeneral.IsValid()) return ActionConfirmation.CreateFailure("item no es válido");

            try
            {
                _generalRepository.SaveOrUpdate(gerenteGeneral);
                _generalRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, gerenteGeneral.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        #endregion

        #region Nacionales

        public PagedList<GerenteNacional> GetNacionalesPagedList(int page = 0, int limit = 10)
        {
            return _nacionalRepository.GetPagedList(page, limit);
        }

        public PagedList<GerenteNacional> GetNacionalesPagedList(Guid parentId, int page = 0, int limit = 10)
        {
            return _nacionalRepository.GetPagedList(parentId, page, limit);
        }

        public IList<GerenteNacional> GetNacionalesList(Guid parentId)
        {
            return _nacionalRepository.GetList(parentId);
        }

        public GerenteNacional GetNacional(Guid id)
        {
            return _nacionalRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdateNacional(GerenteNacional nacional)
        {
            if (!nacional.IsValid()) return ActionConfirmation.CreateFailure("item no es válido");

            try
            {
                _nacionalRepository.SaveOrUpdate(nacional);
                _nacionalRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, nacional.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation DeleteNacional(Guid id)
        {
            var item = _nacionalRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("item no existe");

            try
            {
                if (item.GerentesRegionales.Any())
                {
                    foreach (var regional in item.GerentesRegionales)
                    {
                        DeleteRegional(regional.Id);
                    }
                }

                _nacionalRepository.Delete(item.Id);
                _nacionalRepository.DbContext.CommitChanges();

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


        #region Regionales

        public PagedList<GerenteRegional> GetRegionalesPagedList(int page = 0, int limit = 10)
        {
            return _regionalRepository.GetPagedList(page, limit);
        }

        public PagedList<GerenteRegional> GetRegionalesPagedList(Guid parentId, int page = 0, int limit = 10)
        {
            return _regionalRepository.GetPagedList(parentId, page, limit);
        }

        public IList<GerenteRegional> GetRegionalesList(Guid parentId)
        {
            return _regionalRepository.GetList(parentId);
        }

        public GerenteRegional GetRegional(Guid id)
        {
            return _regionalRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdateRegional(GerenteRegional regional)
        {
            if (!regional.IsValid()) return ActionConfirmation.CreateFailure("item no es válido");

            try
            {
                _regionalRepository.SaveOrUpdate(regional);
                _regionalRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, regional.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation DeleteRegional(Guid id)
        {
            var item = _regionalRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("item no existe");

            try
            {
                if (item.JefesAreas.Any())
                {
                    foreach (var jefeArea in item.JefesAreas)
                    {
                        DeleteJefeArea(jefeArea.Id);
                    }
                }

                _regionalRepository.Delete(item.Id);
                _regionalRepository.DbContext.CommitChanges();

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

        #region JefesArea

        public IList<JefeArea> GetJefesAreasList()
        {
            return _jefeAreaRepository.GetList();
        }

        public IList<JefeArea> GetJefesAreasList(Guid gerenteRegionalId)
        {
            return _jefeAreaRepository.GetList(gerenteRegionalId);
        }

        public JefeArea GetJefeArea(Guid id)
        {
            return _jefeAreaRepository.Get(id);
        }

        public PagedList<JefeArea> GetJefesAreasPagedList(int page = 0, int limit = 10)
        {
            return _jefeAreaRepository.GetPagedList(page, limit);
        }

        public PagedList<JefeArea> GetJefesAreasPagedList(Guid parentId, int page = 0, int limit = 10)
        {
            var jefesPagedList = new PagedList<JefeArea>();
            var isRegional = GetRegional(parentId) != null;
            if (isRegional)
            {
                return _jefeAreaRepository.GetPagedList(parentId, page, limit);
            }
            else
            {
                var isGeneral = GetGeneral(parentId) != null;
                if (isGeneral)
                {
                    var gerenteGeneral = GetGeneral();
                    var regionales = new List<GerenteRegional>();
                    foreach (var nacional in gerenteGeneral.GerentesNacionales)
                    {
                        if (nacional.GerentesRegionales.Any())
                        {
                            regionales.AddRange(nacional.GerentesRegionales);
                        }
                    }
                    var parentIds = regionales.Select(x => x.Id).ToArray();

                    jefesPagedList = _jefeAreaRepository.GetPagedList(parentIds, page, limit);
                }
                else if (GetNacional(parentId) != null)
                {
                    var gerenteNacional = GetNacional(parentId);
                    var regionales = new List<GerenteRegional>();

                    if (gerenteNacional.GerentesRegionales.Any())
                    {
                        regionales.AddRange(gerenteNacional.GerentesRegionales);
                    }

                    var parentIds = regionales.Select(x => x.Id).ToArray();

                    jefesPagedList = _jefeAreaRepository.GetPagedList(parentIds, page, limit);
                }

            }

            return jefesPagedList;
        }

        public IList<JefeArea> GetJefesAreaByCadena(Guid cadenaId)
        {
            var cadena = _cadenaService.Get(cadenaId);
            var jefes = new List<JefeArea>();
            if (cadena != null)
            {
                var gerentes = cadena.GerentesNacionales;
                var regionales = new List<GerenteRegional>();
                foreach (var gerente in gerentes)
                {
                    if (gerente.GerentesRegionales.Any())
                    {
                        regionales.AddRange(gerente.GerentesRegionales);
                    }
                }

                foreach (var regional in regionales)
                {
                    if (regional.JefesAreas.Any())
                    {
                        jefes.AddRange(regional.JefesAreas);
                    }
                }
            }

            jefes = jefes.OrderBy(x => x.Nombre).ToList();

            return jefes;
        }

        public ActionConfirmation SaveOrUpdateJefeArea(JefeArea jefeArea)
        {
            if (!jefeArea.IsValid()) return ActionConfirmation.CreateFailure("item no es válido");

            try
            {
                _jefeAreaRepository.SaveOrUpdate(jefeArea);
                _jefeAreaRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, jefeArea.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation DeleteJefeArea(Guid id)
        {
            var item = _jefeAreaRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("item no existe");

            try
            {
                if (item.Locales.Any())
                {
                    foreach (var local in item.Locales)
                    {
                        _localService.Delete(local.Id);
                    }
                }

                _jefeAreaRepository.Delete(item.Id);
                _jefeAreaRepository.DbContext.CommitChanges();

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