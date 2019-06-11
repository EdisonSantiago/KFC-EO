using NHibernate.Criterion;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class GroupRepository : NHibernateRepository<Grupo> , IGroupRepository 
    {
        public Grupo GetByName(string groupName)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Grupo>()
                .Add(Restrictions.Eq("Nombre", groupName));

            return criteria.UniqueResult<Grupo>();
        }
    }
}