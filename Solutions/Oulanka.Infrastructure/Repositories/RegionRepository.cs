using System;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models.Ubicacion;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class RegionRepository : NHibernateRepositoryWithTypedId<Region,Guid>, IRegionRepository
    {
        public PagedList<Region> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Region>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Region>(session, page, limit);
        }

        


    }
}