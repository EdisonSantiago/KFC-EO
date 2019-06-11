using System;
using System.Collections.Generic;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models;

namespace Oulanka.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ISettingService _settingService;
        private readonly IProjectService _projectService;

        public DashboardService(ISettingService settingService, IProjectService projectService)
        {
            _settingService = settingService;
            _projectService = projectService;
        }


    }
}