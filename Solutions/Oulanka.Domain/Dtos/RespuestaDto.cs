using System;

namespace Oulanka.Domain.Dtos
{
    public class RespuestaDto
    {
        public Guid Id { get; set; }
        public  short Valor { get; set; }
        public  string Detalle { get; set; }
        public  DateTime FechaRespuesta { get; set; }
        public  string CreadoPor { get; set; }
        public  string ActualizadoPor { get; set; }
        public  DateTime CreadoEn { get; set; }
        public  DateTime ActualizadoEn { get; set; }
        public Guid EvaluacionId { get; set; }
        public Guid EstandarId { get; set; }

        
    }
}