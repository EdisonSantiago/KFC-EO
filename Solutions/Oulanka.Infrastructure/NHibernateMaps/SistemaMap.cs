using System;
using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Estandares;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class SistemaMap : ClassMap<Sistema>
    {
        public SistemaMap()
        {
            Table("Sistema");

            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);

            // Propiedades
            Map(x => x.Nombre).Length(50).Not.Nullable().Default("''");
            Map(x => x.Descripcion).Length(255).Not.Nullable().Default("''");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Relaciones
            References<Estado>(x => x.Estado).Not.Nullable();
            HasMany<Estandar>(x => x.Estandares)
                .Cascade.All().Inverse()
                .Table("EstandarSistema");
        }
    }
}