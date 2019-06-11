using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Domain.Models.Respuestas;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class RespuestaMap : ClassMap<Respuesta>
    {
        public RespuestaMap()
        {
            Table("Respuesta");

            Id(x => x.Id).GeneratedBy.GuidComb();

            // Propiedades
            Map(x => x.Valor).Not.Nullable().Default("0");
            Map(x => x.Detalle).Length(10000).Not.Nullable().Default("''");
            Map(x => x.FechaRespuesta).Not.Nullable().Default("getdate()");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Relaciones
            References<Evaluacion>(x => x.Evaluacion).Not.Nullable();
            References<Estandar>(x => x.Estandar).Not.Nullable();



        }
    }
}