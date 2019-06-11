using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class SettingMap : ClassMap<Setting>
    {
        public SettingMap()
        {
            Table("Settings");

            Id(x => x.Id).UnsavedValue(0).GeneratedBy.Identity();

            Map(x => x.Name).Not.Nullable().Length(500).Default("''");
            Map(x => x.OptionName).Not.Nullable().Length(10000).Default("''");
            Map(x => x.Value).Not.Nullable().Length(10000).Default("''");
            Map(x => x.CreatedBy).Not.Nullable().Length(50).Default("''");
            Map(x => x.UpdatedBy).Not.Nullable().Length(50).Default("''");
            Map(x => x.CreatedOn).Not.Nullable().Default("getdate()");
            Map(x => x.UpdatedOn).Not.Nullable().Default("getdate()");


        }
    }
}