using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Locales;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class GrupoEstandarRepository : NHibernateRepositoryWithTypedId<GrupoEstandar,Guid>, IGrupoEstandarRepository
    {
        public PagedList<GrupoEstandar> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GrupoEstandar>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<GrupoEstandar>(session, page, limit);

        }

        public GrupoEstandar GetByCodigo(string codigo)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GrupoEstandar>();

            return criteria.UniqueResult<GrupoEstandar>();
        }

        public IList<GrupoEstandar> GetByTipoLocalList(Guid tipoLocalId)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GrupoEstandar>()
                .CreateAlias("TipoLocales", "tipoLocales")
                .Add(Restrictions.Eq("tipoLocales.Id", tipoLocalId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.List<GrupoEstandar>();
        }
    }
}