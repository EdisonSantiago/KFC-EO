using Oulanka.Domain.Common;
using Oulanka.Domain.Models;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IEmailTemplateService
    {
        EmailTemplate GetTemplate(string emailType);
        void PopulateHeaders(EmailQueueItem email, string to, string[] cc, string[] bcc, bool isHtml);
        void PopulateHeadersToHtmlEmail(EmailQueueItem email, string to, string[] cc, string[] bcc);

    }
}