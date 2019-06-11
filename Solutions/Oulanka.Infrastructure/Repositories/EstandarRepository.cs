using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Locales;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class EstandarRepository : NHibernateRepositoryWithTypedId<Estandar, Guid>, IEstandarRepository
    {
        public PagedList<Estandar> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Estandar>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Estandar>(session, page, limit);

        }

        public PagedList<Estandar> GetByGrupoPagedList(Guid grupoId, int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Estandar>()
                .CreateAlias("GrupoEstandar", "grupoEstandar")
                .Add(Restrictions.Eq("grupoEstandar.Id", grupoId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Estandar>(session, page, limit);
        }

        public PagedList<Estandar> GetByGrupoPagedList(Guid grupoId, TipoEstandar tipoEstandar = TipoEstandar.Contenedor, int page = 0,
            int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Estandar>()
                .CreateAlias("GrupoEstandar", "grupoEstandar")
                .Add(Restrictions.Eq("grupoEstandar.Id", grupoId))
                .Add(Restrictions.Eq("TipoEstandar",(short)tipoEstandar))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Estandar>(session, page, limit);
        }

        public PagedList<Estandar> GetByParentPagedList(Guid parentId, int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Estandar>()
                .CreateAlias("EstandarPadre", "estandarPadre")
                .Add(Restrictions.Eq("estandarPadre.Id", parentId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Estandar>(session, page, limit);
        }

        

        public IList<Estandar> GetByGrupo(Guid grupoId, TipoEstandar tipoEstandar = TipoEstandar.Contenedor)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Estandar>()
                .CreateAlias("GrupoEstandar", "grupoEstandar")
                .Add(Restrictions.Eq("grupoEstandar.Id", grupoId))
                .Add(Restrictions.Eq("TipoEstandar", (short)tipoEstandar))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.List<Estandar>();
        }

        public IList<Estandar> GetByParent(Guid parentId)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Estandar>()
                .CreateAlias("EstandarPadre", "estandarPadre")
                .Add(Restrictions.Eq("estandarPadre.Id", parentId))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.List<Estandar>();
        }

        public IList<Estandar> GetPicklist(Guid parentId)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Estandar>()
                .CreateAlias("EstandarPadre", "estandarPadre")
                .Add(Restrictions.Eq("estandarPadre.Id", parentId))
                .Add(Restrictions.Eq("TipoEstandar", (short)TipoEstandar.Opcion))
                .AddOrder(Order.Asc("Nombre"));

            return criteria.List<Estandar>();
        }

        public Estandar GetByCodigo(string codigo)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Estandar>()
                .Add(Restrictions.Eq("Codigo", codigo));

            return criteria.UniqueResult<Estandar>();
        }
    }
}