using System;
using System.Collections.Generic;
using System.Linq;
using Oulanka.Domain;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;

namespace Oulanka.Services
{
    public class CadenaService : ICadenaService
    {
        private readonly ICadenaRepository _cadenaRepository;
        private readonly IEventLogService _eventLogService;


        public CadenaService(ICadenaRepository cadenaRepository, IEventLogService eventLogService)
        {
            _cadenaRepository = cadenaRepository;
            _eventLogService = eventLogService;
        }


        public PagedList<Cadena> GetPagedList(int page = 0, int limit = 10)
        {
            return _cadenaRepository.GetPagedList(page, limit);
        }

        public IList<Cadena> GetList()
        {
            return _cadenaRepository.GetAll();
        }

        public List<Cadena> GetList(bool onlineOnly)
        {
            return _cadenaRepository.GetAll().ToList();
        }

        public Cadena Get(Guid id)
        {
            return _cadenaRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdate(Cadena cadena)
        {
            if (!cadena.IsValid()) return ActionConfirmation.CreateFailure("cadena no es válida");

            try
            {
                _cadenaRepository.SaveOrUpdate(cadena);
                _cadenaRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, cadena.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }

        }


        public ActionConfirmation Delete(Guid id)
        {
            var item = _cadenaRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("cadena no existe");

            try
            {
                _cadenaRepository.Delete(item.Id);
                _cadenaRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("Delete OK (" + item.Nombre + ")");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }

        }
    }
}