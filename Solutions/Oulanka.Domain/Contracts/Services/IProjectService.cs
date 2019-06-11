using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IProjectService
    {
        PagedList<Project> GetProjectsPagedList(int page = 0, int limit = 10);
        IList<ProjectDto> GetProjectsByUser(int userId);
        IList<ProjectDto> GetProjectsDto();
        Project GetProjectByIdentifier(string identifier);
        Project GetProject(int projectId);
        ActionConfirmation SaveOrUpdate(Project project);
        ActionConfirmation SaveOrUpdateMember(ProjectMember member);
    }
}