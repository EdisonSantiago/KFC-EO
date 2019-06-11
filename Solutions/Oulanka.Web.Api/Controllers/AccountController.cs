using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Oulanka.Domain.Contracts.Services;

namespace Oulanka.Web.Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEventLogService _eventLogService;
        private readonly IUserAccountService _userAccountService;
        private readonly IProjectService _projectService;

        public AccountController(
            IEventLogService eventLogService,
            IUserAccountService userAccountService,
            IProjectService projectService)
        {
            _eventLogService = eventLogService;
            _userAccountService = userAccountService;
            _projectService = projectService;
        }

        

    }
}