using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Jerarquias;
using Oulanka.Domain.Models.Locales;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class GerenteRegionalRepository : NHibernateRepositoryWithTypedId<GerenteRegional, Guid>, IGerenteRegionalRepository
    {

        public PagedList<GerenteRegional> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GerenteRegional>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<GerenteRegional>(session, page, limit);
        }

        public PagedList<GerenteRegional> GetPagedList(Guid parentId, int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GerenteRegional>()
                .CreateAlias("GerenteNacional", "gerente")
                .Add(Restrictions.Eq("gerente.Id", parentId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<GerenteRegional>(session, page, limit);
        }

        public IList<GerenteRegional> GetList()
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GerenteRegional>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.List<GerenteRegional>();
        }

        public IList<GerenteRegional> GetList(Guid parentId)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GerenteRegional>()
                .CreateAlias("GerenteNacional", "gerente")
                .Add(Restrictions.Eq("gerente.Id", parentId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.List<GerenteRegional>();
        }
    }
}