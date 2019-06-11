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
    public class GerenteGeneralRepository : NHibernateRepositoryWithTypedId<GerenteGeneral, Guid>, IGerenteGeneralRepository
    {

        public PagedList<GerenteGeneral> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GerenteGeneral>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<GerenteGeneral>(session, page, limit);
        }


        public IList<GerenteGeneral> GetList()
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GerenteGeneral>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.List<GerenteGeneral>();
        }

    }
}