using System;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Locales;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class TipoEquipoRepository : NHibernateRepositoryWithTypedId<TipoEquipo, Guid>, ITipoEquipoRepository
    {
        public PagedList<TipoEquipo> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<TipoEquipo>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<TipoEquipo>(session, page, limit);
        }
    }
}