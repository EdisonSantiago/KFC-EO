using System;

namespace Oulanka.Api.Models.FormModels
{
    public class RespuestaFormModel
    {
        public Guid Id { get; set; }
        public  short Valor { get; set; }
        public  string Detalle { get; set; }
        public  DateTime FechaRespuesta { get; set; }
        public Guid EvaluacionId { get; set; }
        public Guid EstandarId { get; set; }
    }
}