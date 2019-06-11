using System.Collections.Generic;
using NHibernate.Criterion;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class EnumerationRepository : NHibernateRepository<Enumeration>, IEnumerationRepository
    {

        public IList<Enumeration> GetListByOptionGroup(string optionGroup)
        {
            var session = RepositoryHelper.GetSession();

            var criteria = session.CreateCriteria<Enumeration>();
            criteria.Add(Restrictions.Eq("OptionGroup", optionGroup.ToLower()));

            return criteria.List<Enumeration>();
        }

        public IList<Enumeration> GetListByOptionName(string optionGroup, string optionName)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Enumeration>();
            criteria.Add(Restrictions.Eq("OptionGroup", optionGroup.ToLower()))
                .Add(Restrictions.Eq("OptionName",optionName.ToLower()))
                .AddOrder(Order.Asc("Position"));

            return criteria.List<Enumeration>();
        }

        public Enumeration GetByValue(string optionGroup, string optionName, string value)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Enumeration>();
            criteria.Add(Restrictions.Eq("OptionGroup", optionGroup.ToLower()))
                .Add(Restrictions.Eq("OptionName", optionName.ToLower()))
                .Add(Restrictions.Eq("Value", value))
                ;

            return criteria.UniqueResult<Enumeration>();
        }
    }
}