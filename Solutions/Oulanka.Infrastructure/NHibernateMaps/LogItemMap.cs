using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class LogItemMap : ClassMap<LogItem>
    {
        public LogItemMap()
        {
            Table("EventLog");

            Id(x => x.Id).UnsavedValue(0).GeneratedBy.Identity();

            Map(x => x.IsVisible);
            Map(x => x.Message).Not.Nullable().Length(10000);
            Map(x => x.MessageDescription).Not.Nullable().Length(10000);
            Map(x => x.ObjectId);
            Map(x => x.ObjectType);
            Map(x => x.Source);
            Map(x => x.Username);
            Map(x => x.Category);
            Map(x => x.EventDate);
            Map(x => x.EventType);
        }
    }
}