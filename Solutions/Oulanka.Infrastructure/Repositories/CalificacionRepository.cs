using System;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class CalificacionRepository : NHibernateRepositoryWithTypedId<Calificacion, Guid>, ICalificacionRepository
    {
        public PagedList<Calificacion> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Calificacion>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Calificacion>(session, page, limit);
        }
    }
}