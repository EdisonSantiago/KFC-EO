using System;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class CategoriaRepository : NHibernateRepositoryWithTypedId<Categoria, Guid>, ICategoriaRepository
    {
        public PagedList<Categoria> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Categoria>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Categoria>(session, page, limit);
        }
    }
}