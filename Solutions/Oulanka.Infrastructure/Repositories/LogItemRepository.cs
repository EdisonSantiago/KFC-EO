using System.Collections.Generic;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class LogItemRepository : NHibernateRepository<LogItem>, ILogItemRepository
    {
        public IList<LogItem> GetByCategory(string category)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<LogItem>();
            criteria.Add(Restrictions.Eq("Category", category));

            return criteria.List<LogItem>();
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="objectId">The object id.</param>
        /// <returns></returns>
        public IList<LogItem> GetList(string category, string objectId)
        {
            var session = RepositoryHelper.GetSession();

            var criteria = session.CreateCriteria<LogItem>();
            criteria.Add(Restrictions.Eq("Category", category)).Add(Restrictions.Eq("ObjectId", objectId));

            return criteria.List<LogItem>();
        }

        public PagedList<LogItem> GetPagedList(int pageIndex = 0, int pageSize = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<LogItem>()
                .AddOrder(Order.Desc("EventDate"));

            return criteria.PagedList<LogItem>(session, pageIndex, pageSize);
        }
    }
}