using NHibernate;

namespace Oulanka.Domain.Extensions
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

    }
}