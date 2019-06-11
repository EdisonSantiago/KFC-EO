using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models;

namespace Oulanka.Domain.Contracts.Services
{
    public interface ILogItemService
    {
        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        LogItem Get(int id);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IList<LogItem> GetAll();

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="objectId">The object id.</param>
        /// <returns></returns>
        IList<LogItem> GetList(string category, string objectId);

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="logItem">The log item.</param>
        /// <returns></returns>
        ActionConfirmation SaveOrUpdate(LogItem logItem);

        PagedList<LogItem> GetPagedList(int pageIndex = 0, int pageSize = 10);
    }
}