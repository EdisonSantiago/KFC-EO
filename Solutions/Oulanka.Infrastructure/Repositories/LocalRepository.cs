using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models.Locales;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class LocalRepository : NHibernateRepositoryWithTypedId<Local, Guid>, ILocalRepository
    {
        public PagedList<Local> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Local>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Local>(session, page, limit);

        }

        public IList<Local> GetListByCadena(Guid cadenaId)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Local>()
                .CreateAlias("Cadena", "cadena")
                .Add(Restrictions.Eq("cadena.Id", cadenaId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.List<Local>();

        }

        public PagedList<Local> GetPagedList(Guid cadenaId, int page, int limit)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Local>()
                .CreateAlias("Cadena", "cadena")
                .Add(Restrictions.Eq("cadena.Id", cadenaId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Local>(session, page, limit);
        }

        public PagedList<Local> GetPagedList(Guid cadenaId, Guid statusId, int page, int limit)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Local>()
                .CreateAlias("Cadena", "cadena")
                .CreateAlias("Estado", "estado")
                .Add(Restrictions.Eq("cadena.Id", cadenaId))
                .Add(Restrictions.Eq("estado.Id", statusId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Local>(session, page, limit);
        }

        public Local GetByCode(string code)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Local>()
                .Add(Restrictions.Eq("Codigo", code));

            return criteria.UniqueResult<Local>();
        }
    }
}