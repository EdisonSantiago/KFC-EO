using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class EmailQueueItemMap : ClassMap<EmailQueueItem>
    {
        public EmailQueueItemMap()
        {
            Table("EmailQueue");

            Id(x => x.Id).UnsavedValue(0)
                .GeneratedBy
                .Identity();

            this.Map(x => x.Priority).Not.Nullable().Default("1");
            this.Map(x => x.IsBodyHtml).Not.Nullable().Default("0");
            this.Map(x => x.Subject).Not.Nullable().Length(1024);
            this.Map(x => x.From, "[From]").Not.Nullable().Length(256);
            this.Map(x => x.To, "[To]").Not.Nullable().Length(1024);
            this.Map(x => x.Cc).Length(256);
            this.Map(x => x.Bcc).Length(256);
            this.Map(x => x.Body, "[Body]").Not.Nullable().Nullable().Length(10000);
            this.Map(x => x.NextTryTime);
            this.Map(x => x.NumberOfTries).Not.Nullable();
            this.Map(x => x.CreatedOn).Not.Nullable();

        }
    }
}