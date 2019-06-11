using FluentNHibernate;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class PerfilUsuarioMap : ClassMap<PerfilUsuario>
    {
        public PerfilUsuarioMap()
        {
            Table("PerfilUsuario");

            Id(x => x.Id).UnsavedValue(0).GeneratedBy.Identity();

            Map(x=>x.Nombre).Length(50).Not.Nullable().Default("''");
            Map(x=>x.Apellido).Length(50).Not.Nullable().Default("''");
            Map(x=>x.Direccion).Length(200).Not.Nullable().Default("''");
            Map(x=>x.Telefono).Length(20).Not.Nullable().Default("''");

            References(x => x.Usuario).Unique().Cascade.All();
        }
    }
}