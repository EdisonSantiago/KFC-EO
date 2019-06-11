using System;
using System.Net.Mail;
using System.Xml;

namespace Oulanka.Web.Core.Common
{
    [Serializable]
    public class EmailTemplate
    {
        public string Body { get; set; }
        public string EmailType { get; set; }
        public string From { get; set; }
        public MailPriority Priority { get; set; }
        public string Subject { get; set; }

        public EmailTemplate() { }

        public EmailTemplate(XmlNode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            EmailType = node.Attributes.GetNamedItem("emailType").InnerText;
            Priority =
                (MailPriority)Enum.Parse(Priority.GetType(), node.Attributes.GetNamedItem("priority").InnerText);
            Subject = node.SelectSingleNode("subject").InnerText;
            Body = node.SelectSingleNode("body").InnerText;
            From = node.SelectSingleNode("from").InnerText;
        }

        public EmailTemplate(string emailType, MailPriority priority, string @from, string subject, string body)
        {
            EmailType = emailType;
            Priority = priority;
            From = from;
            Subject = subject;
            Body = body;
        }
    }
}