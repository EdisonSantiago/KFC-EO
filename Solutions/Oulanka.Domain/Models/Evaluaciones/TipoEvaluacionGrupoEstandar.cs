using System;
using Oulanka.Domain.Models.Estandares;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Evaluaciones
{
    public class TipoEvaluacionGrupoEstandar : EntityWithTypedId<Guid>
    {
        public virtual int Orden { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        // Relaciones
        public virtual TipoEvaluacion TipoEvaluacion { get; set; }
        public virtual GrupoEstandar GrupoEstandar { get; set; }
    }
}