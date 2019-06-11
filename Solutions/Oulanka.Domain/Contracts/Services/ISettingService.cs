using System.Collections.Generic;
using Oulanka.Domain.Models;

namespace Oulanka.Domain.Contracts.Services
{
    public interface ISettingService
    {
        IList<Setting> GetAll();
        Setting Get(int id);
        ActionConfirmation SaveOrUpdate(Setting dbSetting);
        ActionConfirmation Delete(int id);
        Setting Get(string option, string name);
        IList<Setting> GetByGroup(string groupName);
    }
}