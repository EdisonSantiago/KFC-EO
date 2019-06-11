using System;
using FluentNHibernate.Mapping;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Evaluaciones;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class TipoEvaluacionGrupoEstandarMap : ClassMap<TipoEvaluacionGrupoEstandar>
    {
        public TipoEvaluacionGrupoEstandarMap()
        {
            Table("TipoEvaluacionGrupoEstandar");

            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);
            Map(x => x.Orden).Not.Nullable().Default("0");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            References<TipoEvaluacion>(x => x.TipoEvaluacion).Cascade.All().Not.Nullable();
            References<GrupoEstandar>(x => x.GrupoEstandar).Cascade.All().Not.Nullable();

        }
    }
}