using System;
using System.Web.Mvc;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models;
using Oulanka.Web.Core.Attributes;
using Oulanka.Web.Core.Controllers;

namespace Oulanka.Web.Mvc.Controllers
{
    [AuthorizeGroup(Groups = "admins")]
    public class SettingsController : BaseController
    {
        private readonly ISettingService _settingService;
        private readonly IEventLogService _eventLogService;
        private readonly IEmailService _emailService;

        public SettingsController(
            ISettingService settingService, 
            IEventLogService eventLogService, 
            IEmailService emailService)
        {
            _settingService = settingService;
            _eventLogService = eventLogService;
            _emailService = emailService;
        }

        public ActionResult Index()
        {
            this.LogEnterToAction();

            return View();
        }

        public ActionResult AppSettings()
        {
            LogEnterToAction();

            return View();
        }

        public JsonResult GetAppSettings()
        {
            var settings = _settingService.GetAll();
            return Json(settings, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveAppSetting(Setting setting)
        {
            Setting dbSetting = null;
            dbSetting = setting.Id <= 0
                ? new Setting()
                : _settingService.Get(setting.Id);

            dbSetting.OptionName = setting.OptionName;
            dbSetting.Name = setting.Name;
            dbSetting.Value = setting.Value;

            if (setting.Id <= 0)
            {
                dbSetting.CreatedOn = DateTime.Now;
                dbSetting.CreatedBy = User.Identity.Name;
            }

            dbSetting.UpdatedOn = DateTime.Now;
            dbSetting.UpdatedBy = User.Identity.Name;

            var confirmation = _settingService.SaveOrUpdate(dbSetting);
            return Json(confirmation.WasSuccessful);
        }

        public JsonResult DeleteAppSetting(int id)
        {
            var item = _settingService.Get(id);
            if (item == null)
                return Json(false);

            var itemName = item.Name + "|" + item.Value;
            var confirmation = _settingService.Delete(item.Id);
            if (confirmation.WasSuccessful)
            {
                LogSaveObjectAction(savedObject: $"{itemName} deleted");
                LogSaveObjectAction(savedObject: $"{itemName} deleted");
            }
            else
            {
                LogErrorObjectAction(confirmation.Message, $"{itemName}");
            }

            return Json(new { status = confirmation.WasSuccessful, message = confirmation.Message });

        }

        public ActionResult EventLog()
        {
            this.LogEnterToAction();

            return View();
        }

        public JsonResult GetEventLog(int page = 1, int limit = 10)
        {
            var logs = _eventLogService.GetPagedList(page, limit);
            var records = logs.Items;
            var total = logs.TotalCount;

            return Json(new {records,total},JsonRequestBehavior.AllowGet);
        }


    }
}