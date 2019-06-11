using System;
using System.Linq;
using Oulanka.Configuration.Models;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;

namespace Oulanka.Services.Jobs
{
    public class UserSessionJob : IJob
    {
        private readonly IUserAccountService _userAccountService;
        private readonly ISettingService _settingService;

        public UserSessionJob(IUserAccountService userAccountService, ISettingService settingService)
        {
            _userAccountService = userAccountService;
            _settingService = settingService;
        }

        public void Execute(JobItemConfigurationElement jobElement)
        {
            var sessionTimeout = _settingService.Get("global","session_timeout").Value;

            var users = _userAccountService.GetUsers().Where(u=>u.EstaEnLinea);
            foreach (var user in users)
            {
                if (DateTime.Now.Subtract(user.UltimaActividadEn) >= TimeSpan.FromMinutes(double.Parse(sessionTimeout)))
                {
                    user.EstaEnLinea = false;
                    _userAccountService.SaveOrUpdateUser(user);
                }
            }
        }
    }
}