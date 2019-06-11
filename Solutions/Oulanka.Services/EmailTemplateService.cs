using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Xml;
using Oulanka.Configuration;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models;

namespace Oulanka.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private const string TemplateFileName = "emails.xml";
        private readonly IConfigurationSettings _configuration;

        public EmailTemplateService(IConfigurationSettings configuration)
        {
            _configuration = configuration;
        }

        public EmailTemplate GetTemplate(string emailType)
        {
            if (string.IsNullOrEmpty(emailType)) throw new ArgumentNullException(nameof(emailType)); 

            EmailTemplate template = null;
            var templates = LoadTemplates();
            if (templates.ContainsKey(emailType))
            {
                template = (EmailTemplate)templates[emailType];
            }

            return template;
        }


        private Hashtable LoadTemplates()
        {
            var templates = new Hashtable();
            var physicalPath =
                HostingEnvironment.MapPath(
                    $"{_configuration.GetConfig().EmailTemplatesLocation}{TemplateFileName}");

            FileInfo fileInfo;
            try
            {
                fileInfo = new FileInfo(physicalPath);
            }
            catch
            {
                throw new FileNotFoundException("No email templates found.");
            }

            var reader = fileInfo.OpenRead();
            var document = new XmlDocument();
            document.Load(reader);
            reader.Close();

            foreach (XmlNode node in document.GetElementsByTagName("email"))
            {
                var template = new EmailTemplate(node);
                templates.Add(template.EmailType, template);
            }

            return templates;
        }


        public void PopulateHeaders(EmailQueueItem email, string to, string[] cc, string[] bcc, bool isHtml)
        {
            if (isHtml)
            {
                email.IsBodyHtml = true;
                email.Body =
                    $"<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /></head><body>{FormatPlainTextAsHtml(email.Body).Trim()}</body></html>";
            }

            if (!string.IsNullOrEmpty(to))
            {
                email.To = to;
            }

            if (cc != null)
            {
                email.Cc = cc.ToDelimitedString(",");
            }

            if (bcc != null)
            {
                email.Bcc = bcc.ToDelimitedString(",");
            }
        }

        public void PopulateHeadersToHtmlEmail(EmailQueueItem email, string to, string[] cc, string[] bcc)
        {
            email.IsBodyHtml = true;
            var bodyStyle =
                "bgcolor=\"#F8F8F8\" style=\"font-variant: normal; font-weight: normal; font-size: 12px; margin: 0; background-repeat: repeat-x; background-color: #F8F8F8; line-height: 17px; font-family: 'Lucida Grande' , 'Lucida Sans Unicode', Verdana, sans-serif; padding: 0; text-align: center;  font-style: normal;\"";
            email.Body =
                $"<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /></head><body {bodyStyle}>{FormatEmail(email.Body.Trim())}</body></html>";

            if (!string.IsNullOrEmpty(to))
            {
                email.To = to;
            }

            if (cc != null)
            {
                email.Cc = cc.ToDelimitedString(",");
            }

            if (bcc != null)
            {
                email.Bcc = bcc.ToDelimitedString(",");
            }
        }

        protected string FormatEmail(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            value = value.Replace("\r", string.Empty);

            value = Regex.Replace(
                value,
                "\\[server\\]",
                _configuration.GetConfig().HostPath,
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            value = Regex.Replace(
                value,
                "\\[host\\]",
                _configuration.GetConfig().HostPath,
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            value = Regex.Replace(
                value,
                "\\[CurrentDate\\]",
                DateTime.Now.ToLongDateString(),
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return value;
        }

        protected string FormatHtmlAsPlainText(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            // get rid of extra line breaks
            value = Regex.Replace(value, "\n", " ", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            value = value.Replace("\r", string.Empty);

            // add linebreaks from HTML for <br>, <p>, <li>, and <blockquote> tags
            value = Regex.Replace(
                value, @"</?(br|p|li|blockquote)(\s/)?>", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            // strip all remaining HTML
            value = Regex.Replace(
                value,
                @"</?(\w+)(\s*\w*\s*=\s*(string.Empty[^string.Empty]*string.Empty|'[^']'|[^>]*))*|/?>",
                string.Empty,
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            // server address
            value = Regex.Replace(
                value,
                "\\[server\\]",
                _configuration.GetConfig().HostPath,
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            value = Regex.Replace(
                value,
                "\\[host\\]",
                _configuration.GetConfig().HostPath,
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            value = Regex.Replace(
                value,
                "\\[CurrentDate\\]",
                DateTime.Now.ToLongDateString(),
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            // replace special characters
            value = value.Replace("&nbsp;", " ");
            value = value.Replace("&lt;", "<");
            value = value.Replace("&gt;", ">");
            value = value.Replace("&amp;", "&");
            value = value.Replace("&quot;", "\"");

            return value;
        }


        protected string FormatPlainTextAsHtml(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            value = Regex.Replace(value, "\n", "<br />", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            value = value.Replace("\r", string.Empty);

            // make urls clickable
            //value = Regex.Replace(value, @"(http|ftp|https):\/\/[\w]+(.[\w]+)([\w\-\.,@?^=%&:/~\+#\$]*[\w\-\@?^=%&/~\+#])?", "<a href=\"$0\">$0</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            value = Regex.Replace(
                value,
                "\\[server\\]",
                _configuration.GetConfig().HostPath,
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            value = Regex.Replace(
                value,
                "\\[host\\]",
                _configuration.GetConfig().HostPath,
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            value = Regex.Replace(
                value,
                "\\[CurrentDate\\]",
                DateTime.Now.ToLongDateString(),
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return value;
        }
    }
}