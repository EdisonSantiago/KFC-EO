using NHibernate.Criterion;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models.Identity;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class IdentityRoleRepository : NHibernateRepository<IdentityRole>, IIdentityRoleRepository
    {
        public IdentityRole GetByName(string roleName)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<IdentityRole>()
                .Add(Restrictions.Eq("Name", roleName));

            return criteria.UniqueResult<IdentityRole>();
        }

        public IdentityRole GetById(string roleId)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<IdentityRole>()
                .Add(Restrictions.Eq("Id", roleId));

            return criteria.UniqueResult<IdentityRole>();
        }
    }
}