using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using Oulanka.Configuration;
using Oulanka.Domain;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;

namespace Oulanka.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfigurationSettings _configuration;
        private readonly ISettingService _settingService;
        private readonly IEmailTemplateService _templateService;
        private readonly IEmailQueueService _queueService;
       

        public EmailService(
            IConfigurationSettings configuration,
            IEmailTemplateService templateService,
            IEmailQueueService queueService,
            ISettingService settingService)
        {
            _configuration = configuration;
            _templateService = templateService;
            _queueService = queueService;
            _settingService = settingService;
        }

        public IList<EmailQueueItem> GetEmailsInQueue()
        {
            return _queueService.Dequeue();
        }

        public ActionConfirmation QueueGenericEmail(string to, string @from, string subject, string body)
        {
            var formatList = new FormatList();
            formatList.Add("Body", body);
            formatList.Add("useremail", from);
            formatList.Add("Subject", subject);


            var email = GenericEmail("GenericEmail", null, null, true);
            if (email == null)
            {
                return ActionConfirmation.CreateFailure("Email template not found!");
            }

            email.To = to;
            email.From = from;
            email.Body = formatList.Format(email.Body);
            email.Subject = formatList.Format(email.Subject);

            return _queueService.Queue(email);
        }






        public void SendQueuedEmails(int failureInterval, int maxNumberOfTries)
        {
            var configuration = _configuration.GetConfig();
            var emails = _queueService.Dequeue();
            var failure = new List<EmailQueueItem>();

            var smtp = new SmtpClient(configuration.SmtpServer, configuration.SmtpServerPort);
            if (!string.IsNullOrEmpty(configuration.SmtpServerUsername) && !string.IsNullOrEmpty(configuration.SmtpServerPassword))
            {
                smtp.Credentials = new NetworkCredential(configuration.SmtpServerUsername, configuration.SmtpServerPassword);
            }

            var connectionLimit = configuration.SmtpServerConnectionLimit;
            var sentCount = 0;
            var totalSent = 0;

            foreach (var item in emails)
            {
                try
                {
                    var message = CreateEmailMessageFromQueue(item);
                    message.Headers.Add("X-EB-EmailID", item.Id.ToString());
                    message.Headers.Add("X-EB-Attempts", (item.NumberOfTries + 1).ToString());
                    message.Headers.Add("X-EB-AppDomain", AppDomain.CurrentDomain.FriendlyName);
                    message.BodyEncoding = Encoding.UTF8;

                    // Replace any LF characters with CR LF
                    message.Body = message.Body.Replace("\r", string.Empty);
                    message.Body = message.Body.Replace("\n", "\r\n");

                    smtp.Send(message);

                    _queueService.Delete(item.Id);

                    if (connectionLimit != -1 && ++sentCount >= connectionLimit)
                    {
                        Thread.Sleep(new TimeSpan(0, 0, 0, 15, 0));
                        sentCount = 0;
                    }

                }
                catch (Exception)
                {
                    failure.Add(item);
                }

                ++totalSent;
            }

            if (failure.Count > 0)
            {
                foreach (var fail in failure)
                {
                    _queueService.QueueSendingFailure(fail.Id, failureInterval, maxNumberOfTries);
                }
            }

            if (totalSent > 0 || failure.Count > 0)
            {
                var infoLog = $"Email Queue:\n Messages Sent: {totalSent}\nMessages Failed: {failure.Count}";

            }

        }




        private static MailMessage CreateEmailMessageFromQueue(EmailQueueItem email)
        {
            var message = new MailMessage
            {
                From = new MailAddress(email.From),
                Priority = (MailPriority)email.Priority,
                IsBodyHtml = email.IsBodyHtml,
                Subject = email.Subject,
                Body = email.Body,
                BodyEncoding = email.BodyEncoding
            };

            var toArray = email.To.Split(';');
            foreach (var t in toArray)
            {
                message.To.Add(t);
            }

            return message;
        }

        private EmailQueueItem GenericEmail(string emailType, string[] cc, string[] bcc, bool isHtml)
        {
            var template = _templateService.GetTemplate(emailType);
            if (template == null)
            {
                return null;
            }

            var queue = new EmailQueueItem
            {
                IsBodyHtml = isHtml,
                From = template.From,
                Subject = template.Subject,
                Body = template.Body,
                Priority = (short)template.Priority,
                CreatedOn = DateTime.Now
            };

            _templateService.PopulateHeaders(queue, null, cc, bcc, isHtml);

            return queue;
        }

        private EmailQueueItem ActionEmail(ActionType actionType, string[] cc, string[] bcc, bool isHtml)
        {
            var emailType = "Action_" + actionType;

            var template = _templateService.GetTemplate(emailType);
            if (template == null)
            {
                template = _templateService.GetTemplate("Action");
                if (template == null)
                    return null;
            }


            var queue = new EmailQueueItem
            {
                IsBodyHtml = isHtml,
                From = template.From,
                Subject = template.Subject,
                Body = template.Body,
                Priority = (short)template.Priority,
                CreatedOn = DateTime.Now
            };

            _templateService.PopulateHeaders(queue, null, cc, bcc, isHtml);

            return queue;
        }
        private EmailQueueItem PanelActionEmail(ActionType actionType, string[] cc, string[] bcc, bool isHtml)
        {
            var emailType = "PanelAction_" + actionType;

            var template = _templateService.GetTemplate(emailType);
            if (template == null)
            {
                template = _templateService.GetTemplate("PanelAction");
                if (template == null)
                    return null;
            }


            var queue = new EmailQueueItem
            {
                IsBodyHtml = isHtml,
                From = template.From,
                Subject = template.Subject,
                Body = template.Body,
                Priority = (short)template.Priority,
                CreatedOn = DateTime.Now
            };

            _templateService.PopulateHeaders(queue, null, cc, bcc, isHtml);

            return queue;
        }

    }
}