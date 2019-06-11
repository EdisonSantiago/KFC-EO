using System;
using System.Collections.Generic;
using System.Linq;
using Oulanka.Domain;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Locales;

namespace Oulanka.Services
{
    public class LocalService : ILocalService
    {
        private readonly ILocalRepository _localRepository;
        private readonly ITipoLocalRepository _tipoLocalRepository;
        private readonly IEventLogService _eventLogService;
        private readonly IEquipoService _equipoService;
        private readonly IStatusService _statusService;
        private readonly IImagenLocalRepository _imagenRepository;


        public LocalService(
            ILocalRepository localRepository,
            IEventLogService eventLogService,
            ITipoLocalRepository tipoLocalRepository,
            IEquipoService equipoService, 
            IStatusService statusService, 
            IImagenLocalRepository imagenRepository)
        {
            _localRepository = localRepository;
            _eventLogService = eventLogService;
            _tipoLocalRepository = tipoLocalRepository;
            _equipoService = equipoService;
            _statusService = statusService;
            _imagenRepository = imagenRepository;
        }


        #region Locales

        public PagedList<Local> GetPagedList(int page = 0, int limit = 10)
        {
            return _localRepository.GetPagedList(page, limit);
        }

        public PagedList<Local> GetPagedList(Guid cadenaId, int page = 0, int limit = 10)
        {
            return _localRepository.GetPagedList(cadenaId, page, limit);

        }

        public PagedList<Local> GetPagedList(Guid cadenaId, bool onlineOnly, int page = 0, int limit = 10)
        {
            var online = _statusService.Online();
            return _localRepository.GetPagedList(cadenaId, online.Id, page, limit);

        }

        public IList<Local> GetList()
        {
            return _localRepository.GetAll();
        }

        public IList<Local> GetList(Guid cadenaId)
        {
            return _localRepository.GetListByCadena(cadenaId);
        }

        public IList<Local> GetList(Guid cadenaId, bool onlineOnly)
        {
            var online = _statusService.Online();
            return _localRepository.GetListByCadena(cadenaId)
                        .Where(x => x.Estado.Equals(online))
                        .ToList();
        }

        public Local Get(Guid id)
        {
            return _localRepository.Get(id);
        }

        public Local GetByCode(string code)
        {
            return _localRepository.GetByCode(code);
        }

        public ActionConfirmation SaveOrUpdate(Local local)
        {
            if (!local.IsValid()) return ActionConfirmation.CreateFailure("cadena no es válida");

            try
            {
                _localRepository.SaveOrUpdate(local);
                _localRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, local.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation Delete(Guid id)
        {
            var item = _localRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("cadena no existe");

            try
            {
                if (item.Equipos.Any())
                {
                    foreach (var equipo in item.Equipos)
                    {
                        _equipoService.Delete(equipo.Id);
                    }
                }


                _localRepository.Delete(item.Id);
                _localRepository.DbContext.CommitChanges();

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

        #region TipoLocales

        public PagedList<TipoLocal> GetTiposPagedList(int page = 0, int limit = 10)
        {
            return _tipoLocalRepository.GetPagedList(page, limit);
        }

        public IList<TipoLocal> GetTiposList()
        {
            return _tipoLocalRepository.GetAll();
        }

        public TipoLocal GetTipo(Guid id)
        {
            return _tipoLocalRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdateTipo(TipoLocal item)
        {
            if (!item.IsValid()) return ActionConfirmation.CreateFailure("objeto no es válida");

            try
            {
                _tipoLocalRepository.SaveOrUpdate(item);
                _tipoLocalRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation DeleteTipo(Guid id)
        {
            var item = _tipoLocalRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("objeto no existe");

            try
            {
                _tipoLocalRepository.Delete(item.Id);
                _tipoLocalRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("Delete OK (" + item.Detalle + ")");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        

        #endregion

        #region Images

        public PagedList<ImagenLocal> GetImages(Guid localId, int page = 0, int limit = 10)
        {
            return _imagenRepository.GetPagedListByLocal(localId, page, limit);
        }

        public IList<ImagenLocal> GetListImages(Guid localId)
        {
            return _imagenRepository.GetListByLocal(localId);
        }

        public ImagenLocal GetImage(Guid id)
        {
            return _imagenRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdateImagen(ImagenLocal imagenLocal)
        {
            if (!imagenLocal.IsValid()) return ActionConfirmation.CreateFailure("imagenLocal no es válida");

            try
            {
                _imagenRepository.SaveOrUpdate(imagenLocal);
                _imagenRepository.DbContext.CommitChanges();

                var confirmation = ActionConfirmation.CreateSuccess("Respuesta " + imagenLocal.Id + " guardada!");
                confirmation.Value = imagenLocal;

                return confirmation;
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, imagenLocal.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        #endregion
    }
}