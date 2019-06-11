using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class StatusRepository : NHibernateRepositoryWithTypedId<Estado, Guid>, IStatusRepository

    {
        public IList<Estado> GetItems(string grupo)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Estado>()
                .Add(Restrictions.Eq("Grupo", grupo))
                .AddOrder(Order.Asc("Nombre"));


            return criteria.List<Estado>();
        }

    }
}