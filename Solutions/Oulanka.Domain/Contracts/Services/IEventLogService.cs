using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IEventLogService
    {
        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="exception">The exception.</param>
        void AddException(string messageTitle, string messageDescription, string category, Exception exception);

        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="user">The user.</param>
        void AddException(
            string messageTitle, string messageDescription, string category, Exception exception, string user);

        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        void AddException(
            string messageTitle,
            string messageDescription,
            string category,
            Exception exception,
            string user,
            EventSource source);

        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        void AddException(
            string messageTitle,
            string messageDescription,
            string category,
            Exception exception,
            string user,
            string source);

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
        void AddException(
            string messageTitle,
            string messageDescription,
            string category,
            Exception exception,
            string user,
            EventSource source,
            string objectId);

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
        void AddException(
            string messageTitle,
            string messageDescription,
            string category,
            Exception exception,
            string user,
            string source,
            string objectId);

        /// <summary>
        /// Adds the info.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        void AddInfo(string messageTitle, string messageDescription, string category);

        /// <summary>
        /// Adds the info.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        void AddInfo(string messageTitle, string messageDescription, string category, string user);

        /// <summary>
        /// Adds the info.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        void AddInfo(string messageTitle, string messageDescription, string category, string user, EventSource source);

        /// <summary>
        /// Adds the info.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        void AddInfo(string messageTitle, string messageDescription, string category, string user, string source);

        /// <summary>
        /// Adds the info.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        void AddInfo(
            string messageTitle,
            string messageDescription,
            string category,
            string user,
            EventSource source,
            string objectId);

        /// <summary>
        /// Adds the info.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        void AddInfo(
            string messageTitle, string messageDescription, string category, string user, string source, string objectId);

        /// <summary>
        /// Adds the warn.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        void AddWarn(string messageTitle, string messageDescription, string category);

        /// <summary>
        /// Adds the warn.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        void AddWarn(string messageTitle, string messageDescription, string category, string user);

        /// <summary>
        /// Adds the warn.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        void AddWarn(string messageTitle, string messageDescription, string category, string user, EventSource source);

        /// <summary>
        /// Adds the warn.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        void AddWarn(string messageTitle, string messageDescription, string category, string user, string source);

        /// <summary>
        /// Adds the warn.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        void AddWarn(
            string messageTitle,
            string messageDescription,
            string category,
            string user,
            EventSource source,
            string objectId);

        /// <summary>
        /// Adds the warn.
        /// </summary>
        /// <param name="messageTitle">The message title.</param>
        /// <param name="messageDescription">The message description.</param>
        /// <param name="category">The category.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        void AddWarn(
            string messageTitle, string messageDescription, string category, string user, string source, string objectId);

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="objectId">The object id.</param>
        /// <returns></returns>
        IList<LogItem> GetList(string category, string objectId);

        /// <summary>
        /// Writes the specified log item.
        /// </summary>
        /// <param name="logItem">The log item.</param>
        void Write(LogItem logItem);

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="eventType">Type of the event.</param>
        void Write(string message, string category, EventType eventType);

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="user">The user.</param>
        void Write(string message, string category, EventType eventType, string user);

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        void Write(string message, string category, EventType eventType, string user, string source);

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        void Write(string message, string category, EventType eventType, string user, EventSource source);

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        void Write(
            string message, string category, EventType eventType, string user, EventSource source, string objectId);

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="user">The user.</param>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object id.</param>
        void Write(string message, string category, EventType eventType, string user, string source, string objectId);


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IList<LogItem> GetAll();

        PagedList<LogItem> GetPagedList(int pageIndex = 0, int pageSize = 10);
    }
}