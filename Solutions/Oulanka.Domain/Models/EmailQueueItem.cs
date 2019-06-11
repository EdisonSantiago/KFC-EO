using System;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models
{
    public class EmailQueueItem : Entity
    {
        public virtual string From { get; set; }
        public virtual string To { get; set; }
        public virtual string Cc { get; set; }
        public virtual string Bcc { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }

        public virtual Encoding BodyEncoding => Encoding.UTF8;
        public virtual DateTime CreatedOn { get; set; }
        public virtual bool IsBodyHtml { get; set; }
        public virtual DateTime? NextTryTime { get; set; }
        public virtual int NumberOfTries { get; set; }
        public virtual short Priority { get; set; }
    }
}