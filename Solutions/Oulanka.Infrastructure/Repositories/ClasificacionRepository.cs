using System;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class ClasificacionRepository : NHibernateRepositoryWithTypedId<Clasificacion, Guid>, IClasificacionRepository
    {
        public PagedList<Clasificacion> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Clasificacion>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Clasificacion>(session, page, limit);
        }
    }
}