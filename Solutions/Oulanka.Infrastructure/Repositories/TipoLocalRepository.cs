using System;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models.Locales;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class TipoLocalRepository : NHibernateRepositoryWithTypedId<TipoLocal, Guid>, ITipoLocalRepository
    {
        public PagedList<TipoLocal> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<TipoLocal>()
                .AddOrder(Order.Asc("Detalle"));

            return criteria.PagedList<TipoLocal>(session, page, limit);

        }
    }
}