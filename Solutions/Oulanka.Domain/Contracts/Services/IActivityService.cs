using System.Collections.Generic;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IActivityService
    {
        /// <summary>
        /// Adds the specified activity type.
        /// </summary>
        /// <param name="activityType">Type of the activity.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="message">The message.</param>
        /// <param name="objectId">The object id.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        ActionConfirmation Add(string activityType, EventType eventType, string message, string objectId, string objectType, string username);

        /// <summary>
        /// Gets the latest.
        /// </summary>
        /// <returns></returns>
        IList<LogItem> GetLatest();
    }
}