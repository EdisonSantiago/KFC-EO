using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class UserRepository : NHibernateRepository<Usuario>, IUserRepository
    {
        public Usuario GetUser(string username)
        {
            var session = RepositoryHelper.GetSession();

            var criteria = session.CreateCriteria<Usuario>()
                .Add(Restrictions.Eq("NombreUsuario", username));

            return criteria.UniqueResult<Usuario>();

        }

        public Usuario GetUserByEmail(string email)
        {
            var session = RepositoryHelper.GetSession();

            var criteria = session.CreateCriteria<Usuario>()
                .Add(Restrictions.Eq("Email", email));

            return criteria.UniqueResult<Usuario>();

        }

        public PagedList<Usuario> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Usuario>()
                .AddOrder(Order.Asc("NombreUsuario"));

            return criteria.PagedList<Usuario>(session, page, limit);
        }
    }
}