using System;
using Oulanka.Domain.Enums;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models
{
    public class LogItem : Entity
    {
        public virtual string Category { get; set; }
        public virtual DateTime EventDate { get; set; }
        public virtual short EventType { get; set; }

        public virtual string EventTypeLabel => 
            Enum.GetName(typeof (EventType), EventType);

        public virtual string Message { get; set; }
        public virtual string MessageDescription { get; set; }
        public virtual string ObjectId { get; set; }
        public virtual string ObjectType { get; set; }
        public virtual string Source { get; set; }
        public virtual string Username { get; set; }
        public virtual bool IsVisible { get; set; }

    }
}