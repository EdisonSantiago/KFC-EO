using System.Collections.Generic;
using Oulanka.Domain.Models;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IEmailQueueService
    {
        ActionConfirmation Delete(int itemId);
        IList<EmailQueueItem> Dequeue();
        ActionConfirmation Queue(EmailQueueItem item);
        ActionConfirmation QueueSendingFailure(int itemId, int failureInterval, int maxNumberOfTries);
    }
}