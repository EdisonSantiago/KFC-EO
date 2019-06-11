using System.Collections.Generic;
using NHibernate.Criterion;
using NHibernate.Transform;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class ProjectRepository : NHibernateRepository<Project>, IProjectRepository 
    {
        public PagedList<Project> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Project>()
                .AddOrder(Order.Asc("Name"));

            return criteria.PagedList<Project>(session, page, limit);
        }

        public IList<ProjectDto> GetByUser(int userId)
        {
            var session = RepositoryHelper.GetSession();
            var query = session.GetNamedQuery("GetProjectListByUser")
                .SetParameter("userId", userId)
                .SetResultTransformer(Transformers.AliasToBean<ProjectDto>());

            return query.List<ProjectDto>();
        }

        public IList<ProjectDto> GetDtoList()
        {
            var session = RepositoryHelper.GetSession();
            var query = session.GetNamedQuery("GetProjectList")
                .SetResultTransformer(Transformers.AliasToBean<ProjectDto>());

            return query.List<ProjectDto>();
        }

        public Project GetByIdentifier(string identifier)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Project>()
                .Add(Restrictions.Eq("Identifier", identifier));

            return criteria.UniqueResult<Project>();
        }
    }
}