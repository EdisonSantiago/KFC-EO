using System;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models.Ubicacion;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class CiudadRepository : NHibernateRepositoryWithTypedId<Ciudad, Guid>, ICiudadRepository
    {
        public PagedList<Ciudad> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Ciudad>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Ciudad>(session, page, limit);
        }
    }
}