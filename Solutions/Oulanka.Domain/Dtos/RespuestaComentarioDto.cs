using System;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Respuestas;

namespace Oulanka.Domain.Dtos
{
    public class RespuestaComentarioDto
    {
        public Guid Id { get; set; }
        public string Valor { get; set; }
        public string Detalle { get; set; }

        // auditoría micro a nivel de registro
        public string CreadoPor { get; set; }
        public string ActualizadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime ActualizadoEn { get; set; }

        // Relaciones
        public Guid RespuestaId { get; set; }
    }
}