using System;
using Oulanka.Domain.Models.Estandares;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Respuestas
{
    public class RespuestaComentario : EntityWithTypedId<Guid>
    {
        public virtual string Valor { get; set; }
        public virtual string Detalle { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        // Relaciones
        public virtual Respuesta Respuesta { get; set; }
    }
}