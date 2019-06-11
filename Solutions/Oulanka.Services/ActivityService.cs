using System;
using System.Collections.Generic;
using System.Linq;
using Oulanka.Domain;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;

namespace Oulanka.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IEventLogService _eventLogService;

        public ActivityService(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }

        /// <summary>
        ///   Adds the specified activity type.
        /// </summary>
        /// <param name = "activityType">Type of the activity.</param>
        /// <param name = "eventType">Type of the event.</param>
        /// <param name = "message">The message.</param>
        /// <param name = "objectId">The object id.</param>
        /// <param name = "objectType">Type of the object.</param>
        /// <param name = "username">The username.</param>
        /// <returns></returns>
        public ActionConfirmation Add(
            string activityType,
            EventType eventType,
            string message,
            string objectId,
            string objectType,
            string username)
        {
            var logItem = new LogItem
            {
                Category = EventCategory.Actividad.ToString(),
                EventDate = DateTime.Now,
                IsVisible = true,
                Message = activityType,
                MessageDescription = message,
                Username = username,
                ObjectId = objectId,
                ObjectType = objectType,
                EventType = (short)eventType,
                Source = EventSource.Sistema.ToString()
            };

            ActionConfirmation confirmation;
            try
            {
                _eventLogService.Write(logItem);
                confirmation = ActionConfirmation.CreateSuccess("Activity Added");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(
                    exception.Message, exception.ToString(), EventCategory.NoDefinida.ToString(), exception);

                confirmation =
                    ActionConfirmation.CreateFailure("Activity cannot be added > " + exception.Message);
            }

            return confirmation;
        }

        /// <summary>
        /// Gets the latest.
        /// </summary>
        /// <returns></returns>
        public IList<LogItem> GetLatest()
        {
            var category = EventCategory.Actividad.ToString();

            var items =
                _eventLogService.GetAll()
                .Where(l => l.Category == category)
                .OrderByDescending(l => l.EventDate)
                .Take(10)
                .ToList();

            return items;
        }

    }
}