using System;
using System.Collections.Generic;
using System.Linq;
using Oulanka.Domain;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;

namespace Oulanka.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IEventLogService _eventLogService;
        private readonly ISettingService _settingService;

        public StatusService(
            IStatusRepository statusRepository,
            IEventLogService eventLogService,
            ISettingService settingService)
        {
            _statusRepository = statusRepository;
            _eventLogService = eventLogService;
            _settingService = settingService;
        }

        public IList<Estado> GetItems()
        {
            return _statusRepository.GetAll().OrderBy(x => x.Nombre).ToList();
        }

        public IList<Estado> GetItems(string grupo)
        {
            return _statusRepository.GetItems(grupo);
        }

        public Estado Get(Guid id)
        {
            return _statusRepository.Get(id);
        }

        public Estado Online()
        {
            var onlineStatus = _settingService.Get("global", "estado_online_id").Value;
            return Get(Guid.Parse(onlineStatus));
        }

        public ActionConfirmation SaveOrUpdate(Estado estado)
        {
            if (!estado.IsValid()) return ActionConfirmation.CreateFailure("not valid!");

            try
            {
                _statusRepository.SaveOrUpdate(estado);
                _statusRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, estado.ActualizadoPor, EventSource.Sistema);
                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation Delete(Guid id)
        {
            var status = Get(id);
            if (status == null)
                return ActionConfirmation.CreateFailure("Status no existe");

            try
            {
                _statusRepository.Delete(status);
                return ActionConfirmation.CreateSuccess("status borrado!");
            }
            catch (System.Exception exception)
            {
                _eventLogService.AddException(exception.Message, exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, "", EventSource.Sistema);

                var confirmation = ActionConfirmation.CreateFailure("status no eliminado | " + exception);
                return confirmation;

            }
        }
    }
}