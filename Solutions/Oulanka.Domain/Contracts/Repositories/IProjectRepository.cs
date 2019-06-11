using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        PagedList<Project> GetPagedList(int page = 0, int limit = 10);
        IList<ProjectDto> GetByUser(int userId);
        IList<ProjectDto> GetDtoList();
        Project GetByIdentifier(string identifier);
    }
}