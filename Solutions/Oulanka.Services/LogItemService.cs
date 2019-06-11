using System;
using System.Collections.Generic;
using System.Linq;
using Oulanka.Domain;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models;

namespace Oulanka.Services
{
    public class LogItemService : ILogItemService
    {
        private readonly ILogItemRepository _logItemRepository;

        public LogItemService(ILogItemRepository logItemRepository)
        {
            _logItemRepository = logItemRepository;
        }
        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public LogItem Get(int id)
        {
            return this._logItemRepository.Get(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IList<LogItem> GetAll()
        {
            return this._logItemRepository.GetAll();
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="objectId">The object id.</param>
        /// <returns></returns>
        public IList<LogItem> GetList(string category, string objectId)
        {
            return this._logItemRepository.GetList(category, objectId).OrderBy(l => l.EventDate).ToList();
        }

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="logItem">The log item.</param>
        /// <returns></returns>
        public ActionConfirmation SaveOrUpdate(LogItem logItem)
        {
            try
            {
                if (logItem.IsValid())
                {
                    this._logItemRepository.SaveOrUpdate(logItem);
                    _logItemRepository.DbContext.CommitChanges();


                    ActionConfirmation confirmation = ActionConfirmation.CreateSuccess("Log Added!");
                    confirmation.Value = logItem;

                    return confirmation;
                }
                else
                {
                    this._logItemRepository.DbContext.RollbackTransaction();

                    return
                        ActionConfirmation.CreateFailure(
                            "The log item could not be saved due to missing or invalid information");
                }
            }
            catch (Exception exception)
            {
                return ActionConfirmation.CreateFailure("Cannot insert into the log > " + exception.Message);
            }
        }

        public PagedList<LogItem> GetPagedList(int pageIndex = 0, int pageSize = 10)
        {
            return _logItemRepository.GetPagedList(pageIndex, pageSize);
        }
    }
}