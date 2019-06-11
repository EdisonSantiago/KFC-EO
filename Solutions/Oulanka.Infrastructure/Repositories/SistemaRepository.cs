using System;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class SistemaRepository : NHibernateRepositoryWithTypedId<Sistema, Guid>, ISistemaRepository
    {
        public PagedList<Sistema> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Sistema>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Sistema>(session, page, limit);
        }
    }
}