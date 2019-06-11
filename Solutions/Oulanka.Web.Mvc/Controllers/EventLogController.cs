using System.Web.Mvc;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Web.Core.Controllers;

namespace Oulanka.Web.Mvc.Controllers
{
    public class EventLogController : BaseController
    {
        private IEventLogService _eventLogService;

        public EventLogController(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }

        public ActionResult Index()
        {

            var logs = _eventLogService.GetAll();

            return View(logs);
        }

        [ChildActionOnly]
        public ActionResult ReportByObjectId(string category, string objectId)
        {
            var items = this._eventLogService.GetList(category, objectId);

            return PartialView("_reportByObjectId", items);
        }

    }
}