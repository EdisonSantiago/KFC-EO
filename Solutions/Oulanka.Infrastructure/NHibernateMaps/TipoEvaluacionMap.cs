using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Evaluaciones;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class TipoEvaluacionMap : ClassMap<TipoEvaluacion>
    {
        public TipoEvaluacionMap()
        {
            Table("TipoEvaluacion");

            // Propiedades
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Nombre).Not.Nullable().Length(50).Default("''");
            Map(x => x.Descripcion).Not.Nullable().Length(255).Default("''");
            Map(x => x.Observaciones).Not.Nullable().Length(255).Default("''");
            
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