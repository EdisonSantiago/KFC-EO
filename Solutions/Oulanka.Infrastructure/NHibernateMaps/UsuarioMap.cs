using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("Usuario");

            Id(x => x.Id).UnsavedValue(0).GeneratedBy.Identity();

            Map(x => x.NombreUsuario).Length(100).Not.Nullable().Default("''");
            Map(x => x.NombreMostrar).Length(100).Not.Nullable().Default("''");
            Map(x => x.Email).Length(100).Not.Nullable().Default("''");
            Map(x => x.EstaBloqueado).Not.Nullable().Default("0");
            Map(x => x.EstaEnLinea).Not.Nullable().Default("1");
            Map(x => x.EsLdap).Not.Nullable().Default("0");
            Map(x => x.LocalPassword).Length(255).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.CuentaAccesosFallidos).Not.Nullable().Default("0");
            Map(x => x.UltimoLoginEn).Not.Nullable().Default("getdate()");
            Map(x => x.UltimaActividadEn).Not.Nullable().Default("getdate()");

            HasManyToMany<Grupo>(x => x.Grupos)
                .Cascade.All().LazyLoad()
                .Table("GruposUsuario");

            HasOne(x => x.PerfilUsuario)
                .Cascade.All()
                .PropertyRef("Usuario");
        }
    }
}