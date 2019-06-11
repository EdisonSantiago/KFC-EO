using System.Collections.Generic;
using Oulanka.Domain.Models;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IEnumerationRepository : IRepository<Enumeration>
    {
        Enumeration GetByValue(string optionGroup, string optionName, string value);
        IList<Enumeration> GetListByOptionName(string optionGroup, string optionName);
        IList<Enumeration> GetListByOptionGroup(string optionGroup);
    }
}