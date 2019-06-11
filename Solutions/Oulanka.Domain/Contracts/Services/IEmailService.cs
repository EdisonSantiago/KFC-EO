using System.Collections.Generic;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IEmailService
    {
        IList<EmailQueueItem> GetEmailsInQueue();
        ActionConfirmation QueueGenericEmail(string to, string @from, string subject, string body);
        void SendQueuedEmails(int failureInterval, int maxNumberOfTries);

    }
}