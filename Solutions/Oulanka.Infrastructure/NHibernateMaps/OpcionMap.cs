using System;
using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Estandares;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class OpcionMap : ClassMap<Opcion>
    {
        public OpcionMap()
        {
            Table("Opcion");

            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);
            Map(x => x.Nombre).Not.Nullable().Length(50).Default("''");
            Map(x => x.Etiqueta).Not.Nullable().Length(255).Default("''");
            Map(x => x.Valor).Not.Nullable().Length(10000).Default("''");
            Map(x => x.TipoOpcion).Not.Nullable().Default("0");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Relaciones
            References<Estado>(x => x.Estado).Not.Nullable();
        }
    }
}