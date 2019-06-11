using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Domain.Models.Jerarquias;
using Oulanka.Domain.Models.Locales;
using Oulanka.Domain.Models.Personal;
using Oulanka.Domain.Models.Respuestas;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class EvaluacionMap : ClassMap<Evaluacion>
    {
        public EvaluacionMap()
        {
            Table("Evaluacion");

            // Propiedades
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.FechaEvaluacion).Not.Nullable().Default("getdate()");
            Map(x => x.HoraEvaluacion).Not.Nullable().Default("getdate()");
            Map(x => x.NombreRGM).Not.Nullable().Length(100).Default("''");
            Map(x => x.NombreMIC).Not.Nullable().Length(255).Default("''");
            Map(x => x.ParteDelDia).Not.Nullable().Default("0");
            Map(x => x.TipoVisita).Not.Nullable().Default("0");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Relaciones
            References<Estado>(x => x.Estado).Not.Nullable();
            References<TipoEvaluacion>(x => x.TipoEvaluacion).Not.Nullable();
            References<Posicion>(x => x.PosicionMIC).Not.Nullable();
            References<Local>(x => x.Local).Not.Nullable();

            HasMany<Respuesta>(x => x.Respuestas).Inverse().Cascade.All().AsBag();
            HasMany<ImagenEvaluacion>(x => x.Imagenes).Inverse().Cascade.All().AsBag();
        }
    }
}