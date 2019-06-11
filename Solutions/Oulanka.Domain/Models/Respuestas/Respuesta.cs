using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Evaluaciones;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Respuestas
{
    public class Respuesta : EntityWithTypedId<Guid>
    {
        public virtual short Valor { get; set; }
        public virtual string Detalle { get; set; }
        public virtual DateTime FechaRespuesta { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        // Relaciones
        public virtual Evaluacion Evaluacion { get; set; }
        public virtual Estandar Estandar { get; set; }

        [ScriptIgnore]
        public virtual IEnumerable<RespuestaComentario> Comentarios { get; set; }
        [ScriptIgnore]
        public virtual IEnumerable<ImagenEstandar> Imagenes { get; set; }
    }
}