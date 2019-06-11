using System;
using System.Collections.Generic;
using System.Linq;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;

namespace Oulanka.Services
{
    public class EventLogService : IEventLogService
    {
        private readonly ILogItemService _logItemService;

        public EventLogService(ILogItemService logItemService)
        {
            _logItemService = logItemService;
        }


        /// <summary>
        /// Adds the info.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
        public void AddInfo(
            string messageTitle,
            string messageDescription,
            string category,
            string user,
            string source,
            string objectId,
            bool isVisible)
        {
            var logItem = new LogItem
            {
                Message = messageTitle,
                MessageDescription = messageDescription,
                Category = category,
                EventType = (short)EventType.Informacion,
                Username = user,
                Source = source,
                EventDate = DateTime.Now,
                ObjectId = objectId,
                IsVisible = isVisible,
            };

            Write(logItem);
        }

        /// <summary>
        /// Adds the warn.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
        public void AddWarn(
            string messageTitle,
            string messageDescription,
            string category,
            string user,
            string source,
            string objectId,
            bool isVisible)
        {
            var logItem = new LogItem
            {
                Message = messageTitle,
                Category = category,
                EventType = (short)EventType.Advertencia,
                Username = user,
                Source = source,
                EventDate = DateTime.Now,
                ObjectId = objectId,
                IsVisible = isVisible
            };

            Write(logItem);
        }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
        public void Write(
            string message,
            string category,
            EventType eventType,
            string user,
            string source,
            string objectId,
            bool isVisible)
        {
            var logItem = new LogItem
            {
                Message = message,
                Category = category,
                EventType = (short)eventType,
                Username = user,
                EventDate = DateTime.Now,
                Source = source,
                ObjectId = objectId,
                IsVisible = isVisible
            };

            Write(logItem);
        }



        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="exception">The exception.</param>
        public void AddException(string messageTitle, string messageDescription, string category, Exception exception)
        {
            AddException(messageTitle, messageDescription, category, exception, string.Empty);
        }

        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="user">The user.</param>
        public void AddException(
            string messageTitle, string messageDescription, string category, Exception exception, string user)
        {
            AddException(
                messageTitle,
                messageDescription,
                category,
                exception,
                user,
                Enum.GetName(typeof(EventSource), EventSource.Sistema),
                null);
        }

        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        public void AddException(
            string messageTitle,
            string messageDescription,
            string category,
            Exception exception,
            string user,
            EventSource source)
        {
            AddException(messageTitle, messageDescription, category, exception, user, source, null);
        }

        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        public void AddException(
            string messageTitle,
            string messageDescription,
            string category,
            Exception exception,
            string user,
            string source)
        {
            AddException(
                messageTitle,
                messageDescription,
                category,
                exception,
                user,
                Enum.GetName(typeof(EventSource), source),
                null);
        }

        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        public void AddException(
            string messageTitle,
            string messageDescription,
            string category,
            Exception exception,
            string user,
            EventSource source,
            string objectId)
        {
            AddException(
                messageTitle,
                messageDescription,
                category,
                exception,
                user,
                Enum.GetName(typeof(EventSource), source),
                objectId);
        }

        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        public void AddException(
            string messageTitle,
            string messageDescription,
            string category,
            Exception exception,
            string user,
            string source,
            string objectId)
        {
            var logItem = new LogItem
            {
                Message = messageTitle,
                MessageDescription = messageDescription + " EXCEPTION: " + exception,
                Category = category,
                EventType = (short)EventType.Error,
                Username = user,
                Source = source,
                EventDate = DateTime.Now,
                IsVisible = false,
                ObjectId = objectId,
                ObjectType = ""
            };

            Write(logItem);
        }

        /// <summary>
        /// Adds the info.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        public void AddInfo(string messageTitle, string messageDescription, string category)
        {
            AddInfo(messageTitle, messageDescription, category, string.Empty);
        }

        /// <summary>
        /// Adds the info.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        public void AddInfo(string messageTitle, string messageDescription, string category, string user)
        {
            AddInfo(
                messageTitle, messageDescription, category, user, Enum.GetName(typeof(EventSource), EventSource.Sistema));
        }

        /// <summary>
        /// Adds the info.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        public void AddInfo(
            string messageTitle, string messageDescription, string category, string user, EventSource source)
        {
            AddInfo(
                messageTitle, messageDescription, category, user, Enum.GetName(typeof(EventSource), source), null);
        }

        /// <summary>
        /// Adds the info.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        public void AddInfo(string messageTitle, string messageDescription, string category, string user, string source)
        {
            AddInfo(messageTitle, messageDescription, category, user, source, null);
        }

        /// <summary>
        /// Adds the info.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        public void AddInfo(
            string messageTitle,
            string messageDescription,
            string category,
            string user,
            EventSource source,
            string objectId)
        {
            AddInfo(
                messageTitle, messageDescription, category, user, Enum.GetName(typeof(EventSource), source), objectId);
        }

        /// <summary>
        /// Adds the info.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        public void AddInfo(
            string messageTitle, string messageDescription, string category, string user, string source, string objectId)
        {
            AddInfo(messageTitle, messageDescription, category, user, source, objectId, false);
        }

        /// <summary>
        /// Adds the warn.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        public void AddWarn(string messageTitle, string messageDescription, string category)
        {
            AddWarn(messageTitle, messageDescription, category, string.Empty);
        }

        /// <summary>
        /// Adds the warn.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        public void AddWarn(string messageTitle, string messageDescription, string category, string user)
        {
            AddWarn(
                messageTitle,
                messageDescription,
                category,
                user,
                Enum.GetName(typeof(EventSource), EventSource.Sistema),
                null);
        }

        /// <summary>
        /// Adds the warn.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        public void AddWarn(
            string messageTitle, string messageDescription, string category, string user, EventSource source)
        {
            AddWarn(
                messageTitle, messageDescription, category, user, Enum.GetName(typeof(EventSource), source), null);
        }

        /// <summary>
        /// Adds the warn.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        public void AddWarn(string messageTitle, string messageDescription, string category, string user, string source)
        {
            AddWarn(messageTitle, messageDescription, category, user, source, null);
        }

        /// <summary>
        /// Adds the warn.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        public void AddWarn(
            string messageTitle,
            string messageDescription,
            string category,
            string user,
            EventSource source,
            string objectId)
        {
            AddWarn(
                messageTitle, messageDescription, category, user, Enum.GetName(typeof(EventSource), source), objectId);
        }

        /// <summary>
        /// Adds the warn.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        public void AddWarn(
            string messageTitle, string messageDescription, string category, string user, string source, string objectId)
        {
            AddWarn(messageTitle, messageDescription, category, user, source, objectId, false);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="objectId">The object id.</param>
        /// <returns></returns>
        public IList<LogItem> GetList(string category, string objectId)
        {
            return _logItemService.GetList(category, objectId);
        }

        /// <summary>
        /// Writes the specified log item.
        /// </summary>
        /// <param name="logItem">The log item.</param>
        public void Write(LogItem logItem)
        {
            _logItemService.SaveOrUpdate(logItem);
        }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="eventType">Type of the event.</param>
        public void Write(string message, string category, EventType eventType)
        {
            Write(message, category, eventType, string.Empty);
        }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="user">The user.</param>
        public void Write(string message, string category, EventType eventType, string user)
        {
            Write(message, category, eventType, user, Enum.GetName(typeof(EventSource), EventSource.Sistema), null);
        }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        public void Write(string message, string category, EventType eventType, string user, string source)
        {
            Write(message, category, eventType, user, source, null);
        }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        public void Write(string message, string category, EventType eventType, string user, EventSource source)
        {
            Write(message, category, eventType, user, Enum.GetName(typeof(EventSource), source), null);
        }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        public void Write(
            string message, string category, EventType eventType, string user, EventSource source, string objectId)
        {
            Write(message, category, eventType, user, Enum.GetName(typeof(EventSource), source), objectId);
        }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        public void Write(
            string message, string category, EventType eventType, string user, string source, string objectId)
        {
            Write(message, category, eventType, user, source, objectId, false);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IList<LogItem> GetAll()
        {
            return _logItemService.GetAll().OrderByDescending(l => l.EventDate).ToList();
        }

        public PagedList<LogItem> GetPagedList(int pageIndex = 0, int pageSize = 10)
        {
            return _logItemService.GetPagedList(pageIndex, pageSize);
        }
    }
}