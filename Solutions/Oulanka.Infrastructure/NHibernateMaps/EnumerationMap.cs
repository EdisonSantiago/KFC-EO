using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class EnumerationMap : ClassMap<Enumeration>
    {
        public EnumerationMap()
        {
            Table("Enumerations");

            Id(x => x.Id).UnsavedValue(0).GeneratedBy.Identity();
            Map(x => x.OptionGroup).Length(255).Not.Nullable().Default("''");
            Map(x => x.OptionName).Length(255).Not.Nullable().Default("''");
            Map(x => x.Name).Length(255).Not.Nullable().Default("''");
            Map(x => x.Value).Length(255).Not.Nullable().Default("''");
            Map(x => x.Position).Not.Nullable().Default("0");
            Map(x => x.IsDefault).Not.Nullable().Default("0");

        }
    }
}