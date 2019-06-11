using FluentNHibernate.Mapping;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Respuestas;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class RespuestaComentarioMap : ClassMap<RespuestaComentario>
    {
        public RespuestaComentarioMap()
        {
            Table("RespuestaComentario");

            Id(x => x.Id).GeneratedBy.GuidComb();

            // Propiedades
            Map(x => x.Valor).Length(10000).Not.Nullable().Default("''");
            Map(x => x.Detalle).Length(10000).Not.Nullable().Default("''");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Relaciones
            References<Respuesta>(x => x.Respuesta).Not.Nullable();

        }
    }
}