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
    public class GerenteNacionalRepository : NHibernateRepositoryWithTypedId<GerenteNacional, Guid>, IGerenteNacionalRepository
    {

        public PagedList<GerenteNacional> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GerenteNacional>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<GerenteNacional>(session, page, limit);
        }

        public PagedList<GerenteNacional> GetPagedList(Guid parentId, int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GerenteNacional>()
                .CreateAlias("GerenteGeneral", "gerente")
                .Add(Restrictions.Eq("gerente.Id", parentId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<GerenteNacional>(session, page, limit);
        }

        public PagedList<GerenteNacional> GetByCadenaPagedList(Guid cadenaId, int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GerenteNacional>()
                .CreateAlias("Cadena", "cadena")
                .Add(Restrictions.Eq("cadena.Id", cadenaId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<GerenteNacional>(session, page, limit);
        }

        public IList<GerenteNacional> GetList()
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GerenteNacional>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.List<GerenteNacional>();
        }

        public IList<GerenteNacional> GetList(Guid parentId)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GerenteNacional>()
                .CreateAlias("GerenteGeneral", "gerente")
                .Add(Restrictions.Eq("gerente.Id", parentId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.List<GerenteNacional>();
        }

        public IList<GerenteNacional> GetListByCadena(Guid cadenaId)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<GerenteNacional>()
                .CreateAlias("Cadena", "cadena")
                .Add(Restrictions.Eq("cadena.Id", cadenaId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.List<GerenteNacional>();
        }
    }
}