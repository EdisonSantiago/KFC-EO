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
    public class JefeAreaRepository : NHibernateRepositoryWithTypedId<JefeArea, Guid>, IJefeAreaRepository
    {

        public PagedList<JefeArea> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<JefeArea>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<JefeArea>(session, page, limit);
        }

        public PagedList<JefeArea> GetPagedList(Guid parentId, int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<JefeArea>()
                .CreateAlias("GerenteRegional", "gerente")
                .Add(Restrictions.Eq("gerente.Id", parentId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<JefeArea>(session, page, limit);
        }

        public PagedList<JefeArea> GetPagedList(Guid[] parentIds, int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<JefeArea>()
                .CreateAlias("GerenteRegional", "gerente")
                .Add(Restrictions.In("gerente.Id", parentIds))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<JefeArea>(session, page, limit);
        }

        public IList<JefeArea> GetList()
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<JefeArea>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.List<JefeArea>();
        }

        public IList<JefeArea> GetList(Guid parentId)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<JefeArea>()
                .CreateAlias("GerenteRegional", "gerente")
                .Add(Restrictions.Eq("gerente.Id", parentId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.List<JefeArea>();
        }
    }
}