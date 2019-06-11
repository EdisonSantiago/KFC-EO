using System;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Domain.Models.Locales;
using Oulanka.Domain.Models.Personal;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class PosicionRepository : NHibernateRepositoryWithTypedId<Posicion, Guid>, IPosicionRepository
    {
        public PagedList<Posicion> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Posicion>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Posicion>(session, page, limit);
        }
    }
}