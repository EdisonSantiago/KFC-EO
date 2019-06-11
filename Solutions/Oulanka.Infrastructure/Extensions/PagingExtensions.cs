using System.Collections.Generic;
using FluentNHibernate.Utils;
using NHibernate;
using NHibernate.Criterion;
using Oulanka.Domain.Common;

namespace Oulanka.Infrastructure.Extensions
{
    public static class PagingExtensions
    {
        public static ICriteria Page(this ICriteria criteria, int pageIndex, int pageSize)
        {
            return criteria.SetFirstResult(pageIndex*pageSize)
                .SetMaxResults(pageSize);
        }

        public static IQuery Page(this IQuery query, int pageIndex, int pageSize)
        {
            return query.SetFirstResult(pageIndex*pageSize)
                .SetMaxResults(pageIndex);
        }

        public static PagedList<T> PagedList<T>(this ICriteria criteria, ISession session, int pageIndex, int pageSize) where T : class
        {
            if (pageIndex < 0)
                pageIndex = 0;

            var countCrit = (ICriteria)criteria.Clone();
            countCrit.ClearOrders(); // so we don't have missing group by exceptions

            var results = session.CreateMultiCriteria()
                .Add<long>(countCrit.SetProjection(Projections.RowCountInt64()))
                .Add<T>(criteria.SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize))
                .List();

            var totalCount = ((IList<long>)results[0])[0];

            return new PagedList<T>((IList<T>)results[1], totalCount, pageIndex, pageSize);
        }

    }
}