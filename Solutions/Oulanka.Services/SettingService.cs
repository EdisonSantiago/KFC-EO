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
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IEventLogService _eventLogService;

        public SettingService(ISettingRepository settingRepository, IEventLogService eventLogService)
        {
            _settingRepository = settingRepository;
            _eventLogService = eventLogService;
        }

        public IList<Setting> GetAll()
        {
            var settings = _settingRepository.GetAll().OrderBy(s => s.OptionName);
            return settings.ToList();
        }

        public Setting Get(int id)
        {
            return _settingRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdate(Setting dbSetting)
        {
            if (dbSetting.IsValid())
            {
                try
                {
                    _settingRepository.SaveOrUpdate(dbSetting);
                    _settingRepository.DbContext.CommitChanges();

                    return ActionConfirmation.CreateSuccess("setting saved ok!");
                }
                catch (System.Exception exception)
                {
                    _eventLogService.AddException(exception.Message,
                        exception.StackTrace,EventCategory.GuardarObjeto.ToString(),exception,dbSetting.UpdatedBy,EventSource.Sistema);

                    var confirmation = ActionConfirmation.CreateFailure("setting not saved | " + exception.ToString());
                    return confirmation;
                }
            }
            else
            {
                return ActionConfirmation.CreateFailure("setting is not valid!");
            }
        }

        public ActionConfirmation Delete(int id)
        {
            var setting = Get(id);
            if (setting == null)
                return ActionConfirmation.CreateFailure("Setting does not exist");

            try
            {
                _settingRepository.Delete(setting.Id);
                return ActionConfirmation.CreateSuccess("setting deleted!");
            }
            catch (System.Exception exception)
            {
                _eventLogService.AddException(exception.Message, exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, "", EventSource.Sistema);

                var confirmation = ActionConfirmation.CreateFailure("setting not deleted | " + exception);
                return confirmation;

            }

        }

        public Setting Get(string option, string name)
        {
            if (option == null) throw new ArgumentNullException(nameof(option));
            if (name == null) throw new ArgumentNullException(nameof(name));

            return _settingRepository.Get(option, name);
        }

        public IList<Setting> GetByGroup(string groupName)
        {
            return GetAll().Where(x=>x.OptionName == groupName).ToList();
        }
    }
}