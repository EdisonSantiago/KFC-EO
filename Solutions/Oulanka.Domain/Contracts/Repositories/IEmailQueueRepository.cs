using System.Collections.Generic;
using Oulanka.Domain.Models;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IEmailQueueRepository : IRepository<EmailQueueItem>
    {
        IList<EmailQueueItem> Dequeue();

        void QueueSendingFailure(EmailQueueItem itemToQueue, int failureInterval, int maxNumberOfTries);

    }
}