using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Jerarquias;
using Oulanka.Domain.Models.Locales;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class CadenaMap : ClassMap<Cadena>
    {
        public CadenaMap()
        {
            Table("Cadena");

            // Propiedades
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Nombre).Not.Nullable().Length(50).Default("''");
            Map(x => x.Descripcion).Not.Nullable().Length(255).Default("''");
            Map(x => x.Manual).Not.Nullable().Length(500).Default("''");
            Map(x => x.Logo).Not.Nullable().Length(500).Default("''");
            Map(x => x.FechaFundacion).Not.Nullable().Default("getdate()");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Relaciones
            References<Estado>(x => x.Estado).Not.Nullable();
            HasMany<Local>(x => x.Locales).Inverse().Cascade.All().AsBag();
            HasMany<GerenteNacional>(x => x.GerentesNacionales).Inverse().Cascade.All().AsBag();
        }
    }
}