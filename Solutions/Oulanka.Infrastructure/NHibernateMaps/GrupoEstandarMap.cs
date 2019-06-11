using System;
using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Locales;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class GrupoEstandarMap : ClassMap<GrupoEstandar>
    {
        public GrupoEstandarMap()
        {
            Table("GrupoEstandar");

            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);
            Map(x => x.Codigo).Not.Nullable().Length(50).Default("''").Unique();
            Map(x => x.Nombre).Not.Nullable().Length(50).Default("''");
            Map(x => x.Descripcion).Not.Nullable().Length(255).Default("''");
            Map(x => x.Imagen).Not.Nullable().Length(255).Default("''");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Relaciones
            References<Estado>(x => x.Estado).Not.Nullable();
            HasMany<Estandar>(x => x.Estandares).Inverse().Cascade.All().AsBag();

        }
    }
}