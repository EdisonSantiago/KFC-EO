using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class AtributoMap : ClassMap<Atributo>
    {
        public AtributoMap()
        {
            Table("Atributo");

            // Propiedades
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Nombre).Not.Nullable().Length(50).Default("''");
            Map(x => x.Descripcion).Not.Nullable().Length(255).Default("''");
            Map(x => x.Etiqueta).Not.Nullable().Length(255).Default("''");
            Map(x => x.TipoDeDato).Not.Nullable().Default("0");
            Map(x => x.Entidad).Not.Nullable().Length(255).Default("''");
            Map(x => x.Datos).Not.Nullable().Length(255).Default("''");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            HasMany<ValorAtributo>(x => x.Valores).Inverse().Cascade.AllDeleteOrphan().Cascade.All().AsBag();
        }
    }
}