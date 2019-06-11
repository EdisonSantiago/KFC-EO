using System;
using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Estandares;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class ComentarioMap : ClassMap<Comentario>
    {
        public ComentarioMap()
        {
            Table("Comentario");

            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);
            Map(x => x.Nombre).Not.Nullable().Length(50).Default("''");
            Map(x => x.Descripcion).Not.Nullable().Length(255).Default("''");
            Map(x => x.Valor).Not.Nullable().Length(10000).Default("''");
            Map(x => x.TipoComentario).Not.Nullable().Default("0");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Relaciones
            References<Estado>(x => x.Estado).Not.Nullable();
            References<Estandar>(x => x.Estandar).Not.Nullable();

            HasMany<Opcion>(x => x.Opciones).Inverse().Cascade.All().AsBag();
        }
    }
}