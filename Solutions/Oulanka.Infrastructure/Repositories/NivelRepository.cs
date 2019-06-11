using System;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class NivelRepository : NHibernateRepositoryWithTypedId<Nivel, Guid>, INivelRepository
    {
        public PagedList<Nivel> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Nivel>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Nivel>(session, page, limit);
        }
    }
}