using System.Collections.Generic;
using Oulanka.Domain.Models;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IEnumerationService
    {
        IList<Enumeration> GetAll();
        IList<Enumeration> GetListByOptionGroup(string optionGroup);
        IList<Enumeration> GetListByOptionName(string optionGroup, string optionName);
        Enumeration GetByValue(string optionGroup, string optionName, string value);

    }
}