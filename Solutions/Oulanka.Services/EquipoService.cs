using System;
using System.Collections.Generic;
using System.Linq;
using Oulanka.Domain;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Locales;

namespace Oulanka.Services
{
    public class EquipoService : IEquipoService
    {
        private readonly IEquipoRepository _equipoRepository;
        private readonly ITipoEquipoRepository _tipoEquipoRepository;
        private readonly IEventLogService _eventLogService;
        private readonly IStatusService _statusService;

        public EquipoService(IEventLogService eventLogService, IEquipoRepository equipoRepository, ITipoEquipoRepository tipoEquipoRepository, IStatusService statusService)
        {
            _eventLogService = eventLogService;
            _equipoRepository = equipoRepository;
            _tipoEquipoRepository = tipoEquipoRepository;
            _statusService = statusService;
        }

        public PagedList<Equipo> GetPagedList(int page = 0, int limit = 10)
        {
            return _equipoRepository.GetPagedList(page, limit);
        }

        public IList<Equipo> GetList()
        {
            return _equipoRepository.GetAll();
        }

        public IList<Equipo> GetListByTipo(Guid tipoId, bool onlineOnly)
        {
            var online = _statusService.Online();
            return _equipoRepository.GetListByTipo(tipoId).Where(x=>x.Estado == online).ToList();
        }

        public Equipo Get(Guid id)
        {
            return _equipoRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdate(Equipo equipo)
        {
            if (!equipo.IsValid()) return ActionConfirmation.CreateFailure("equipo no es válida");

            try
            {
                _equipoRepository.SaveOrUpdate(equipo);
                _equipoRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, equipo.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }

        }


        public ActionConfirmation Delete(Guid id)
        {
            var item = _equipoRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("equipo no existe");

            try
            {
                _equipoRepository.Delete(item.Id);
                _equipoRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("Delete OK (" + item.Modelo + ")");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }

        }


        #region Tipo Equipo

        public PagedList<TipoEquipo> GetTipoEquipoPagedList(int page = 0, int limit = 10)
        {
            return _tipoEquipoRepository.GetPagedList(page, limit);
        }

        public IList<TipoEquipo> GetTipoEquipoList()
        {
            return _tipoEquipoRepository.GetAll();
        }

        public IList<TipoEquipo> GetTipoEquipoList(bool onlineOnly)
        {
            var online = _statusService.Online();
            return _tipoEquipoRepository.GetAll().Where(x => x.Estado == online).ToList();
        }

        public TipoEquipo GetTipoEquipo(Guid id)
        {
            return _tipoEquipoRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdateTipoEquipo(TipoEquipo tipoEquipo)
        {
            if (!tipoEquipo.IsValid()) return ActionConfirmation.CreateFailure("tipo equipo no es válida");

            try
            {
                _tipoEquipoRepository.SaveOrUpdate(tipoEquipo);
                _tipoEquipoRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, tipoEquipo.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }

        }


        public ActionConfirmation DeleteTipoEquipo(Guid id)
        {
            var item = _tipoEquipoRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("tipo equipo no existe");

            try
            {
                _tipoEquipoRepository.Delete(item.Id);
                _tipoEquipoRepository.DbContext.CommitChanges();

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