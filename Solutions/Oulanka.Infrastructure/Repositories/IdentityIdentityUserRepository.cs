using System;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Criterion;
using NHibernate.Linq;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models.Identity;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class IdentityIdentityUserRepository : NHibernateRepository<IdentityUser>, IIdentityUserRepository 
    {
        public IdentityUser GetUser(Expression<Func<IdentityUser, bool>> filter) 
        {
            var session = RepositoryHelper.GetSession();

            var query = session.Query<IdentityUser>().Where(filter);
            query.Fetch(p => p.Roles).ToFuture();
            query.Fetch(p => p.Logins).ToFuture();
            query.Fetch(p => p.Claims).ToFuture();
            return query.ToFuture().FirstOrDefault();
        }

        public IdentityUser GetById(string userId)
        {
            var session = RepositoryHelper.GetSession();

            var criteria = session.CreateCriteria<IdentityUser>()
                .Add(Restrictions.Eq("UserId", userId));

            return criteria.UniqueResult<IdentityUser>();
        }

        public IdentityUser GetUserFromLogin(string loginProvider, string providerKey)
        {
            var session = RepositoryHelper.GetSession();
            var query = from user in session.Query<IdentityUser>()
                from l in user.Logins
                where l.LoginProvider == loginProvider &&
                      l.ProviderKey == providerKey
                select user;

            return query.SingleOrDefault();
        }
    }
}