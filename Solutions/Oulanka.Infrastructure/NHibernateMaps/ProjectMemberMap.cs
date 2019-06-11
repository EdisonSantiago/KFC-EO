using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class ProjectMemberMap : ClassMap<ProjectMember>
    {
        public ProjectMemberMap()
        {
            Table("ProjectMembers");

            Id(x => x.Id).UnsavedValue(0).GeneratedBy.Identity();

            References<Usuario>(x => x.Usuario).Cascade.All()
                .Not.Nullable();

            References<Project>(x => x.Project).Cascade.All()
                .Not.Nullable();

            References<Grupo>(x => x.Grupo).Cascade.All()
                .Not.Nullable();

            Map(x => x.CreatedBy).Not.Nullable().Length(50).Default("''");
            Map(x => x.CreatedOn).Not.Nullable().Default("getdate()");

        }
    }
}