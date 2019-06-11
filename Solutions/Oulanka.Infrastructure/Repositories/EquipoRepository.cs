using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Locales;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class EquipoRepository : NHibernateRepositoryWithTypedId<Equipo, Guid>, IEquipoRepository
    {
        public PagedList<Equipo> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Equipo>()
                .AddOrder(Order.Asc("Modelo"));

            return criteria.PagedList<Equipo>(session, page, limit);
        }

        public IList<Equipo> GetListByTipo(Guid tipoId)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Equipo>()
                .CreateAlias("TipoEquipo", "tipoEquipo")
                .Add(Restrictions.Eq("tipoEquipo.Id", tipoId))
                .AddOrder(Order.Asc("Modelo"));

            return criteria.List<Equipo>();
        }
    }
}