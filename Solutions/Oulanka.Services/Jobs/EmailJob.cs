using Oulanka.Configuration.Models;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;

namespace Oulanka.Services.Jobs
{
    public class EmailJob : IJob
    {

        private const int FailureInterval = 15;
        private const int MaxNumberOfTries = 100;
        private readonly IEmailService _emailService;

        public EmailJob(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void Execute(JobItemConfigurationElement jobElement)
        {
           SendQueuedEmail();
        }

        public void SendQueuedEmail()
        {
            _emailService.SendQueuedEmails(FailureInterval, MaxNumberOfTries);
        }
    }
}