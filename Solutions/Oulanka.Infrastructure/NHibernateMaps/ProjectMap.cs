using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Table("Projects");

            Id(x => x.Id).UnsavedValue(0).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable().Length(250).Default("''");
            Map(x => x.Description).Not.Nullable().Length(500).Default("''");
            Map(x => x.Identifier).Not.Nullable().Length(50).Default("''");
            Map(x => x.IsPrivate).Not.Nullable().Default("0");
            Map(x => x.CreatedOn).Not.Nullable().Default("getdate()");
            Map(x => x.CreatedBy).Not.Nullable().Length(50).Default("''");
            Map(x => x.UpdatedOn).Not.Nullable().Default("getdate()");
            Map(x => x.UpdateBy).Not.Nullable().Length(50).Default("''");

            HasMany<ProjectMember>(x => x.Members).Cascade.All()
                .LazyLoad()
                .Inverse();

        }
    }
}