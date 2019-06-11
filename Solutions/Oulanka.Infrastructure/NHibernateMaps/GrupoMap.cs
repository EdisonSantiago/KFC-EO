using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class GrupoMap : ClassMap<Grupo>
    {
        public GrupoMap()
        {
            Table("Grupo");

            Id(x => x.Id).UnsavedValue(0).GeneratedBy.Identity();

            Map(x => x.Nombre).Length(100).Not.Nullable().Default("''");
            Map(x => x.Descripcion).Length(255).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.CreadoPor).Length(100).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(100).Not.Nullable().Default("''");

            HasManyToMany<Usuario>(x => x.Usuarios)
                .Cascade.SaveUpdate()
                .Inverse().LazyLoad()
                .Table("GruposUsuario");



        }
    }
}