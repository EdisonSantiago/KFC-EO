using System;
using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Ubicacion;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class ProvinciaMap : ClassMap<Provincia>
    {
        public ProvinciaMap()
        {
            Table("Provincia");

            // Propiedades
            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);
            Map(x => x.Nombre).Not.Nullable().Length(50).Default("''");
            
            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Relaciones
            References<Estado>(x => x.Estado).Not.Nullable();
            References<Region>(x => x.Region).Not.Nullable();
            HasMany<Ciudad>(x => x.Ciudades).Inverse().Cascade.All().AsBag();
        }
    }
}