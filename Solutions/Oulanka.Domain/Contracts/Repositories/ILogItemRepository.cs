using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface ILogItemRepository : IRepository<LogItem>
    {
        IList<LogItem> GetByCategory(string category);
        IList<LogItem> GetList(string category,string objectId);
        PagedList<LogItem> GetPagedList(int pageIndex = 0, int pageSize = 10);
    }
}