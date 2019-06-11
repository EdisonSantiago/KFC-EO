using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Domain.Models.Locales;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class ImagenLocalRepository : NHibernateRepositoryWithTypedId<ImagenLocal, Guid>, IImagenLocalRepository
    {

        public PagedList<ImagenLocal> GetPagedListByLocal(Guid localId, int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<ImagenLocal>()
                .CreateAlias("Local", "local")
                .Add(Restrictions.Eq("local.Id", localId))
                .AddOrder(Order.Asc("Orden"));

            return criteria.PagedList<ImagenLocal>(session, page, limit);
        }

        public IList<ImagenLocal> GetListByLocal(Guid localId)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<ImagenLocal>()
                .CreateAlias("Local", "local")
                .Add(Restrictions.Eq("local.Id", localId))
                .AddOrder(Order.Asc("Orden"));

            return criteria.List<ImagenLocal>();
        }
    }
}