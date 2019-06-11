using System;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class CadenaRepository : NHibernateRepositoryWithTypedId<Cadena, Guid>, ICadenaRepository
    {
        public PagedList<Cadena> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Cadena>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Cadena>(session, page, limit);
        }
    }
}