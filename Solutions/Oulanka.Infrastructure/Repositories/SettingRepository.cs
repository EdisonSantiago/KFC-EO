using NHibernate.Criterion;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class SettingRepository : NHibernateRepository<Setting>, ISettingRepository 
    {
        public Setting Get(string option, string name)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Setting>()
                .Add(Restrictions.Eq("OptionName", option))
                .Add(Restrictions.Eq("Name", name));

            return criteria.UniqueResult<Setting>();
        }
    }
}